using System.Threading.Tasks;
using iKnow.Core.Models;

namespace iKnow.Core {
    public interface IEmailSender {
        Task SendForgotPasswordMailAsync(AppUser user, string callbackUrl);
    }
}