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

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class ActivityControllerTests {
        private ActivityController _controller;
        private iKnowContext _context;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new ActivityController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void GetFollowTopic_WhenCalled_ShouldReturnActivityTopicInViewModel() {
            var topic = _context.AddTestTopicToDatabase();
            var activity = _context.AddTestActivityTopicFollowingToDatabase(topic.Id);

            var result = _controller.GetFollowTopic(activity.Id);

            Assert.That((result.Model as ActivityViewModel).Topic.Id, Is.EqualTo(topic.Id));
        }

        [Test, Isolated]
        public void GetAnswerQuestion_WhenCalled_ShouldReturnActivityQuestionAndAnswerInViewModel() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);
            var activity = _context.AddTestActivityAnswerQuestionToDatabase(question.Id, answer.Id);

            var result = _controller.GetAnswerQuestion(activity.Id);

            Assert.That((result.Model as ActivityViewModel).Question.Id, Is.EqualTo(question.Id));
            Assert.That((result.Model as ActivityViewModel).Answer.Id, Is.EqualTo(answer.Id));
        }

        [Test, Isolated]
        public void GetAddQuestion_WhenCalled_ShouldReturnActivityQuestionInViewModel() {
            var question = _context.AddTestQuestionToDatabase();
            var activity = _context.AddTestActivityAddQuestionToDatabase(question.Id);

            var result = _controller.GetAddQuestion(activity.Id);

            Assert.That((result.Model as ActivityViewModel).Question.Id, Is.EqualTo(question.Id));
        }
    }
}
