﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using iKnow.ViewModels;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Core.Models {
    [TestFixture]
    public class AppUserTests {
        private AppUser _appUser;
        private Mock<IFileHelper> _fileHelper;
        private Mock<HttpRequestBase> _httpRequestBase;

        [SetUp]
        public void Setup() {
            _fileHelper = new Mock<IFileHelper>();
            _httpRequestBase = new Mock<HttpRequestBase>();

            _httpRequestBase.Setup(r => r.IsSecureConnection)
                .Returns(false);
            _httpRequestBase.Setup(r => r.Url)
                .Returns(new Uri("http://tempuri.com"));
            
            _appUser = new AppUser(_fileHelper.Object, _httpRequestBase.Object) {
                FirstName = "Test",
                LastName = "User",
                UserName = "testuser0",
                Id = "testid",
                DefaultIconNumber = 1
            };
        }

        [Test]
        public void FullName_WhenGet_ReturnFullName() {
            var fullName = _appUser.FullName;

            Assert.That(fullName, Is.EqualTo("Test User"));
        }

        [Test]
        public void IconPath_FileExists_ReturnFilePath() {
            _fileHelper.Setup(f => f.DoesFileExist(It.IsAny<string>()))
                .Returns(true);

            var result = _appUser.IconPath;

            Assert.That(result, Is.EqualTo(Constants.UserIconFolderPath + _appUser.Id + Constants.DefaultIconExtension));
        }

        [Test]
        public void IconPath_FileDoesNotExist_ReturnDefaultFilePath() {
            _fileHelper.Setup(f => f.DoesFileExist(It.IsAny<string>()))
                .Returns(false);

            var result = _appUser.IconPath;

            Assert.That(result, Is.EqualTo(Constants.UserIconFolderPath
                + Constants.UserDefaultIconName + _appUser.DefaultIconNumber
                + Constants.DefaultIconExtension));
        }

        [Test]
        public void ProfilePageUrl_HttpRequestIsNotSecureConnection_ReturnHttpUrl() {
            var result = _appUser.ProfilePageUrl;

            Assert.That(result, Is.EqualTo($"http://tempuri.com/Account/UserProfile/{_appUser.UserName}"));
        }

        [Test]
        public void ProfilePageUrl_HttpRequestIsSecureConnection_ReturnHttpsUrl() {
            _httpRequestBase.Setup(r => r.IsSecureConnection)
                .Returns(true);

            var result = _appUser.ProfilePageUrl;

            Assert.That(result, Is.EqualTo($"https://tempuri.com/Account/UserProfile/{_appUser.UserName}"));
        }

    }
}
