using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using Constants = iKnow.Core.Models.Constants;
using iKnow.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET: Answer
        public ActionResult Index()
        {
            var viewModel = new QuestionAnswerCountViewModel
            {
                QuestionsWithAnswerCount = GetQuestionsWithAnswerCount(0)
            };

            return View(viewModel);
        }

        private IDictionary<Question, int> GetQuestionsWithAnswerCount(int currentPage, int pageSize = Constants.DefaultPageSize)
        {
            var questions = _unitOfWork.QuestionRepository.GetAll(query =>
                query.OrderByDescending(question => question.Id), skip: currentPage * pageSize, take: pageSize);
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(questions);

            return questionsWithAnswerCount;
        }

        [Route("Answer/LoadMore/{currentPage}")]
        public IActionResult LoadMore(int currentPage)
        {
            var questionsWithAnswerCount = GetQuestionsWithAnswerCount(++currentPage);
            if (questionsWithAnswerCount == null || !questionsWithAnswerCount.Any())
            {
                return Content(string.Empty);
            }

            return PartialView("_AnswerQuestionListPartial", questionsWithAnswerCount);
        }

        public ActionResult Detail(int id)
        {
            var answer = _unitOfWork.AnswerRepository.SingleOrDefault(a => a.Id == id, 
                nameof(Answer.Question) + "," + nameof(Answer.AnswerLikes) + "," + nameof(Answer.Comments));
            if (answer == null)
            {
                return NotFound();
            }

            var viewModel = ConstructAnswerDetailViewModel(answer);

            return View(viewModel);
        }

        private AnswerDetailViewModel ConstructAnswerDetailViewModel(Answer answer)
        {
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == answer.Question.Id, nameof(Question.TopicQuestions));
            var topicIds = question.TopicQuestions.Select(tq => tq.TopicId);
            _ = _unitOfWork.TopicRepository.Get(t => topicIds.Contains(t.Id)).ToList();
            var answerCount = _unitOfWork.AnswerRepository.Count(a => a.QuestionId == question.Id);
            var moreAnswers = _unitOfWork.AnswerRepository.Get(a => a.Id != answer.Id && a.QuestionId == question.Id, 
                    includeProperties: nameof(Answer.AnswerLikes) + "," + nameof(Answer.Comments), take: 2)
                .ToList();

            // TODO: can we utilize ConstructQuestionDetailViewModel method in QuestionController?
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingAnswer = _unitOfWork.AnswerRepository.SingleOrDefault(
                a => a.QuestionId == question.Id && a.AppUserId == userId);

            answer.SetLikedByCurrentUser(userId);
            moreAnswers.ForEach(ma=>ma.SetLikedByCurrentUser(userId));

            var questionDetailViewModel = new QuestionDetailViewModel
            {
                Question = question,
                CanUserEditQuestion = question.CanUserModify(User),
                UserAnswerId = existingAnswer?.Id ?? 0,
                CanUserDeleteAnswerPanelAnswer = existingAnswer != null,
            };

            var viewModel = new AnswerDetailViewModel
            {
                Answer = answer,
                MoreAnswers = moreAnswers,
                QuestionDetailViewModel = questionDetailViewModel,
                AnswerCount = answerCount
            };
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(QuestionDetailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingAnswer =
                _unitOfWork.AnswerRepository.SingleOrDefault(
                    a => a.QuestionId == viewModel.Question.Id && a.AppUserId == userId);
            var isNewAnswer = existingAnswer == null;

            var answerToSave = UpdateOrAddAnswer(viewModel, existingAnswer);

            try
            {
                _unitOfWork.Complete();

                if (isNewAnswer)
                {
                    _unitOfWork.ActivityRepository.Add(
                        Activity.ActivityAnswerQuestion(userId, viewModel.Question.Id, answerToSave.Id));
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("~/Views/Question/Detail.cshtml", viewModel);
            }

            return RedirectToAction("Detail", "Answer", new { id = answerToSave.Id });
        }

        private Answer UpdateOrAddAnswer(QuestionDetailViewModel viewModel, Answer existingAnswer)
        {
            Answer answerToSave;
            if (existingAnswer != null)
            {
                answerToSave = UpdateAnswer(viewModel.AnswerPanelContent, existingAnswer);
            }
            else
            {
                answerToSave = AddAnswer(viewModel);
            }
            return answerToSave;
        }

        private Answer AddAnswer(QuestionDetailViewModel viewModel)
        {
            var answer = new Answer
            {
                Content = viewModel.AnswerPanelContent,
                QuestionId = viewModel.Question.Id,
                AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreatedDate = DateTime.Now
            };
            _unitOfWork.AnswerRepository.Add(answer);
            return answer;
        }

        private static Answer UpdateAnswer(string content, Answer existingAnswer)
        {
            existingAnswer.UpdateContent(content);
            return existingAnswer;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(QuestionDetailViewModel viewModel)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == viewModel.UserAnswerId);

            if (answer.AppUserId != currentUserId)
            {
                return new UnauthorizedResult();
            }
            _unitOfWork.AnswerRepository.Remove(answer);

            var activity = _unitOfWork.ActivityRepository.SingleOrDefault(a => a.Type == ActivityType.AnswerQuestion &&
                                                                               a.AppUserId == currentUserId &&
                                                                               a.AnswerId == viewModel.UserAnswerId);
            if (activity != null)
                _unitOfWork.ActivityRepository.Remove(activity);

            _unitOfWork.Complete();

            return RedirectToAction("Detail", "Question", new { id = viewModel.Question.Id });
        }
    }
}