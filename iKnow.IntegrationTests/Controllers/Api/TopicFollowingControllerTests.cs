using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using iKnow.Controllers;
using iKnow.Controllers.Api;
using iKnow.Core.Models;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers.Api {
    [TestFixture]
    public class TopicFollowingControllerTests {
        private TopicFollowingController _controller;
        private iKnowContext _context;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new TopicFollowingController(new UnitOfWork(_context));

            _currentUser = new Mock<IPrincipal>();
            _controller.User = _currentUser.Object;
            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }
   
        [Test, Isolated]
        public void Follow_WhenCalled_ShouldSaveFollowingToDatabase() {
            var topic = _context.AddTestTopicToDatabase();

            var result = _controller.Follow(topic.Id);

            Assert.That(_context.TopicFollowings.Count(), Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Follow_WhenCalled_ShouldSaveFollowTopicActivityToDatabase() {
            var topic = _context.AddTestTopicToDatabase();

            var result = _controller.Follow(topic.Id);

            var activity = _context.Activities.Single();

            Assert.That(activity.Type, Is.EqualTo(ActivityType.FollowTopic));
            Assert.That(activity.TopicId, Is.EqualTo(topic.Id));
        }

        [Test, Isolated]
        public void Unfollow_WhenCalled_ShouldRemoveFollowingFromDatabase() {
            var topic = _context.AddTestTopicToDatabase();
            var following = _context.AddTestTopicFollowingToDatabase(topic.Id);

            var result = _controller.Unfollow(topic.Id);

            Assert.That(_context.TopicFollowings.Count(), Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Unfollow_WhenCalled_ShouldRemoveFollowTopicActivityFromDatabase()
        {
            var topic = _context.AddTestTopicToDatabase();
            var following = _context.AddTestTopicFollowingToDatabase(topic.Id);
            _context.AddTestActivityTopicFollowingToDatabase(topic.Id);

            var result = _controller.Unfollow(topic.Id);

            Assert.That(_context.Activities.Count(), Is.EqualTo(0));
        }
    }
}
