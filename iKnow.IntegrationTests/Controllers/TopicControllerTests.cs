using iKnow.Controllers;
using iKnow.Persistence;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class TopicControllerTests {
        private TopicController _controller;
        private iKnowContext _context;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new TopicController(new UnitOfWork(_context), new FileHelper());
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test]
        public void Cancel_WhenCalled_ShouldCancelTheGivenGigs() {
        }
    }
}
