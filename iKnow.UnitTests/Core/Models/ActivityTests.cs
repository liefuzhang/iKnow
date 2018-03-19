using System;
using iKnow.Core;
using iKnow.Core.Models;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Core.Models {
    [TestFixture]
    public class ActivityTests {
        [Test]
        public void ActviityFollowTopic_WhenCalled_ShouldReturnFollowTopicActivity() {
            var result = Activity.ActivityFollowTopic("1", 1);

            Assert.That(result.Type, Is.EqualTo(ActivityType.FollowTopic));
        }

        [Test]
        public void ActviityFollowTopic_TopicIdIsZeroOrNegativeOrUserIdIsNull_ShouldThrowArgumentException() {
            Assert.Throws<ArgumentException>(() => Activity.ActivityFollowTopic("1", 0));
            Assert.Throws<ArgumentException>(() => Activity.ActivityFollowTopic("1", -1));
            Assert.Throws<ArgumentNullException>(() => Activity.ActivityFollowTopic(null, 1));
        }

        [Test]
        public void ActivityAnswerQuestion_questionIdOrAnswerIdIsZeroOrNegativeOrUserIdIsNull_ShouldThrowArgumentException() {
            Assert.Throws<ArgumentException>(() => Activity.ActivityAnswerQuestion("1", 0, 1));
            Assert.Throws<ArgumentException>(() => Activity.ActivityAnswerQuestion("1", -1, 1));
            Assert.Throws<ArgumentException>(() => Activity.ActivityAnswerQuestion("1", 1, 0));
            Assert.Throws<ArgumentException>(() => Activity.ActivityAnswerQuestion("1", 1, -1));
            Assert.Throws<ArgumentNullException>(() => Activity.ActivityAnswerQuestion(null, 1, 1));
        }

        [Test]
        public void ActviityAddQuestion_QuestionIdIsZeroOrNegativeOrUserIdIsNull_ShouldThrowArgumentException() {
            Assert.Throws<ArgumentException>(() => Activity.ActivityAddQuestion("1", 0));
            Assert.Throws<ArgumentException>(() => Activity.ActivityAddQuestion("1", -1));
            Assert.Throws<ArgumentNullException>(() => Activity.ActivityFollowTopic(null, 1));
        }
    }
}