using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iKnow.ViewModels;
using System.Data.Entity;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Helper;
using iKnow.Persistence;
using Constants = iKnow.Core.Models.Constants;
using Microsoft.AspNet.Identity;

namespace iKnow.Controllers {
    public class QuestionController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public QuestionController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult GetForm(int? id) {
            Question question = null;
            if (id.HasValue && id.Value > 0) {
                question = _unitOfWork.QuestionRepository.Single(q => q.Id == id, "Topics");
            }
            if (question == null) {
                question = new Question();
            }

            var viewModel = ConstructQuestionFormViewModel(question);

            return PartialView("_QuestionFormModalPartial", viewModel);
        }

        public PartialViewResult GetTopic(int id) {
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == id, "Topics");

            var viewModel = ConstructQuestionFormViewModel(question);

            return PartialView("_QuestionTopicModalPartial", viewModel);
        }

        private QuestionFormViewModel ConstructQuestionFormViewModel(Question question) {
            var topics = _unitOfWork.TopicRepository.GetAll().Select(t => new {
                TopicId = t.Id,
                TopicName = t.Name
            }).ToList();

            var selectedTopicIds = question.Topics.Select(t => t.Id).ToArray();
            var currentUserId = User.Identity.GetUserId();

            var viewModel = new QuestionFormViewModel {
                Question = question,
                Topics = new MultiSelectList(topics, "TopicId", "TopicName"),
                TopicIds = selectedTopicIds,
                CanUserDelete = User.Identity.IsAuthenticated
                               && (question.AppUserId == currentUserId
                                   || User.IsInRole(Constants.AdminRoleName)
                               && question.Id > 0)
            };

            return viewModel;
        }

        public ActionResult Detail(int id) {
            var question = _unitOfWork.QuestionRepository.SingleOrDefault(q => q.Id == id, "Topics");
            if (question == null) {
                return HttpNotFound();
            }

            // Load question into context
            _unitOfWork.AnswerRepository.Get(a => a.QuestionId == question.Id);

            var viewModel = ConstructQuestionDetailViewModel(question);

            return View(viewModel);
        }

        private QuestionDetailViewModel ConstructQuestionDetailViewModel(Question question) {
            var currentUserId = User.Identity.GetUserId();
            bool canUserEditQuestion = User.Identity.IsAuthenticated
                                       && (question.AppUserId == currentUserId
                                           || User.IsInRole(Constants.AdminRoleName));

            var existingAnswer = _unitOfWork.AnswerRepository.SingleOrDefault(
                a => a.QuestionId == question.Id && a.AppUserId == currentUserId);

            var viewModel = new QuestionDetailViewModel {
                Question = question,
                CanUserEditQuestion = canUserEditQuestion,
                UserAnswerId = existingAnswer?.Id ?? 0,
                CanUserDeleteAnswerPanelAnswer = existingAnswer != null
            };
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(QuestionFormViewModel formViewModel) {
            if (!ModelState.IsValid) {
                // return to current page
                return Redirect(Request.UrlReferrer.ToString());
            }

            TrimInput(formViewModel.Question);

            // check if new question title is unique 
            if (DoesQuestionTitleExist(formViewModel.Question)) {
                TempData["pageError"] = "Question already exists.";
                return Redirect(Request.UrlReferrer.ToString());
            }

            var questionSaved = SaveQuestion(formViewModel);
            return RedirectToAction("Detail", new { id = questionSaved.Id });
        }

        private Question SaveQuestion(QuestionFormViewModel formViewModel) {
            var questionToSave = formViewModel.Question;
            if (formViewModel.Question.Id > 0) {
                var questionInDb = _unitOfWork.QuestionRepository.Single(q => q.Id == formViewModel.Question.Id, "Topics");
                questionInDb.Title = formViewModel.Question.Title;
                questionInDb.Description = formViewModel.Question.Description;
                questionToSave = questionInDb;
            } else {
                questionToSave.AppUserId = User.Identity.GetUserId();
                _unitOfWork.QuestionRepository.Add(questionToSave);
            }

            UpdateQuestionTopics(questionToSave, formViewModel.TopicIds);

            _unitOfWork.Complete();
            return questionToSave;
        }

        private void UpdateQuestionTopics(Question questionToSave, int[] topicIds) {
            questionToSave.ClearTopics();
            if (topicIds != null && topicIds.Length > 0) {
                var topics = _unitOfWork.TopicRepository.Get(t => topicIds.Contains(t.Id));
                foreach (var topic in topics) {
                    questionToSave.AddTopic(topic);
                }
            }
        }

        private bool DoesQuestionTitleExist(Question question) {
            return question.Id == 0 && _unitOfWork.QuestionRepository.Any(q => q.Title == question.Title);
        }

        private static void TrimInput(Question question) {
            question.Title = MyHelper.CapitalizeWords(question.Title?.Trim());
            question.Description = MyHelper.CapitalizeWords(question.Description?.Trim());

            if (question.Title != null && !question.Title.EndsWith("?")) {
                question.Title += "?";
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SaveQuestionTopics(QuestionFormViewModel formViewModel) {
            var questionPosted = formViewModel.Question;

            var questionInDb = _unitOfWork.QuestionRepository.Single(q => q.Id == questionPosted.Id, "Topics");

            UpdateQuestionTopics(questionInDb, formViewModel.TopicIds);

            _unitOfWork.Complete();
            return RedirectToAction("Detail", new { id = questionInDb.Id });
        }

        public PartialViewResult GetRelatedQuestions(int id) {
            var currentQuestion = _unitOfWork.QuestionRepository.Single(q => q.Id == id, "Topics");
            var topicIds = currentQuestion.Topics.Select(t => t.Id);
            var relatedQuestions = _unitOfWork.QuestionRepository.Get(q => q.Id != id && q.Topics.Any(t => topicIds.Contains(t.Id)));

            if (!relatedQuestions.Any()) {
                return null;
            }

            var viewModel = ConstructQuestionAnswerCountViewModel(relatedQuestions);

            return PartialView("_SideBarRelatedQuestionsPartial", viewModel);
        }

        private QuestionAnswerCountViewModel ConstructQuestionAnswerCountViewModel(IEnumerable<Question> relatedQuestions) {
            const int relatedQuestionMaxNumber = 5;
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(relatedQuestions, relatedQuestionMaxNumber);

            var viewModel = new QuestionAnswerCountViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(QuestionFormViewModel viewModel) {
            var currentUserId = User.Identity.GetUserId();
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == viewModel.Question.Id, "Answers");
            if (question.AppUserId == currentUserId
                || User.IsInRole(Constants.AdminRoleName)) {
                _unitOfWork.AnswerRepository.RemoveRange(question.Answers);
                _unitOfWork.QuestionRepository.Remove(question);

                _unitOfWork.Complete();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}