using DataAccess.Entities.EmailEntity;
using MailKit.Net.Smtp;
using MimeKit;
using Services.Abstracts;

namespace Services.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmail(message);
            SendMessage(emailMessage);
        }

        private MimeMessage CreateEmail(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("company.bookify@gmail.com", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        private void SendMessage(MimeMessage mailMessage)
        {
            var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);
                client.Send(mailMessage);
            }
            catch
            {
                throw new Exception("Error occured!");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}