using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iKnow.Controllers;
using iKnow.Core.Models;
using Moq;

namespace iKnow.UnitTests.Extensions {
    public static class ControllerExtensions {
        public static void MockContext(this Controller controller, Mock<HttpRequestBase> request, Mock<IPrincipal> user = null) {
            request.SetupGet(r => r.UrlReferrer).Returns(new Uri("http://test.com"));
            request.SetupGet(r => r.Url).Returns(new Uri("http://test.com"));

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            if (user != null)
                context.SetupGet(x => x.User).Returns(user.Object);

            controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), controller);
        }
    }
}
