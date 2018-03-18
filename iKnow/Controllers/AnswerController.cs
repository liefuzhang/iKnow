﻿using System;
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
            int pageSize = Constants.DefaultPageSize;
            var viewModel = ConstructAnswerIndexViewModel(0, pageSize);

            return View(viewModel);
        }

        private QuestionAnswerCountViewModel ConstructAnswerIndexViewModel(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _unitOfWork.QuestionRepository.GetAll(query =>
                query.OrderByDescending(question => question.Id), skip: currentPage * pageSize, take: pageSize);
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(questions);

            return new QuestionAnswerCountViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };
        }

        [Route("Answer/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var viewModel = ConstructAnswerIndexViewModel(++currentPage);
            if (viewModel.QuestionsWithAnswerCount == null || !viewModel.QuestionsWithAnswerCount.Any()) {
                return null;
            }

            return PartialView("_AnswerQuestionListPartial", viewModel.QuestionsWithAnswerCount);
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
            var currentUserId = User.Identity.GetUserId();
            Answer answerToSave;
            var isNewAnswer = false;

            var existingAnswer =
                _unitOfWork.AnswerRepository.SingleOrDefault(
                    a => a.QuestionId == viewModel.Question.Id && a.AppUserId == currentUserId);
            if (existingAnswer != null) {
                existingAnswer.UpdateContent(viewModel.AnswerPanelContent);
                answerToSave = existingAnswer;
            } else {
                isNewAnswer = true;
                answerToSave = new Answer {
                    Content = viewModel.AnswerPanelContent,
                    QuestionId = viewModel.Question.Id,
                    AppUserId = currentUserId,
                    CreatedDate = DateTime.Now
                };
                _unitOfWork.AnswerRepository.Add(answerToSave);
            }

            try {
                _unitOfWork.Complete();

                if (isNewAnswer) {
                    _unitOfWork.ActivityRepository.Add(
                        Activity.ActivityAnswerQuestion(currentUserId, viewModel.Question.Id, answerToSave.Id));
                    _unitOfWork.Complete();
                }
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors?.FirstOrDefault()?.ValidationErrors?.FirstOrDefault();
                ModelState.AddModelError("", error?.ErrorMessage);
                return View("~/Views/Question/Detail.cshtml", viewModel);
            }

            return RedirectToAction("Detail", "Answer", new { id = answerToSave.Id });
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