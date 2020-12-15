using Blog.Data.Configurations;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Blog.Data.Services
{
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly MailAccountSettings _defaultAccount;
        private readonly SmtpClient _client;

        public MailService(IOptions<SmtpSettings> smtpSettings, IOptions<MailAccountSettings> defaultAccount)
        {
            _smtpSettings = smtpSettings.Value;
            _defaultAccount = defaultAccount.Value;

            _client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port);
            _client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
        }

        public async Task SendEmailConfirmation(string recipientName, string recipientEmail, string subject, string body) =>
            await SendMail(_defaultAccount.DefaultSender, _defaultAccount.DefaultEmail, recipientName, recipientEmail, subject, body);

        public async Task SendMail(string senderName, string senderEmail, string recipientName, string recipientEmail, string subject, string body)
        {
            var sender = new MailAddress(senderEmail, senderName);
            var recipient = new MailAddress(recipientEmail, recipientName);
            var message = new MailMessage(sender, recipient)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            await _client.SendMailAsync(message);
        }
    }
}
