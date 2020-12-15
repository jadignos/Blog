using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Services
{
    public interface IMailService
    {
        Task SendMail(string senderName, string senderEmail, string recipientName, string recipientEmail, string subject, string body);
        
        Task SendEmailConfirmation(string recipientName, string recipientEmail, string subject, string body);
    }
}
