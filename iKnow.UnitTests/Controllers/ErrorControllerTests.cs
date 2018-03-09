using System;
using System.Collections.Generic;
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
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class ErrorControllerTests {
        private ErrorController _controller;
        private Mock<HttpResponseBase> _response;

        [SetUp]
        public void Setup() {
            var context = new Mock<HttpContextBase>();
            _response = new Mock<HttpResponseBase>();
            _response.SetupProperty(r => r.StatusCode);
            context.SetupGet(x => x.Response).Returns(_response.Object);

            _controller = new ErrorController();
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        [Test]
        public void Index_WhenCalled_ReturnErrorView() {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).ViewName, Is.EqualTo("Error"));
        }

        [Test]
        public void NotFound_WhenCalled_Return404View() {
            var result = _controller.NotFound();

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).ViewName, Is.EqualTo("404"));
        }

        [Test]
        public void NotFound_WhenCalled_SetResponseStatusCodeTo200() {
            var result = _controller.NotFound();

            Assert.That(_controller.Response.StatusCode, Is.EqualTo(200));
        }
    }
}
