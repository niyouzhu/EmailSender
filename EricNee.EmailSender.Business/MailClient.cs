using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EricNee.EmailSender.Business
{
    public class MailClient
    {
        public MailClient(MailSettings settings)
        {
            Settings = settings;
        }

        public MailSettings Settings { get; }

        public void Send(EmailMessage emailMessage)
        {
            using (var smtpClient = new SmtpClient() { Host = Settings.Host, EnableSsl = Settings.SSL, Port = Settings.Port })
            {
                if (!string.IsNullOrWhiteSpace(Settings.UserName))
                {
                    smtpClient.Credentials = new NetworkCredential(Settings.UserName, Settings.Password);
                }
                var message = new MailMessage() { Body = emailMessage.Body, From = emailMessage.From, Subject = emailMessage.Subject, IsBodyHtml = emailMessage.IsHtml };
                message.To.AddRange(emailMessage.To);
                message.CC.AddRange(emailMessage.CC);
                message.Bcc.AddRange(emailMessage.BCC);
                smtpClient.Send(message);
            }


        }
    }
}
