using System.Security.Claims;
using System.Security.Principal;
using iKnow.Core.Models;
using Moq;

namespace iKnow.IntegrationTests.Extensions {
    public static class UserExtensions {
        public static Mock<ClaimsIdentity> MockIdentity(this Mock<IPrincipal> user, string userId) {
            var claim = new Claim("testUserName", userId);
            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            identity.Setup(i => i.IsAuthenticated).Returns(true);

            user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(false);
            user.SetupGet(u => u.Identity).Returns(identity.Object);

            return identity;
        }
    }
}
