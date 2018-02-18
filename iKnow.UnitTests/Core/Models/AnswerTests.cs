using iKnow.Core.Models;
using NUnit.Framework;

namespace iKnow.UnitTests.Core.Models {
    [TestFixture]
    public class AnswerTests {
        private Answer _answer;

        [SetUp]
        public void Setup() {
            _answer = new Answer();
        }

        [Test]
        [TestCase("", "")]
        [TestCase("<span>test text</span><div>test</div>",
            "test texttest")]
        [TestCase(" <span> test text</span> <div>test</div> ",
            "test text test")]
        public void PlainContent_WhenGet_RemoveTags(string content, string expectedResult) {
            _answer.Content = content;

            Assert.That(_answer.PlainContent, Is.EqualTo(expectedResult)); 
        }
        
        [Test]
        [TestCase("<h2>test text</h2><div>test</div>",
            "test text test")]
        [TestCase("<p>test text</p><div>test</div>",
            "test text test")]
        [TestCase("<ul><li>test text1</li><li>test text2</li><div>test</div>",
            "test text1 test text2 test")]
        [TestCase("<blockquote>test text</blockquote><div>test</div>",
            "test text test")]
        public void PlainContent_WhenGet_AddSpaceAfterRemovingSomeOfTheClosingTags(string content, string expectedResult) {
            _answer.Content = content;

            Assert.That(_answer.PlainContent, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ShortContent_PlainContentExceedsMaxLength_TruncateContentAndAddEllipsis() {
            _answer.Content = new string('a', Constants.ShortAnswerLength + 1);

            Assert.That(_answer.ShortContent, Is.EqualTo(new string('a', Constants.ShortAnswerLength) + "..."));
        }

        [Test]
        public void ShortContent_PlainContentDoesNotExceedsMaxLength_ReturnPlainContent() {
            _answer.Content = new string('a', Constants.ShortAnswerLength);

            Assert.That(_answer.ShortContent, Is.EqualTo(new string('a', Constants.ShortAnswerLength)));
        }

        [Test]
        public void ShortContentImageData_ContentContainsImage_ReturnImageData() {
            _answer.Content = "<div>test content <img src=\"testimagesrc\"></div>";

            Assert.That(_answer.ShortContentImageData, Is.EqualTo("<img src=\"testimagesrc\">"));
        }

        [Test]
        public void ShortContentImageData_ContentDoesNotContainImage_ReturnNull() {
            _answer.Content = "<div>test content</div>";

            Assert.That(_answer.ShortContentImageData, Is.Null);
        }
    }
}
