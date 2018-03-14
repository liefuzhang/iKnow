using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Moq;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class HomeControllerTests {
        private HomeController _controller;
        private iKnowContext _context;
        private Mock<IPrincipal> _user;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new HomeController(new UnitOfWork(_context));

            _user = new Mock<IPrincipal>();
            _controller.MockContext(new Mock<HttpRequestBase>(), _user);

            var user1 = _context.Users.First();
            _user.MockIdentity(user1.Id);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnQuestionAnswerPairsInViewModel() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            var result = _controller.Index();

            var homeViewModel = (result as ViewResult).Model as HomeViewModel;
            Assert.That(homeViewModel.QuestionAnswers.Count, Is.EqualTo(1));
            Assert.That(homeViewModel.QuestionAnswers.Keys.First().Id, Is.EqualTo(question.Id));
            Assert.That(homeViewModel.QuestionAnswers.Values.First().Id, Is.EqualTo(answer.Id));
        }

        [Test, Isolated]
        public void LoadMore_WhenCalled_ShouldReturnQuestionAnswerPairsInViewModel() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            for (var i = 0; i < Constants.DefaultPageSize; i++)
                _context.AddTestQuestionToDatabase("First Page Questions" + i);

            var x = _context.Questions.Count();

            var result = _controller.LoadMore(0);

            var dictionary = result.Model as IDictionary<Question,Answer>;
            Assert.That(dictionary.Count, Is.EqualTo(1));
            Assert.That(dictionary.Keys.First().Id, Is.EqualTo(question.Id));
            Assert.That(dictionary.Values.First().Id, Is.EqualTo(answer.Id));
        }

        [Test, Isolated]
        public void GetUserProfile_WhenCalled_ReturnCurrentUserViewModel() {
            var user = _context.Users.First();

            var result = _controller.GetUserProfile();

            Assert.That((result.Model as AppUser).Id, Is.EqualTo(user.Id));
        }
    }
}
