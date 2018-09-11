using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using iKnow.Controllers;
using iKnow.Controllers.Api;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers.Api {
    [TestFixture]
    public class AnswerFooterControllerTests
    {
        private AnswerFooterController _controller;
        private iKnowContext _context;
        private iKnowContext _contextAfterAction;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new AnswerFooterController(new UnitOfWork(_context));

            _currentUser = new Mock<IPrincipal>();
            _controller.User = _currentUser.Object;
            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);

            _contextAfterAction = new iKnowContext();
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void PostComment_WhenCalled_ShouldSaveCommentToDatabase()
        {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);
            var viewModel = new AnswerPostCommentViewModel(answer.Id, "Comment");

            var result = _controller.PostComment(viewModel);

            var comment = _contextAfterAction.Comments.Single();

            Assert.That(comment.AnswerId, Is.EqualTo(answer.Id));
            Assert.That(comment.Content, Is.EqualTo("Comment"));
            Assert.That((result as OkNegotiatedContentResult<int>).Content, Is.EqualTo(1));
        }
    }
}
