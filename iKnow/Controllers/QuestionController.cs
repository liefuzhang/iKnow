using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iKnow.Models;
using iKnow.ViewModels;
using System.Data.Entity;
using Constants = iKnow.Models.Constants;
using Microsoft.AspNet.Identity;

namespace iKnow.Controllers {
    public class QuestionController : Controller {
        private iKnowContext _context;

        public QuestionController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public PartialViewResult GetForm(int? id) {
            Question question = null;
            if (id.HasValue && id.Value > 0) {
                question = _context.Questions.Include("Topics").SingleOrDefault(q => q.Id == id);
            }
            if (question == null) {
                question = new Question();
            }

            var viewModel = ConstructQuestionFormViewModel(question);

            return PartialView("_QuestionFormModalPartial", viewModel);
        }

        public PartialViewResult GetTopic(int id) {
            Question question = null;
            question = _context.Questions.Include("Topics").Single(q => q.Id == id);

            var viewModel = ConstructQuestionFormViewModel(question);

            return PartialView("_QuestionTopicModalPartial", viewModel);
        }

        private QuestionFormViewModel ConstructQuestionFormViewModel(Question question) {
            var topics = _context.Topics.Select(t => new {
                TopicId = t.Id,
                TopicName = t.Name
            }).ToList();

            var selectedTopicIds = question.Topics.Select(t => t.Id).ToArray();

            var viewModel = new QuestionFormViewModel {
                Question = question,
                Topics = new MultiSelectList(topics, "TopicId", "TopicName"),
                TopicIds = selectedTopicIds
            };
            return viewModel;
        }

        public ActionResult Detail(int id) {
            var question = _context.Questions.Include("Topics").SingleOrDefault(q => q.Id == id);
            if (question == null) {
                return HttpNotFound();
            }

            // explicit loading (to avoid too complex query)
            _context.Answers.Where(a => a.QuestionId == question.Id).Load();

            var currentUserId = User.Identity.GetUserId();
            bool canUserEdit = User.Identity.IsAuthenticated
                               && (question.AppUserId == currentUserId
                                   || User.IsInRole(Constants.AdminRoleName));

            var existingAnswer = _context.Answers.SingleOrDefault(
                     a => a.QuestionId == question.Id && a.AppUserId == currentUserId);

            var viewModel = new QuestionDetailViewModel {
                Question = question,
                CanUserEdit = canUserEdit,
                UserAnswerId = existingAnswer != null ? existingAnswer.Id : 0
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Save(QuestionFormViewModel formViewModel) {
            if (!ModelState.IsValid) {
                // return to current page
                return Redirect(Request.UrlReferrer.ToString());
            }

            var questionPosted = formViewModel.Question;
            var questionToSave = questionPosted;

            if (questionPosted.Id > 0) {
                var questionInDb = _context.Questions.Include("Topics").Single(q => q.Id == questionPosted.Id);
                questionInDb.Title = questionPosted.Title;
                questionInDb.Description = questionPosted.Description;
                questionToSave = questionInDb;
            } else {
                questionToSave.AppUserId = User.Identity.GetUserId();
                _context.Questions.Add(questionToSave);
            }

            questionToSave.ClearTopics();
            if (formViewModel.TopicIds != null && formViewModel.TopicIds.Length > 0) {
                var topics = _context.Topics.Where(t => formViewModel.TopicIds.Contains(t.Id)).ToList();
                foreach (var topic in topics) {
                    questionToSave.AddTopic(topic);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = questionToSave.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SaveQuestionTopics(QuestionFormViewModel formViewModel) {
            var questionPosted = formViewModel.Question;

            var questionInDb = _context.Questions.Include("Topics").Single(q => q.Id == questionPosted.Id);

            questionInDb.ClearTopics();
            if (formViewModel.TopicIds != null && formViewModel.TopicIds.Length > 0) {
                var topics = _context.Topics.Where(t => formViewModel.TopicIds.Contains(t.Id)).ToList();
                foreach (var topic in topics) {
                    questionInDb.AddTopic(topic);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = questionInDb.Id });
        }

        public PartialViewResult GetRelatedQuestions(int id) {
            var currentQuestion = _context.Questions.Include("Topics").Single(q => q.Id == id);
            var topicIds = currentQuestion.Topics.Select(t => t.Id);
            var relatedQuestions = _context.Questions.Where(q => q.Id != id && q.Topics.Any(t => topicIds.Contains(t.Id)));

            if (!relatedQuestions.Any()) {
                return null;
            }

            var questionsWithAnswerCount = relatedQuestions.GroupJoin(_context.Answers,
                q => q.Id,
                a => a.QuestionId,
                (question, answers) =>
                    new {
                        Question = question,
                        AnswerCount = answers.Count()
                    }).OrderBy(a => Guid.NewGuid())
                    .Take(5)
                    .ToDictionary(a => a.Question, a => a.AnswerCount);

            var viewModel = new QuestionAnswerCountViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };

            return PartialView("_SideBarRelatedQuestionsPartial", viewModel);
        }
    }
}