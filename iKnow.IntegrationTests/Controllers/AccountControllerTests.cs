using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core.Models;
using iKnow.Core.Models.Identity;
using iKnow.Core.ViewModels;
using iKnow.Core.ViewModels.Account;
using iKnow.Helper;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class AccountControllerTests {
        private AccountController _controller;
        private iKnowContext _context;
        private iKnowContext _contextAfterAction;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;
        private Mock<AppUserManager> _userManager;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _contextAfterAction = new iKnowContext();
            _currentUser = new Mock<IPrincipal>();
            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);

            var userStore = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<AppUserManager>(userStore.Object);
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(_firstUserInDb));

            _controller = new AccountController(new UnitOfWork(new iKnowContext()), new EmailSender(), 
                _userManager.Object, null, null);

            _controller.MockContext(new Mock<HttpRequestBase>(), _currentUser);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
            _contextAfterAction.Dispose();
        }

        [Test, Isolated]
        public async Task UserProfile_WhenCalled_ShouldReturnActivitiesInViewModel() {
            var topic = _context.AddTestTopicToDatabase();
            _context.AddTestActivityTopicFollowingToDatabase(topic.Id);

            var result = await _controller.UserProfile(_firstUserInDb.UserName);

            Assert.That(((result as ViewResult).Model as UserProfileViewModel).Activities.Count(), Is.EqualTo(1));
        }

        [Test, Isolated]
        public async Task LoadMore_WhenCalled_ShouldReturnActivitiesInViewModel() {
            var topic = _context.AddTestTopicToDatabase();
            var activity = _context.AddTestActivityTopicFollowingToDatabase(topic.Id);

            for (var i = 0; i < Constants.DefaultPageSize; i++) {
                var moreTopic = _context.AddTestTopicToDatabase();
                _context.AddTestActivityTopicFollowingToDatabase(moreTopic.Id);
            }

            var result = await _controller.LoadMore(0, _firstUserInDb.UserName);

            var activities = result.Model as IEnumerable<Activity>;
            Assert.That(activities.Count(), Is.EqualTo(1));
            Assert.That(activities.ToArray()[0].Id, Is.EqualTo(activity.Id));
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
