using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace iKnow.Models.Identity {
    public class EmailService : IIdentityMessageService {
        public Task SendAsync(IdentityMessage message) {
            var text = $"Please click on this link to {message.Subject}: {message.Body}";
            var html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click to copy the following link on the browser:" + message.Body);

            var msg = new MailMessage();
            msg.From = new MailAddress("joe@contoso.com");
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            var smtpClient = new SmtpClient();
            return smtpClient.SendMailAsync(msg);
        }
    }
}