using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Core.Models;
using iKnow.Persistence;
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
            var keywords = TrimInput(input);
            var topics = GetTopics(keywords);
            var questions = GetQuestions(keywords);

            var viewModel = ConstructSearchResultViewModel(questions, topics);

            return PartialView("_SearchResultPartial", viewModel);
        }

        private SearchResultViewModel ConstructSearchResultViewModel(IQueryable<Question> questions, IQueryable<Topic> topics) {
            // TODO: can we reuse the query in AnswerController ConstructAnswerIndexViewModel method?
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
            return viewModel;
        }

        private IQueryable<Question> GetQuestions(string[] keywords) {
            const int questionCount = 6;
            return _context.Questions
                .Where(question => keywords.All(keyword => question.Title.ToLower().StartsWith(keyword.ToLower()) || question.Title.ToLower().Contains(" " + keyword.ToLower()))).Take(questionCount);
        }

        private IQueryable<Topic> GetTopics(string[] keywords) {
            const int topicCount = 3;
            return _context.Topics
                .Where(topic => keywords.All(keyword => topic.Name.ToLower().StartsWith(keyword.ToLower()) || topic.Name.ToLower().Contains(" " + keyword.ToLower()))).Take(topicCount);
        }

        private static string[] TrimInput(string input) {
            return input.Trim().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}