using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Core.ViewModels.Account;
using iKnow.Helper;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Moq;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class AccountControllerTests {
        private AccountController _controller;
        private iKnowContext _context;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new AccountController(new UnitOfWork(_context), new EmailSender(), new FileHelper());

            _currentUser = new Mock<IPrincipal>();
            _controller.MockContext(new Mock<HttpRequestBase>(), _currentUser);

            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void SaveProfile_WhenCalled_UpdateUserProfile() {
            _firstUserInDb.Gender = Gender.Male;
            _context.SaveChanges();

            var appUser = new AppUser {
                Gender = Gender.Female,
                Location = _firstUserInDb.Location + "-",
                Intro = _firstUserInDb.Intro + "-",
                Id = _firstUserInDb.Id
            };
            var viewModel = new UserProfileViewModel {
                AppUser = appUser
            };

            _controller.SaveProfile(viewModel);

            _context.Entry(_firstUserInDb).Reload();

            Assert.That(_firstUserInDb.Gender, Is.EqualTo(appUser.Gender));
            Assert.That(_firstUserInDb.Location, Is.EqualTo(appUser.Location));
            Assert.That(_firstUserInDb.Intro, Is.EqualTo(appUser.Intro));
        }
    }
}
