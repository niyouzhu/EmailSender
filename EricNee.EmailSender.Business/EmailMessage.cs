using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EricNee.EmailSender.Business
{
    public class EmailMessage
    {
        public Guid MessageId { get; set; }

        public DateTime CreatedTime { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public MailAddress From { get; set; }

        public MailAddressCollection To { get; set; }

        public MailAddressCollection CC { get; set; }

        public MailAddressCollection BCC { get; set; }

    }
}
