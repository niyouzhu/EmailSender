using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EricNee.EmailSender.Business
{
    public class EmailMessage
    {

        private MailMessage MailMessage { get; } = new MailMessage();

        public Guid MessageId { get; set; } = Guid.NewGuid();

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string Subject { get { return MailMessage.Subject; } set { MailMessage.Subject = value; } }

        public string Body { get { return MailMessage.Body; } set { MailMessage.Body = value; } }

        public bool IsHtml { get { return MailMessage.IsBodyHtml; } set { MailMessage.IsBodyHtml = value; } }

        public MailAddress From { get { return MailMessage.From; } set { MailMessage.From = value; } }

        public MailAddressCollection To
        {
            get
            {
                return MailMessage.To;
            }
        }

        public MailAddressCollection CC { get { return MailMessage.CC; } }

        public MailAddressCollection BCC { get { return MailMessage.Bcc; } }

        public AttachmentCollection Attachments { get { return MailMessage.Attachments; } }

    }
}
