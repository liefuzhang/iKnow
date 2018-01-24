using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iKnow.Models;
using iKnow.ViewModels;
using System.Data.Entity;
using iKnow.Helper;
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
            var question = _context.Questions.Include("Topics").SingleOrDefault(q => q.Id == id);
            if (question == null) {
                return HttpNotFound();
            }

            // explicit loading (to avoid too complex query)
            _context.Answers.Where(a => a.QuestionId == question.Id).Load();

            var viewModel = QuestionDetailViewModel(question);

            return View(viewModel);
        }

        private QuestionDetailViewModel QuestionDetailViewModel(Question question) {
            var currentUserId = User.Identity.GetUserId();
            bool canUserEditQuestion = User.Identity.IsAuthenticated
                                       && (question.AppUserId == currentUserId
                                           || User.IsInRole(Constants.AdminRoleName));

            var existingAnswer = _context.Answers.SingleOrDefault(
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
                var questionInDb = _context.Questions.Include("Topics").Single(q => q.Id == formViewModel.Question.Id);
                questionInDb.Title = formViewModel.Question.Title;
                questionInDb.Description = formViewModel.Question.Description;
                questionToSave = questionInDb;
            } else {
                questionToSave.AppUserId = User.Identity.GetUserId();
                _context.Questions.Add(questionToSave);
            }

            UpdateQuestionTopics(questionToSave, formViewModel.TopicIds);

            _context.SaveChanges();
            return questionToSave;
        }

        private void UpdateQuestionTopics(Question questionToSave, int[] topicIds) {
            questionToSave.ClearTopics();
            if (topicIds != null && topicIds.Length > 0) {
                var topics = _context.Topics.Where(t => topicIds.Contains(t.Id)).ToList();
                foreach (var topic in topics) {
                    questionToSave.AddTopic(topic);
                }
            }
        }

        private bool DoesQuestionTitleExist(Question question) {
            return question.Id == 0 && _context.Questions.Any(q => q.Title == question.Title);
        }

        private static void TrimInput(Question question) {
            question.Title = MyHelper.CapitalizeWords(question.Title)?.Trim();
            question.Description = MyHelper.CapitalizeWords(question.Description)?.Trim();

            if (question.Title != null && !question.Title.EndsWith("?")) {
                question.Title += "?";
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult SaveQuestionTopics(QuestionFormViewModel formViewModel) {
            var questionPosted = formViewModel.Question;

            var questionInDb = _context.Questions.Include("Topics").Single(q => q.Id == questionPosted.Id);

            UpdateQuestionTopics(questionInDb, formViewModel.TopicIds);

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

            var viewModel = ConstructQuestionAnswerCountViewModel(relatedQuestions);

            return PartialView("_SideBarRelatedQuestionsPartial", viewModel);
        }

        private QuestionAnswerCountViewModel ConstructQuestionAnswerCountViewModel(IQueryable<Question> relatedQuestions) {
            const int relatedQuestionMaxNumber = 5;
            var questionsWithAnswerCount = relatedQuestions.GroupJoin(_context.Answers,
                q => q.Id,
                a => a.QuestionId,
                (question, answers) =>
                    new {
                        Question = question,
                        AnswerCount = answers.Count()
                    }).OrderBy(a => Guid.NewGuid())
                .Take(relatedQuestionMaxNumber)
                .ToDictionary(a => a.Question, a => a.AnswerCount);

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
            var question = _context.Questions.Include("Answers").Single(q => q.Id == viewModel.Question.Id);
            if (question.AppUserId == currentUserId
                || User.IsInRole(Constants.AdminRoleName)) {
                _context.Answers.RemoveRange(question.Answers);
                _context.Questions.Remove(question);

                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}