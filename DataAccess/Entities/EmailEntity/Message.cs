﻿using MimeKit;

namespace DataAccess.Entities.EmailEntity
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(t => new MailboxAddress("company.bookify@gmail.com", t)));
            Subject = subject;
            Content = content;
        }
    }
}