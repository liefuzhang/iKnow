using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Core.Models.Constants;
using iKnow.Persistence;

namespace iKnow.Controllers {
    public class AnswerController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public AnswerController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET: Answer
        public ActionResult Index() {
            var viewModel = new QuestionAnswerCountViewModel {
                QuestionsWithAnswerCount = GetQuestionsWithAnswerCount(0)
            };

            return View(viewModel);
        }

        private IDictionary<Question, int> GetQuestionsWithAnswerCount(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _unitOfWork.QuestionRepository.GetAll(query =>
                query.OrderByDescending(question => question.Id), skip: currentPage * pageSize, take: pageSize);
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(questions);

            return questionsWithAnswerCount;
        }

        [Route("Answer/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var questionsWithAnswerCount = GetQuestionsWithAnswerCount(++currentPage);
            if (questionsWithAnswerCount == null || !questionsWithAnswerCount.Any()) {
                return null;
            }

            return PartialView("_AnswerQuestionListPartial", questionsWithAnswerCount);
        }

        public ActionResult Detail(int id) {
            var answer = _unitOfWork.AnswerRepository.SingleOrDefault(a => a.Id == id, "Question");
            if (answer == null) {
                return HttpNotFound();
            }

            var viewModel = ConstructAnswerDetailViewModel(answer);

            return View(viewModel);
        }

        private AnswerDetailViewModel ConstructAnswerDetailViewModel(Answer answer) {
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == answer.Question.Id, nameof(Question.Topics));
            var answerCount = _unitOfWork.AnswerRepository.Count(a => a.QuestionId == question.Id);

            // TODO: can we utilize ConstructQuestionDetailViewModel method in QuestionController?
            var userId = User.Identity.GetUserId();
            var existingAnswer = _unitOfWork.AnswerRepository.SingleOrDefault(
                a => a.QuestionId == question.Id && a.AppUserId == userId);

            var questionDetailViewModel = new QuestionDetailViewModel {
                Question = question,
                CanUserEditQuestion = question.CanUserModify(User),
                UserAnswerId = existingAnswer?.Id ?? 0,
                CanUserDeleteAnswerPanelAnswer = existingAnswer != null
            };

            var viewModel = new AnswerDetailViewModel {
                Answer = answer,
                QuestionDetailViewModel = questionDetailViewModel,
                AnswerCount = answerCount
            };
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(QuestionDetailViewModel viewModel) {
            var userId = User.Identity.GetUserId();
            var existingAnswer =
                _unitOfWork.AnswerRepository.SingleOrDefault(
                    a => a.QuestionId == viewModel.Question.Id && a.AppUserId == userId);
            var isNewAnswer = existingAnswer == null;

            var answerToSave = UpdateOrAddAnswer(viewModel, existingAnswer);

            try {
                _unitOfWork.Complete();

                if (isNewAnswer) {
                    _unitOfWork.ActivityRepository.Add(
                        Activity.ActivityAnswerQuestion(userId, viewModel.Question.Id, answerToSave.Id));
                    _unitOfWork.Complete();
                }
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors?.FirstOrDefault()?.ValidationErrors?.FirstOrDefault();
                ModelState.AddModelError("", error?.ErrorMessage);
                return View("~/Views/Question/Detail.cshtml", viewModel);
            }

            return RedirectToAction("Detail", "Answer", new { id = answerToSave.Id });
        }

        private Answer UpdateOrAddAnswer(QuestionDetailViewModel viewModel, Answer existingAnswer) {
            Answer answerToSave;
            if (existingAnswer != null) {
                answerToSave = UpdateAnswer(viewModel.AnswerPanelContent, existingAnswer);
            } else {
                answerToSave = AddAnswer(viewModel);
            }
            return answerToSave;
        }

        private Answer AddAnswer(QuestionDetailViewModel viewModel) {
            var answer = new Answer {
                Content = viewModel.AnswerPanelContent,
                QuestionId = viewModel.Question.Id,
                AppUserId = User.Identity.GetUserId(),
                CreatedDate = DateTime.Now
            };
            _unitOfWork.AnswerRepository.Add(answer);
            return answer;
        }

        private static Answer UpdateAnswer(string content, Answer existingAnswer) {
            existingAnswer.UpdateContent(content);
            return existingAnswer;
        }

        public PartialViewResult EditIcon(int id) {
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == id);
            if (User.Identity.IsAuthenticated
                && answer.AppUserId == User.Identity.GetUserId()) {
                return PartialView("_AnswerEditIconPartial");
            }

            return null;
        }

        public PartialViewResult GetAnswerPanelHeader(string id) {
            var user = _unitOfWork.UserRepository.Single(u => u.Id == id);
            return PartialView("_AnswerPanelHeaderPartial", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(QuestionDetailViewModel viewModel) {
            var currentUserId = User.Identity.GetUserId();
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == viewModel.UserAnswerId);

            if (answer.AppUserId != currentUserId) {
                return new HttpUnauthorizedResult();
            }
            _unitOfWork.AnswerRepository.Remove(answer);
            _unitOfWork.Complete();

            return RedirectToAction("Detail", "Question", new { id = viewModel.Question.Id });
        }
    }
}