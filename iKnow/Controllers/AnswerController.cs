using iKnow.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Core.Models.Constants;
using iKnow.Persistence;

namespace iKnow.Controllers {
    public class AnswerController : Controller {
        private iKnowContext _context = new iKnowContext();
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public AnswerController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose();
        }

        // GET: Answer
        public ActionResult Index() {
            int page = 0, pageSize = Constants.DefaultPageSize;
            var viewModel = ConstructAnswerIndexViewModel(page, pageSize);

            return View(viewModel);
        }

        private QuestionAnswerCountViewModel ConstructAnswerIndexViewModel(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _unitOfWork.QuestionRepository.GetQuestionsOrdeyByDescending(query =>
                query.OrderByDescending(question => question.Id), currentPage*pageSize, pageSize);
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(questions);

            return new QuestionAnswerCountViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount,
                Page = currentPage,
                PageSize = pageSize
            };

        }

        public ActionResult Detail(int id) {
            var answer = _context.Answers.Include("Question").SingleOrDefault(a => a.Id == id);
            if (answer == null) {
                return HttpNotFound();
            }

            var viewModel = ConstructAnswerDetailViewModel(answer);

            return View(viewModel);
        }

        private AnswerDetailViewModel ConstructAnswerDetailViewModel(Answer answer) {
            var question = _context.Questions.Include("Topics").Single(q => q.Id == answer.Question.Id);
            var answerCount = _context.Answers.Count(a => a.QuestionId == question.Id);

            // TODO: can we utilize ConstructQuestionDetailViewModel method in QuestionController?
            var currentUserId = User.Identity.GetUserId();
            var existingAnswer = _context.Answers.SingleOrDefault(
                a => a.QuestionId == question.Id && a.AppUserId == currentUserId);

            var questionDetailViewModel = new QuestionDetailViewModel {
                Question = question,
                CanUserEditQuestion = User.Identity.IsAuthenticated
                                      && (question.AppUserId == currentUserId
                                          || User.IsInRole(Constants.AdminRoleName)),
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


        [Route("Answer/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var viewModel = ConstructAnswerIndexViewModel(++currentPage);
            if (!viewModel.QuestionsWithAnswerCount.Any()) {
                return null;
            }

            return PartialView("_AnswerQuestionListPartial", viewModel.QuestionsWithAnswerCount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(QuestionDetailViewModel viewModel) {
            var currentUserId = User.Identity.GetUserId();
            Answer answerToSave;
            var existingAnswer =
                _context.Answers.SingleOrDefault(
                    a => a.QuestionId == viewModel.Question.Id && a.AppUserId == currentUserId);
            if (existingAnswer != null) {
                existingAnswer.Content = viewModel.AnswerPanelContent;
                existingAnswer.UpdatedDate = DateTime.Now;
                answerToSave = existingAnswer;
            } else {
                answerToSave = new Answer {
                    Content = viewModel.AnswerPanelContent,
                    QuestionId = viewModel.Question.Id,
                    AppUserId = User.Identity.GetUserId(),
                    CreatedDate = DateTime.Now
                };
                _context.Answers.Add(answerToSave);
            }

            try {
                _context.SaveChanges();
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError("", error.ErrorMessage);
                return View("~/Views/Question/Detail.cshtml", viewModel);
            }

            return RedirectToAction("Detail", "Answer", new { id = answerToSave.Id });
        }

        public PartialViewResult EditIcon(int id) {
            var answer = _context.Answers.Single(a => a.Id == id);
            if (User.Identity.IsAuthenticated
                              && answer.AppUserId == User.Identity.GetUserId()) {
                return PartialView("_AnswerEditIconPartial");
            }
            return null;
        }

        public PartialViewResult GetAnswerPanelHeader(string id) {
            var user = _context.Users.Single(u => u.Id == id);
            return PartialView("_AnswerPanelHeaderPartial", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(QuestionDetailViewModel viewModel) {
            var currentUserId = User.Identity.GetUserId();
            var answer = _context.Answers.Single(a => a.Id == viewModel.UserAnswerId);
            if (answer.AppUserId == currentUserId) {
                _context.Answers.Remove(answer);

                _context.SaveChanges();
            }

            return RedirectToAction("Detail", "Question", new { id = viewModel.Question.Id });
        }
    }
}