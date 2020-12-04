using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using iKnow.Core;
using iKnow.Helper;
using iKnow.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;

namespace iKnow.Helper {
    public class EmailSender : IEmailSender
    {
        private IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendForgotPasswordMailAsync(AppUser user, string callbackUrl) {
            var body = ConstructEmailBody(user, callbackUrl);

            using (MailMessage mailMessage = new MailMessage(_configuration["AppSettings:GmailUserName"], user.Email)) {
                mailMessage.Subject = "Reset Password - iKnow";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                var smtp = new SmtpClient {
                    Host = _configuration["AppSettings:GmailHost"],
                    Port = Int32.Parse(_configuration["AppSettings:GmailPort"]),
                    EnableSsl = Boolean.Parse(_configuration["AppSettings:GmailSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_configuration["AppSettings:GmailUserName"], _configuration["AppSettings:GmailPassword"])
                };
                await smtp.SendMailAsync(mailMessage);
            }
        }

        private string ConstructEmailBody(AppUser user, string callbackUrl) {
            var emailTemplate = ServerHelper.MapPath("~/App_Data/EmailTemplateForgotPassword.htm");
            var logoUrl = ServerHelper.MapPath("~/Content/Images/logo.png");

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