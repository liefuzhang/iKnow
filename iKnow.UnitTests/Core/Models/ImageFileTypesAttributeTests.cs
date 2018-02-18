using System.Web;
using iKnow.Core.Models;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Core.Models {
    [TestFixture]
    public class ImageFileTypesAttributeTest {
        private ImageFileTypesAttribute _imageFileTypesAttribute;
        private Mock<HttpPostedFileBase> _httpPostedFile;
        private string _fileName = "test.png";

        [SetUp]
        public void Setup() {
            _httpPostedFile = new Mock<HttpPostedFileBase>();
            _httpPostedFile.Setup(hpf => hpf.FileName)
                .Returns(() => _fileName);

            _imageFileTypesAttribute = new ImageFileTypesAttribute("png,jpg");
        }
        
        [Test]
        [TestCase("test.png", true)]
        [TestCase("test.jpg", true)]
        [TestCase("test.JPG", true)]
        [TestCase("test.txt", false)]
        public void IsValid_WhenCalled_ValidateValue(string fileName, bool expectedResult) {
            _fileName = fileName;

            var result = _imageFileTypesAttribute.IsValid(_httpPostedFile.Object);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void IsValid_ValueIsNull_ReturnTrue() {
            var result = _imageFileTypesAttribute.IsValid(null);

            Assert.That(result, Is.True);
        }

        [Test]
        public void FormatErrormessage_WhenCalled_ReturnErrorMessage() {
            var result = _imageFileTypesAttribute.FormatErrorMessage("test");

            Assert.That(result, Does.Contain("invalid file type").IgnoreCase);
        }
    }
}