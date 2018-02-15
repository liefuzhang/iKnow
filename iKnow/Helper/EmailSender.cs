using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using iKnow.Core.Models;
using iKnow.Core.Models.Identity;
using Microsoft.Owin.Security;

namespace iKnow.Helper {
    public interface IEmailSender {
        Task SendForgotPasswordMailAsync(AppUser user, string callbackUrl);
    }

    public class EmailSender : IEmailSender {
        public async Task SendForgotPasswordMailAsync(AppUser user, string callbackUrl) {
            var body = ConstructEmailBody(user, callbackUrl);

            using (MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["GmailUserName"], user.Email)) {
                mailMessage.Subject = "Reset Password - iKnow";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                var smtp = new SmtpClient {
                    Host = ConfigurationManager.AppSettings["GmailHost"],
                    Port = Int32.Parse(ConfigurationManager.AppSettings["GmailPort"]),
                    EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["GmailSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["GmailUserName"], ConfigurationManager.AppSettings["GmailPassword"])
                };
                await smtp.SendMailAsync(mailMessage);
            }
        }

        private string ConstructEmailBody(AppUser user, string callbackUrl) {
            var emailTemplate = HostingEnvironment.MapPath("~/App_Data/EmailTemplateForgotPassword.htm");
            var logoUrl = HostingEnvironment.MapPath("~/Content/Images/logo.png");

            var body = string.Empty;
            if (!string.IsNullOrEmpty(emailTemplate)) {
                using (StreamReader reader = new StreamReader(emailTemplate)) {
                    body = reader.ReadToEnd();
                }
            }
            body = body.Replace("{UserName}", HttpUtility.HtmlEncode(user.FullName));
            body = body.Replace("{LogoUrl}", logoUrl);
            body = body.Replace("{ResetUrl}", callbackUrl);
            return body;
        }
    }
}