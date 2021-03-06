﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Helper;
using iKnow.Persistence;
using Constants = iKnow.Core.Models.Constants;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace iKnow.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public QuestionController()
        {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult GetForm(int? id)
        {
            Question question = null;
            if (id.HasValue && id.Value > 0)
            {
                question = _unitOfWork.QuestionRepository.Single(q => q.Id == id, nameof(Question.Topics));
            }
            if (question == null)
            {
                question = new Question();
            }

            var viewModel = ConstructQuestionFormViewModel(question);

            return PartialView("_QuestionFormModalPartial", viewModel);
        }

        public PartialViewResult GetTopic(int id)
        {
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == id, nameof(Question.Topics));

            var viewModel = ConstructQuestionFormViewModel(question);

            return PartialView("_QuestionTopicModalPartial", viewModel);
        }

        private QuestionFormViewModel ConstructQuestionFormViewModel(Question question)
        {
            var topics = _unitOfWork.TopicRepository.GetAll().Select(t => new
            {
                TopicId = t.Id,
                TopicName = t.Name
            }).ToList();

            var selectedTopicIds = question.Topics.Select(t => t.Id).ToArray();

            var viewModel = new QuestionFormViewModel
            {
                Question = question,
                Topics = new MultiSelectList(topics, "TopicId", "TopicName"),
                TopicIds = selectedTopicIds,
                CanUserDelete = question.CanUserModify(User)
            };

            return viewModel;
        }

        public ActionResult Detail(int id)
        {
            var question = _unitOfWork.QuestionRepository.SingleOrDefault(q => q.Id == id, nameof(Question.Topics));
            if (question == null)
            {
                return HttpNotFound();
            }

            // Load question answers into context
            GetQuestionAnswers(question.Id, 0);

            var viewModel = ConstructQuestionDetailViewModel(question);

            return View(viewModel);
        }

        private QuestionDetailViewModel ConstructQuestionDetailViewModel(Question question)
        {
            var userId = User.Identity.GetUserId();
            var existingAnswer = _unitOfWork.AnswerRepository.SingleOrDefault(
                a => a.QuestionId == question.Id && a.AppUserId == userId);

            var viewModel = new QuestionDetailViewModel
            {
                Question = question,
                CanUserEditQuestion = question.CanUserModify(User),
                UserAnswerId = existingAnswer?.Id ?? 0,
                CanUserDeleteAnswerPanelAnswer = existingAnswer != null,
                AnswerCount = _unitOfWork.AnswerRepository.Count(a => a.QuestionId == question.Id)
            };
            return viewModel;
        }

        private IEnumerable<Answer> GetQuestionAnswers(int questionId, int currentPage, int pageSize = Constants.DefaultPageSize / 2)
        {
            var answers = _unitOfWork.AnswerRepository.Get(a => a.QuestionId == questionId,
                q => q.OrderByDescending(a => a.CreatedDate), nameof(Answer.Comments) + "," + nameof(Answer.AnswerLikes),
                currentPage * pageSize, pageSize).ToList();

            var currentUserId = User.Identity.GetUserId();
            answers.ForEach(a => a.SetLikedByCurrentUser(currentUserId));

            return answers;
        }

        [Route("Question/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage, int questionId)
        {
            var answers = GetQuestionAnswers(questionId, ++currentPage);
            if (!answers.Any())
            {
                return null;
            }

            return PartialView("_QuestionAllAnswerPartial", answers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(QuestionFormViewModel formViewModel)
        {
            if (!ModelState.IsValid)
            {
                // return to current page
                return Redirect(Request.UrlReferrer.ToString());
            }

            formViewModel.Question.TrimTitleAndDescription();

            // check if new question title is unique 
            if (DoesQuestionTitleExist(formViewModel.Question))
            {
                TempData["pageError"] = "Question already exists.";
                return Redirect(Request.UrlReferrer.ToString());
            }

            var questionSaved = SaveQuestion(formViewModel);
            return RedirectToAction("Detail", new { id = questionSaved.Id });
        }

        private Question SaveQuestion(QuestionFormViewModel formViewModel)
        {
            var questionToSave = formViewModel.Question;
            var isQuestionNew = questionToSave.Id == 0;

            questionToSave = UpdateOrAddQuestion(questionToSave);

            if (questionToSave.CanUserModify(User))
            {
                UpdateQuestionTopics(formViewModel.TopicIds, questionToSave);
                _unitOfWork.Complete();

                if (isQuestionNew)
                    AddAddQuestionActivity(User.Identity.GetUserId(), questionToSave.Id);
            }

            return questionToSave;
        }

        private Question UpdateOrAddQuestion(Question question)
        {
            if (question.Id > 0)
            {
                question = UpdateQuestion(question);
            }
            else
            {
                AddQuestion(question);
            }
            return question;
        }

        private void AddQuestion(Question question)
        {
            question.SetUserId(User.Identity.GetUserId());
            _unitOfWork.QuestionRepository.Add(question);
        }

        private Question UpdateQuestion(Question question)
        {
            var questionInDb = _unitOfWork.QuestionRepository.Single(
                q => q.Id == question.Id, nameof(Question.Topics));
            questionInDb.UpdateTitleAndDescription(question.Title, question.Description);

            return questionInDb;
        }

        private void UpdateQuestionTopics(int[] topicIds, Question question)
        {
            if (topicIds == null || topicIds.Length == 0)
                return;

            if (!question.CanUserModify(User))
                return;

            var topics = _unitOfWork.TopicRepository.Get(t => topicIds.Contains(t.Id)).ToList();
            question.UpdateQuestionTopics(topics);
        }

        private bool DoesQuestionTitleExist(Question question)
        {
            return question.Id == 0 && _unitOfWork.QuestionRepository.Any(q => q.Title == question.Title);
        }

        private void AddAddQuestionActivity(string userId, int questionId)
        {
            _unitOfWork.ActivityRepository.Add(
                Activity.ActivityAddQuestion(userId, questionId));
            _unitOfWork.Complete();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SaveQuestionTopics(QuestionFormViewModel formViewModel)
        {
            var questionPosted = formViewModel.Question;
            var questionInDb = _unitOfWork.QuestionRepository.Single(
                q => q.Id == questionPosted.Id,
                nameof(Question.Topics));

            UpdateQuestionTopics(formViewModel.TopicIds, questionInDb);
            _unitOfWork.Complete();

            return RedirectToAction("Detail", new { id = questionInDb.Id });
        }

        public PartialViewResult GetRelatedQuestions(int id)
        {
            var currentQuestion = _unitOfWork.QuestionRepository.Single(q => q.Id == id, nameof(Question.Topics));
            var topicIds = currentQuestion.Topics.Select(t => t.Id);
            const int relatedQuestionMaxNumber = 5;
            var relatedQuestions = _unitOfWork.QuestionRepository.Get(q =>
                q.Id != id && q.Topics.Any(t => topicIds.Contains(t.Id)),
                take: relatedQuestionMaxNumber).ToList();

            if (!relatedQuestions.Any())
            {
                return null;
            }

            var viewModel = ConstructQuestionAnswerCountViewModel(relatedQuestions);

            return PartialView("_SideBarRelatedQuestionsPartial", viewModel);
        }

        private QuestionAnswerCountViewModel ConstructQuestionAnswerCountViewModel(IEnumerable<Question> relatedQuestions)
        {
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(relatedQuestions);

            var viewModel = new QuestionAnswerCountViewModel
            {
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };
            return viewModel;
        }

        [Route("Question/GetComments/{answerId}/{currentPage}")]
        public PartialViewResult GetComments(int answerId, int currentPage)
        {
            if (currentPage < 1)
            {
                return null;
            }

            var totalCount = _unitOfWork.CommentRepository.Count(c => c.AnswerId == answerId);
            var pageSize = Constants.CommentPageSize;
            var comments = _unitOfWork.CommentRepository.Get(c => c.AnswerId == answerId,
                q => q.OrderBy(c => c.CreatedDate), nameof(Comment.AppUser), (currentPage - 1) * pageSize, pageSize).ToList();
            var totalPageCount = (totalCount - 1) / Constants.CommentPageSize + 1;

            var viewModel = new AnswerCommentViewModel
            {
                Comments = comments,
                TotalPageCount = totalPageCount,
                CurrentPage = currentPage,
                DisplayPageNumbers = GetDisplayPageNumbers(currentPage, totalPageCount)
            };

            return PartialView("_AnswerCommentPartial", viewModel);
        }

        private List<int> GetDisplayPageNumbers(int currentPage, int totalPageCount)
        {
            var result = new List<int>();
            if (totalPageCount <= 5)
            {
                for (int i = 1; i <= totalPageCount; i++)
                {
                    result.Add(i);
                }
                return result;
            }

            if (currentPage <= 3)
            {
                result.AddRange(new[] { 1, 2, 3, 4, totalPageCount });
                return result;
            }

            if (totalPageCount - currentPage <= 2)
            {
                result.AddRange(new[] { 1, totalPageCount - 3, totalPageCount - 2, totalPageCount - 1, totalPageCount });
                return result;
            }

            result.AddRange(new[] { 1, currentPage - 1, totalPageCount });
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(QuestionFormViewModel viewModel)
        {
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == viewModel.Question.Id, nameof(Question.Answers));
            if (question.CanUserModify(User))
            {
                _unitOfWork.AnswerRepository.RemoveRange(question.Answers);
                _unitOfWork.QuestionRepository.Remove(question);

                var activity = _unitOfWork.ActivityRepository.SingleOrDefault(a => a.Type == ActivityType.AddQuestion &&
                                                                                   a.QuestionId == viewModel.Question.Id);
                if (activity != null)
                    _unitOfWork.ActivityRepository.Remove(activity);

                _unitOfWork.Complete();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}