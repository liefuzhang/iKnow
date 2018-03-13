using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace iKnow.IntegrationTests.Extensions {
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
