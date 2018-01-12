﻿using iKnow.Models;
using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Models.Constants;

namespace iKnow.Controllers {
    public class AnswerController : Controller {
        private iKnowContext _context = new iKnowContext();

        public AnswerController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET: Answer
        public ActionResult Index() {
            int page = 0, pageSize = Constants.DefaultPageSize;
            var viewModel = constructAnswerIndexViewModel(page, pageSize);

            return View(viewModel);
        }

        private QuestionAnswerCountViewModel constructAnswerIndexViewModel(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questionsWithAnswerCount = _context.Questions.OrderByDescending(q => q.Id).Skip(currentPage * pageSize).Take(pageSize).GroupJoin(_context.Answers,
               q => q.Id,
               a => a.QuestionId,
               (question, answers) =>
               new {
                   Question = question,
                   AnswerCount = answers.Count()
               }).ToDictionary(a => a.Question, a => a.AnswerCount);

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

            var question = _context.Questions.Include("Topics").Single(q => q.Id == answer.Question.Id);
            var answerCount = _context.Answers.Count(a => a.QuestionId == question.Id);
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

            return View(viewModel);
        }


        [Route("Answer/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var viewModel = constructAnswerIndexViewModel(++currentPage);
            if (viewModel.QuestionsWithAnswerCount.Count() == 0) {
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

            _context.SaveChanges();

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