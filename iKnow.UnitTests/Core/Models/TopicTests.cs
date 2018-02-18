using iKnow.Core;
using iKnow.Core.Models;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Core.Models {
    [TestFixture]
    public class TopicTests {
        private Topic _topic;
        private Mock<IFileHelper> _fileHelper;

        [SetUp]
        public void Setup() {
            _fileHelper = new Mock<IFileHelper>();

            _topic = new Topic(_fileHelper.Object) {
                Name = "Test topic",
                Id = 1
            };
        }

        [Test]
        public void IconPath_FileExists_ReturnFilePath() {
            _fileHelper.Setup(f => f.DoesFileExist(It.IsAny<string>()))
                .Returns(true);

            var result = _topic.IconPath;

            Assert.That(result, Is.EqualTo(Constants.TopicIconFolderPath
                + "test-topic" + Constants.DefaultIconExtension));
        }

        [Test]
        public void IconPath_FileDoesNotExist_ReturnDefaultFilePath() {
            _fileHelper.Setup(f => f.DoesFileExist(It.IsAny<string>()))
                .Returns(false);

            var result = _topic.IconPath;

            Assert.That(result, Is.EqualTo(Constants.TopicDefaultIconPath));
        }

        [Test]
        public void IconPath_IdIsZero_ReturnEmptyString() {
            _topic.Id = 0;

            var result = _topic.IconPath;

            Assert.That(result, Is.Empty);
        }
    }
}