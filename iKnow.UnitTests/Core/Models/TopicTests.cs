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
        public void TrimNameAndDescription_WhenCalled_ShouldTrimNameAndDesciption() {
            _topic.Name = " test topic ";
            _topic.Description = " test description ";

            _topic.TrimNameAndDescription();

            Assert.That(_topic.Name, Is.EqualTo("Test Topic"));
            Assert.That(_topic.Description, Is.EqualTo("Test description"));
        }

        [Test]
        public void TrimNameAndDescription_InputIsNull_ShouldSetNull() {
            _topic.Name = null;
            _topic.Description = null;

            _topic.TrimNameAndDescription();

            Assert.That(_topic.Name, Is.Null);
            Assert.That(_topic.Description, Is.Null);
        }

        [Test]
        public void UpdateNameAndDescription_WhenCalled_ShouldUpdateNameAndDescription() {
            _topic.UpdateNameAndDescription("New Name", "New desc");

            Assert.That(_topic.Name, Is.EqualTo("New Name"));
            Assert.That(_topic.Description, Is.EqualTo("New desc"));
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