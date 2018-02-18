using System;
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