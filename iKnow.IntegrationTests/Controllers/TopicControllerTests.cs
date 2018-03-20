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

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class TopicControllerTests {
        private TopicController _controller;
        private iKnowContext _context;
        private iKnowContext _contextAfterAction;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _contextAfterAction = new iKnowContext();
            _controller = new TopicController(new UnitOfWork(new iKnowContext()), new FileHelper());

            _currentUser = new Mock<IPrincipal>();
            _controller.MockContext(new Mock<HttpRequestBase>(), _currentUser);

            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
            _contextAfterAction.Dispose();
        }

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnTopicsInViewModel() {
            _context.AddTestTopicToDatabase();

            var result = _controller.Index();

            Assert.That((result.Model as TopicIndexViewModel).Topics.Count(), Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Detail_WhenCalled_ShouldReturnTopicWithQuestionAnswerPairsInViewModel() {
            var topic = _context.AddTestTopicToDatabase();

            var question = _context.AddTestQuestionToDatabase();
            question.AddTopic(topic);

            var answer = _context.AddTestAnswerToDatabase(question.Id);

            _context.SaveChanges();

            var result = _controller.Detail(topic.Id);

            var topicDetailViewModel = (result as ViewResult).Model as TopicDetailViewModel;
            Assert.That(topicDetailViewModel.Topic.Id, Is.EqualTo(topic.Id));
            Assert.That(topicDetailViewModel.QuestionAnswers.Count, Is.EqualTo(1));
            Assert.That(topicDetailViewModel.QuestionAnswers.Keys.First().Id, Is.EqualTo(question.Id));
            Assert.That(topicDetailViewModel.QuestionAnswers.Values.First().Id, Is.EqualTo(answer.Id));
            Assert.That(topicDetailViewModel.IsFollowing, Is.True);
        }

        [Test, Isolated]
        public void LoadMore_WhenCalled_ShouldReturnTopicWithQuestionAnswerPairsInViewModel() {
            var topic = _context.AddTestTopicToDatabase();

            var question = _context.AddTestQuestionToDatabase();
            question.AddTopic(topic);

            var answer = _context.AddTestAnswerToDatabase(question.Id);

            for (var i = 0; i < Constants.DefaultPageSize; i++) {
                var moreQuestion = _context.AddTestQuestionToDatabase();
                moreQuestion.AddTopic(topic);
                _context.AddTestAnswerToDatabase(moreQuestion.Id);
            }

            _context.SaveChanges();

            var result = _controller.LoadMore(0, topic.Id);

            var pairs = result.Model as IDictionary<Question, Answer>;
            Assert.That(pairs.Count, Is.EqualTo(1));
            Assert.That(pairs.Keys.First().Id, Is.EqualTo(question.Id));
            Assert.That(pairs.Values.First().Id, Is.EqualTo(answer.Id));
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
                Name = "Test Topic",
                Description = "Test desc"
            };

            var viewModel = new TopicFormViewModel {
                Topic = topic
            };

            _controller.Save(viewModel);

            var topicFromDb = _contextAfterAction.Topics.SingleOrDefault(t => t.Name == topic.Name);

            Assert.That(topicFromDb.Id, Is.Not.Null);
            Assert.That(topicFromDb.Name, Is.EqualTo(topic.Name));
            Assert.That(topicFromDb.Description, Is.EqualTo(topic.Description));
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

            var topicFromDb = _contextAfterAction.Topics.Where(t => t.Name == topic.Name);

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

            var topicFromDb = _contextAfterAction.Topics.Find(topic.Id);

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

            var topicInDb = _contextAfterAction.Topics.Find(topic.Id);
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
