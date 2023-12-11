using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace KIS_Core.Domain.Models
{
    public class Message
    {
        public bool isHtml { get; set; }        
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {            
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress( "email", x ) ));
            Subject = subject;
            Content = content;
            isHtml = false;
        }
        public Message(IEnumerable<string> to, string subject, string content, bool ishtml)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
            isHtml = ishtml;
        }

    }
}
