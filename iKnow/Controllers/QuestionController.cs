﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
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

            var viewModel = new QuestionFormViewModel {
                Question = question,
                Topics = new MultiSelectList(topics, "TopicId", "TopicName"),
                TopicIds = selectedTopicIds,
                CanUserDelete = question.CanUserModify(User)
            };

            return viewModel;
        }

        public ActionResult Detail(int id) {
            var question = _unitOfWork.QuestionRepository.SingleOrDefault(q => q.Id == id, "Topics");
            if (question == null) {
                return HttpNotFound();
            }

            var viewModel = ConstructQuestionDetailViewModel(question);

            return View(viewModel);
        }

        private QuestionDetailViewModel ConstructQuestionDetailViewModel(Question question) {
            var userId = User.Identity.GetUserId();
            var existingAnswer = _unitOfWork.AnswerRepository.SingleOrDefault(
                a => a.QuestionId == question.Id && a.AppUserId == userId);

            var viewModel = new QuestionDetailViewModel {
                Question = question,
                CanUserEditQuestion = question.CanUserModify(User),
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

            formViewModel.Question.TrimTitleAndDescription();

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
                var questionInDb = _unitOfWork.QuestionRepository.Single(
                    q => q.Id == formViewModel.Question.Id,
                    "Topics");
                questionInDb.UpdateTitleAndDescription(questionToSave.Title, questionToSave.Description);
                questionToSave = questionInDb;
            } else {
                questionToSave.SetUserId(User.Identity.GetUserId());
                _unitOfWork.QuestionRepository.Add(questionToSave);
            }

            UpdateQuestionTopicsAndSave(formViewModel, questionToSave);

            return questionToSave;
        }

        private void UpdateQuestionTopicsAndSave(QuestionFormViewModel formViewModel, Question question) {
            if (question.CanUserModify(User)) {
                var topics = _unitOfWork.TopicRepository.Get(t => formViewModel.TopicIds.Contains(t.Id)).ToList();
                question.UpdateQuestionTopics(topics);

                _unitOfWork.Complete();
            }
        }
        
        private bool DoesQuestionTitleExist(Question question) {
            return question.Id == 0 && _unitOfWork.QuestionRepository.Any(q => q.Title == question.Title);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SaveQuestionTopics(QuestionFormViewModel formViewModel) {
            var questionPosted = formViewModel.Question;
            var questionInDb = _unitOfWork.QuestionRepository.Single(
                q => q.Id == questionPosted.Id,
                "Topics");

            UpdateQuestionTopicsAndSave(formViewModel, questionInDb);

            return RedirectToAction("Detail", new { id = questionInDb.Id });
        }

        public PartialViewResult GetRelatedQuestions(int id) {
            var currentQuestion = _unitOfWork.QuestionRepository.Single(q => q.Id == id, "Topics");
            var topicIds = currentQuestion.Topics.Select(t => t.Id);
            const int relatedQuestionMaxNumber = 5;
            var relatedQuestions = _unitOfWork.QuestionRepository.Get(q =>
                q.Id != id && q.Topics.Any(t => topicIds.Contains(t.Id)),
                take: relatedQuestionMaxNumber).ToList();

            if (!relatedQuestions.Any()) {
                return null;
            }

            var viewModel = ConstructQuestionAnswerCountViewModel(relatedQuestions);

            return PartialView("_SideBarRelatedQuestionsPartial", viewModel);
        }

        private QuestionAnswerCountViewModel ConstructQuestionAnswerCountViewModel(IEnumerable<Question> relatedQuestions) {
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(relatedQuestions);

            var viewModel = new QuestionAnswerCountViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(QuestionFormViewModel viewModel) {
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == viewModel.Question.Id, "Answers");
            if (question.CanUserModify(User)) {
                _unitOfWork.AnswerRepository.RemoveRange(question.Answers);
                _unitOfWork.QuestionRepository.Remove(question);

                _unitOfWork.Complete();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}