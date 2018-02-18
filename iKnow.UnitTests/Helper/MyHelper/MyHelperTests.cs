using NUnit.Framework;

namespace iKnow.UnitTests.Helper.MyHelper {
    [TestFixture]
    public class MyHelperTests {
        [Test]
        [TestCase(null, null)]
        [TestCase("test", "Test")]
        [TestCase(" test ", " Test ")]
        [TestCase("test text", "Test Text")]
        [TestCase("test Text", "Test Text")]
        public void CapitalizeAllWords_WhenCalled_ReturnCapitalizedWords(string input, string expectedResult) {
            var result = iKnow.Helper.MyHelper.CapitalizeAllWords(input);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(null, null)]
        [TestCase("test", "Test")]
        [TestCase(" test ", " test ")]
        [TestCase("test text", "Test text")]
        [TestCase("test Text", "Test Text")]
        public void CapitalizeFirstWord_WhenCalled_ReturnStringWithCapitalizedFirstWord(string input, string expectedResult) {
            var result = iKnow.Helper.MyHelper.CapitalizeFirstWord(input);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

    }
}
