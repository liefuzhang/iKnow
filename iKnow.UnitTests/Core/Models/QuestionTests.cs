using System.Security.Principal;
using iKnow.Core.Models;
using iKnow.UnitTests.Extensions;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.UnitTests.Core.Models {
    [TestFixture]
    public class QuestionTests {
        private Question _question;

        [SetUp]
        public void Setup() {
            _question = new Question();
        }

        [Test]
        public void AddTopic_WhenCalled_AddTopicToTopics() {
            var topic = new Topic();

            _question.AddTopic(topic);

            Assert.That(_question.Topics, Does.Contain(topic));
        }

        [Test]
        public void ClearTopic_WhenCalled_ClearTopics() {
            var topic = new Topic();

            _question.AddTopic(topic);

            _question.ClearTopics();

            Assert.That(_question.Topics.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanUserModify_UserIsNotAuthenticated_ReturnFalse() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.SetupGet(u => u.Identity.IsAuthenticated).Returns(false);

            var result = new Question().CanUserModify(mockUser.Object);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUserModify_UserIsAuthenticatedButNotQuestionOwner_ReturnFalse() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");

            var result = new Question { AppUserId = "2" }.CanUserModify(mockUser.Object);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUserModify_UserIsInAdminRoleButQuestionIsNew_ReturnFalse() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");
            mockUser.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            var result = new Question { AppUserId = "2" }.CanUserModify(mockUser.Object);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUserModify_UserIsQuestionOwner_ReturnTrue() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");

            var result = new Question { AppUserId = "1" }.CanUserModify(mockUser.Object);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanUserModify_UserIsInAdminRoleAndQuestionIsNotNew_ReturnTrue() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");
            mockUser.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            var result = new Question { AppUserId = "2", Id = 1 }.CanUserModify(mockUser.Object);

            Assert.That(result, Is.True);
        }

    }
}