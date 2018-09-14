using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Moq;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers
{
    [TestFixture]
    public class SearchControllerTests
    {
        private SearchController _controller;
        private iKnowContext _context;
        private iKnowContext _contextAfterAction;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;

        [SetUp]
        public void Setup()
        {
            _context = new iKnowContext();
            _contextAfterAction = new iKnowContext();
            _controller = new SearchController(new UnitOfWork(new iKnowContext()));

            _currentUser = new Mock<IPrincipal>();
            _controller.MockContext(new Mock<HttpRequestBase>(), _currentUser);

            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _contextAfterAction.Dispose();
        }

        [Test, Isolated]
        [TestCase("So", 0, 1, 0)]
        [TestCase("so", 0, 1, 0)]
        [TestCase("top", 0, 2, 0)]
        [TestCase("Ano", 0, 1, 1)]
        [TestCase("que", 0, 0, 2)]
        [TestCase("see", 0, 0, 0)]
        [TestCase("user1", 1, 0, 0)]
        [TestCase("user", 2, 0, 0)]
        public void GetResult_WhenCalled_ShouldReturnSearchResult(string term, int expectedUserCount, int expectedTopicCount, int expectedQuestionCount)
        {
            var topic1 = _context.AddTestTopicToDatabase("Some Topic");
            var topic2 = _context.AddTestTopicToDatabase("Another Topic");
            var question1 = _context.AddTestQuestionToDatabase("Test search keyword question?");
            var question2 = _context.AddTestQuestionToDatabase("Another test search keyword question?");

            _context.SaveChanges();

            var result = _controller.GetResult(term);

            Assert.That((result.Model as SearchResultViewModel).Users.Count(), Is.EqualTo(expectedUserCount));
            Assert.That((result.Model as SearchResultViewModel).Topics.Count(), Is.EqualTo(expectedTopicCount));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Count(), Is.EqualTo(expectedQuestionCount));
        }

        [Test, Isolated]
        public void GetResult_WhenCalled_ShouldReturnSearchResultWithQuestionsAndAnswerCount()
        {
            var question1 = _context.AddTestQuestionToDatabase("Test search keyword question?");
            var question2 = _context.AddTestQuestionToDatabase("Another test search keyword question?");

            var answer = _context.AddTestAnswerToDatabase(question1.Id);

            _context.SaveChanges();

            var result = _controller.GetResult("key");

            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Count(), Is.EqualTo(2));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Single(kvp => kvp.Key.Id == question1.Id).Value, Is.EqualTo(1));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Single(kvp => kvp.Key.Id == question2.Id).Value, Is.EqualTo(0));
        }

        [Test, Isolated]
        [TestCase("user", 2)]
        [TestCase("user1", 1)]
        [TestCase("nouser", 0)]
        public void SearchFullResult_WhenTypeIsUser_ShouldReturnSearchResultWithUsers(string term, int expectedUserCount)
        {
            var result = _controller.SearchFullResult(term, nameof(SearchFullResultViewModel.User));

            Assert.That((result.Model as SearchUsersFullResultViewModel).Users.Count(), Is.EqualTo(expectedUserCount));
        }

        [Test, Isolated]
        [TestCase("So", 1)]
        [TestCase("so", 1)]
        [TestCase("top", 2)]
        [TestCase("Ano", 1)]
        [TestCase("que", 0)]
        public void SearchFullResult_WhenTypeIsTopic_ShouldReturnSearchResultWithTopics(string term, int expectedTopicCount)
        {
            var topic1 = _context.AddTestTopicToDatabase("Some Topic");
            var topic2 = _context.AddTestTopicToDatabase("Another Topic");

            var result = _controller.SearchFullResult(term, nameof(SearchFullResultViewModel.Topic));

            Assert.That((result.Model as SearchTopicsFullResultViewModel).Topics.Count(), Is.EqualTo(expectedTopicCount));
        }

        [Test, Isolated]
        [TestCase("So", false, true, 0)]
        [TestCase("so", false, true, 0)]
        [TestCase("top", false, true, 0)]
        [TestCase("Ano", false, true, 1)]
        [TestCase("que", false, false, 2)]
        [TestCase("see", false, false, 0)]
        [TestCase("user1", true, false, 0)]
        [TestCase("user", true, false, 0)]
        public void SearchFullResult_WhenTypeIsNull_ShouldReturnSearchFullResult(string term,
            bool expectedUserExists, bool expectedTopicExists, int expectedQuestionAnswerCount)
        {
            var topic1 = _context.AddTestTopicToDatabase("Some Topic");
            var topic2 = _context.AddTestTopicToDatabase("Another Topic");
            var question1 = _context.AddTestQuestionToDatabase("Test search keyword question?");
            var question2 = _context.AddTestQuestionToDatabase("Another test search keyword question?");
            var answer1 = _context.AddTestAnswerToDatabase(question1.Id);
            var answer2 = _context.AddTestAnswerToDatabase(question2.Id);

            _context.SaveChanges();

            var result = _controller.SearchFullResult(term);

            Assert.That((result.Model as SearchFullResultViewModel).User != null, Is.EqualTo(expectedUserExists));
            Assert.That((result.Model as SearchFullResultViewModel).Topic != null, Is.EqualTo(expectedTopicExists));
            Assert.That((result.Model as SearchFullResultViewModel).QuestionAnswers.Count(), Is.EqualTo(expectedQuestionAnswerCount));
        }

        [Test, Isolated]
        public void SearchFullResult_WhenTypeIsNull_ShouldReturnQuestionsWithAnswers()
        {
            var question1 = _context.AddTestQuestionToDatabase("Test search keyword question?");
            var question2 = _context.AddTestQuestionToDatabase("Another test search keyword question?");
            var answer1 = _context.AddTestAnswerToDatabase(question1.Id);

            _context.SaveChanges();

            var result = _controller.SearchFullResult("key");

            Assert.That((result.Model as SearchFullResultViewModel).QuestionAnswers.Count(), Is.EqualTo(1));
            Assert.That((result.Model as SearchFullResultViewModel).QuestionAnswers.Single().Key.Id, Is.EqualTo(question1.Id));
            Assert.That((result.Model as SearchFullResultViewModel).QuestionAnswers.Single().Value.Id, Is.EqualTo(answer1.Id));
        }

        [Test, Isolated]
        public void LoadMore_WhenTypeIsUser_ShouldReturnUsers()
        {
            for (var i = 0; i < Constants.DefaultPageSize; i++)
            {
                _context.AddTestUserToDatabase("FirstPageMatchedUserFirstName" + i, "User");
            }
            _context.AddTestUserToDatabase("FirstPageNotMatchedUser", "NotMatchedUser");

            var result = _controller.LoadMore(0, "user", nameof(SearchFullResultViewModel.User));

            Assert.That((result.Model as IEnumerable<AppUser>).Count(), Is.EqualTo(2));
            Assert.That((result.Model as IEnumerable<AppUser>).Any(u => u.Id == _firstUserInDb.Id), Is.True);
        }

        [Test, Isolated]
        public void LoadMore_WhenTypeIsTopic_ShouldReturnTopics()
        {
            var topic1 = _context.AddTestTopicToDatabase("Some Topic");
            var topic2 = _context.AddTestTopicToDatabase("Another Topic");

            for (var i = 0; i < Constants.DefaultPageSize; i++)
            {
                _context.AddTestTopicToDatabase("some topic " + i);
            }
            _context.AddTestUserToDatabase("some notMatchedTopic");

            var result = _controller.LoadMore(0, "topic", nameof(SearchFullResultViewModel.Topic));

            Assert.That((result.Model as IEnumerable<Topic>).Count(), Is.EqualTo(2));
            Assert.That((result.Model as IEnumerable<Topic>).Any(t => t.Id == topic1.Id), Is.True);
            Assert.That((result.Model as IEnumerable<Topic>).Any(t => t.Id == topic2.Id), Is.True);
        }

        [Test, Isolated]
        public void LoadMore_WhenTypeIsQuestionAnswers_ShouldReturnQuestionAnswers()
        {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            for (var i = 0; i < Constants.DefaultPageSize; i++)
            {
                var newQuestion = _context.AddTestQuestionToDatabase("First Page Questions" + i);
                _context.AddTestAnswerToDatabase(newQuestion.Id, "First Page Answers" + i);
            }

            _context.AddTestQuestionToDatabase("Question without Answers");

            var result = _controller.LoadMore(0, "question", nameof(SearchFullResultViewModel.QuestionAnswers));

            Assert.That((result.Model as IDictionary<Question, Answer>).Count(), Is.EqualTo(1));
            Assert.That((result.Model as IDictionary<Question, Answer>).Single().Key.Id, Is.EqualTo(question.Id));
            Assert.That((result.Model as IDictionary<Question, Answer>).Single().Value.Id, Is.EqualTo(answer.Id));
        }
    }
}
