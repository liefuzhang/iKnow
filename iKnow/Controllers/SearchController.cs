using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.ViewModels;

namespace iKnow.Controllers {
    public class SearchController : Controller {
        private readonly iKnowContext _context;
        public SearchController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public PartialViewResult GetResult(string input) {
            var keywords = input.Trim().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var topics = _context.Topics
                .Where(topic => keywords.All(keyword => topic.Name.ToLower().StartsWith(keyword.ToLower()) || topic.Name.ToLower().Contains(" " + keyword.ToLower()))).Take(3);

            var questions = _context.Questions
                .Where(question => keywords.All(keyword => question.Title.ToLower().StartsWith(keyword.ToLower()) || question.Title.ToLower().Contains(" " + keyword.ToLower()))).Take(6);

            var questionsWithAnswerCount = questions.GroupJoin(_context.Answers,
               q => q.Id,
               a => a.QuestionId,
               (question, answers) =>
               new {
                   Question = question,
                   AnswerCount = answers.Count()
               }).ToDictionary(a => a.Question, a => a.AnswerCount);

            var viewModel = new SearchResultViewModel {
                Topics = topics,
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };

            return PartialView("_SearchResultPartial", viewModel);
        }
    }
}