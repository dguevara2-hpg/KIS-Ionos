using KIS_Core.Domain.Interfaces;
using KIS_Core.Domain.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using MailKit;

namespace KIS_Core.Domain.Managers
{
    public class EmailManager : IEmailSender
    {
        private readonly EmailConfig  _emailConfig;

        public EmailManager(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }


        public void SendEmail(Message message) {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }        

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();            
            
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From_Name, _emailConfig.From));            
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;   
            
            if (message.isHtml)
            {
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            }
            else
            {
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            }
            
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using ( SmtpClient mailClient = new SmtpClient() )
            {
                mailClient.Connect(_emailConfig.SmtpServer, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                mailClient.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                mailClient.Send(mailMessage);
                mailClient.Disconnect(true);
            }            
        }
    }
}
