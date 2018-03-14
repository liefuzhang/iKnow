using System;
using System.Collections.Generic;
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

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnTopicsInViewModel() {
            _controller.MockContext(new Mock<HttpRequestBase>());

            _context.AddTestTopicToDatabase();

            var result = _controller.Index();

            Assert.That((result.Model as TopicIndexViewModel).Topics.Count(), Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Detail_WhenCalled_ShouldReturnTopicWithQuestionAnswerPairsInViewModel() {
            _controller.MockContext(new Mock<HttpRequestBase>());

            var topic = _context.AddTestTopicToDatabase();
            var question = _context.AddTestQuestionToDatabase();
            question.AddTopic(topic);
            
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            _context.SaveChanges();

            var result = _controller.Detail(topic.Id);

            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).Topic.Id, Is.EqualTo(topic.Id));
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).QuestionAnswers.Count, Is.EqualTo(1));
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).QuestionAnswers.Keys.First().Id, Is.EqualTo(question.Id));
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).QuestionAnswers.Values.First().Id, Is.EqualTo(answer.Id));
        }

        [Test, Isolated]
        public void About_WhenCalled_ShouldReturnTopicInViewModel() {
            var topic = _context.AddTestTopicToDatabase();

            var result = _controller.About(topic.Id);

            Assert.That((result.Model as Topic).Id, Is.EqualTo(topic.Id));
        }

        [Test, Isolated]
        public void Save_NewTopic_ShouldAddToDatabase() {
            var topic = new Topic {
                Name = "Test Topic"
            };

            var viewModel = new TopicFormViewModel {
                Topic = topic
            };

            _controller.Save(viewModel);

            var topicFromDb = _context.Topics.SingleOrDefault(t => t.Name == topic.Name);

            Assert.That(topicFromDb.Id, Is.Not.Null);
        }

        [Test, Isolated]
        public void Save_NewTopicWithExistingTopicName_ShouldNotAddToDatabase() {
            var topic = _context.AddTestTopicToDatabase();

            var newTopic = new Topic {
                Name = topic.Name,
                Description = topic.Description + "-"
            };

            var viewModel = new TopicFormViewModel {
                Topic = newTopic
            };

            _controller.Save(viewModel);

            var topicFromDb = _context.Topics.Where(t => t.Name == topic.Name);

            Assert.That(topicFromDb.Count(), Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Save_ExistingTopic_ShouldUpdateTopicInDatabase() {
            var topic = _context.AddTestTopicToDatabase();

            var viewModel = new TopicFormViewModel {
                Topic = new Topic {
                    Name = "Test Topic",
                    Description = "Test desc",
                    Id = topic.Id
                }
            };

            _controller.Save(viewModel);

            var topicFromDb = _context.Topics.Find(topic.Id);

            Assert.That(topicFromDb.Name, Is.EqualTo("Test Topic"));
            Assert.That(topicFromDb.Description, Is.EqualTo("Test desc"));
        }

        [Test, Isolated]
        public void Edit_WhenCalled_ReturnTopicInViewModel() {
            var topic = _context.AddTestTopicToDatabase();

            var result = _controller.Edit(topic.Id);

            Assert.That(((result as ViewResult).Model as TopicFormViewModel).Topic.Id, Is.EqualTo(topic.Id));
        }

        [Test, Isolated]
        public void Delete_WhenCalled_RemoveGivenTopicFromDatabase() {
            var topic = _context.AddTestTopicToDatabase();

            var postedTopic = new Topic {
                Id = topic.Id,
                Name = topic.Name
            };

            _controller.Delete(postedTopic);

            var topicInDb = _context.Topics.Find(topic.Id);
            Assert.That(topicInDb, Is.Null);
        }

        [Test, Isolated]
        public void GetRecommendedTopics_IdIsNull_ReturnRecommendedTopics() {
            for (int i = 0; i <= Constants.RecommendedTopicNumber; i++)
                _context.AddTestTopicToDatabase(i.ToString());

            var result = _controller.GetRecommendedTopics(null);

            Assert.That((result.Model as IEnumerable<Topic>).Count(), Is.EqualTo(Constants.RecommendedTopicNumber));
        }

        [Test, Isolated]
        public void GetRecommendedTopics_IdIsNotNull_ReturnRecommendedTopicsExcludingTopicWithGivenId() {
            var topic1 = _context.AddTestTopicToDatabase();
            var topic2 = _context.AddTestTopicToDatabase();

            var result = _controller.GetRecommendedTopics(topic1.Id);

            Assert.That((result.Model as IEnumerable<Topic>).Count(), Is.EqualTo(1));
            Assert.That((result.Model as IEnumerable<Topic>).First().Id, Is.EqualTo(topic2.Id));
        }
    }
}
