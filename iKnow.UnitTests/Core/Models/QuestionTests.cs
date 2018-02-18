using iKnow.Core.Models;
using NUnit.Framework;

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
    }
}