using System;
using System.Linq;
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
    public class TopicControllerTests {
        private TopicController _controller;
        private iKnowContext _context;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new TopicController(new UnitOfWork(_context), new FileHelper());
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnTopicsInViewModel() {
            var count = _context.Topics.Count();
            _controller.MockContext(new Mock<HttpRequestBase>());

            var result = _controller.Index();

            Assert.That((result.Model as TopicIndexViewModel).Topics.Count(), Is.EqualTo(count));
        }

        [Test, Isolated]
        public void Detail_WhenCalled_ShouldReturnTopicWithQuestionAnswerPairsInViewModel() {
            _controller.MockContext(new Mock<HttpRequestBase>());
            var user = _context.Users.First();

            var topic = new Topic {
                Id = 1,
                Name = "testTopic"
            };
            var question = new Question {
                Id = 1,
                Title = "Test Question?"
            };
            var answer = new Answer {
                Id = 1,
                QuestionId = 1,
                Content = "test answer",
                AppUserId = user.Id,
                CreatedDate = DateTime.Now
            };
            question.SetUserId(user.Id);
            question.AddTopic(topic);
            
            _context.Questions.Add(question);
            _context.Answers.Add(answer);
            _context.SaveChanges();

            var result = _controller.Detail(topic.Id);

            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).Topic.Id, Is.EqualTo(topic.Id));
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).QuestionAnswers.Count, Is.EqualTo(1));
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).QuestionAnswers.Keys.First().Id, Is.EqualTo(question.Id));
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).QuestionAnswers.Values.First().Id, Is.EqualTo(answer.Id));
        }

        [Test]
        public void About_WhenCalled_ShouldReturnTopicInViewModel() {

        }
    }
}
