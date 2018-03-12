using System.Collections.Generic;
using System.Linq;
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
            _question = new Question {Title = "Title?"};
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

            var result = new Question().CanUserModify(mockUser.Object);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUserModify_UserIsInAdminRoleButQuestionIsNew_ReturnFalse() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");
            mockUser.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            var result = new Question().CanUserModify(mockUser.Object);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanUserModify_UserIsQuestionOwner_ReturnTrue() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");

            var question = new Question();
            question.SetUserId("1");

            var result = question.CanUserModify(mockUser.Object);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanUserModify_UserIsInAdminRoleAndQuestionIsNotNew_ReturnTrue() {
            var mockUser = new Mock<IPrincipal>();
            mockUser.MockIdentity("1");
            mockUser.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            var result = new Question { Id = 1 }.CanUserModify(mockUser.Object);

            Assert.That(result, Is.True);
        }

        [Test]
        public void TrimTitleAndDescription_WhenCalled_ShouldTrimTitleAndDesciption() {
            _question.Title = " test question ";
            _question.Description = " test description ";

            _question.TrimTitleAndDescription();

            Assert.That(_question.Title, Is.EqualTo("Test question?"));
            Assert.That(_question.Description, Is.EqualTo("Test description"));
        }

        [Test]
        public void TrimTitleAndDescription_InputIsNull_ShouldSetNull() {
            _question.Title = null;
            _question.Description = null;

            _question.TrimTitleAndDescription();

            Assert.That(_question.Title, Is.Null);
            Assert.That(_question.Description, Is.Null);
        }

        [Test]
        public void UpdateTitleAndDescription_WhenCalled_ShouldUpdateTitleAndDescription() {
            _question.UpdateTitleAndDescription("New Title?", "New desc");

            Assert.That(_question.Title, Is.EqualTo("New Title?"));
            Assert.That(_question.Description, Is.EqualTo("New desc"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void UpdateTitleAndDescription_TitleIsNullOrWhiteSpace_ShouldNotUpdateTitleAndDescription(string title) {
            _question.UpdateTitleAndDescription(title, "New desc");

            Assert.That(_question.Title, Is.Not.EqualTo(title));
            Assert.That(_question.Description, Is.Not.EqualTo("New desc"));
        }

        [Test]
        public void UpdateQuestionTopics_TopicIsNull_ShouldNotClearTopics() {
            _question.Topics.Add(new Topic());

            _question.UpdateQuestionTopics(null);

            Assert.That(_question.Topics.Count, Is.EqualTo(1));
        }

        [Test]
        public void UpdateQuestionTopics_TopicsIsNotEmpty_ShouldReplaceTopics() {
            _question.Topics.Add(new Topic());

            _question.UpdateQuestionTopics(new List<Topic> { new Topic { Id = 1 } });

            Assert.That(_question.Topics.Count, Is.EqualTo(1));
            Assert.That(_question.Topics.First().Id, Is.EqualTo(1));
        }
    }
}