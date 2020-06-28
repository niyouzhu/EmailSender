using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EricNee.EmailSender.IService
{
    public static class Extensions
    {
        public static Business.EmailMessage ConvertTo(this EmailMessage message)
        {
            var rt = new Business.EmailMessage()
            {
                Body = message.Body,
                CreatedTime = message.CreatedTime,
                From = message.From.ConvertTo(),
                IsHtml = message.IsHtml,
                MessageId = message.MessageId,
                Subject = message.Subject
            };
            rt.BCC.AddRange(message.BCC.ConvertTo());
            rt.CC.AddRange(message.CC.ConvertTo());
            rt.To.AddRange(message.To.ConvertTo());
            return rt;
        }

        public static System.Net.Mail.MailAddress ConvertTo(this MailAddress address)
        {
            return new System.Net.Mail.MailAddress(address.Address, address.DisplayName);
        }

        public static System.Net.Mail.MailAddressCollection ConvertTo(this MailAddressCollection addresses)
        {
            var rt = new System.Net.Mail.MailAddressCollection();
            if (addresses != null)
                foreach (var address in addresses)
                {
                    rt.Add(address.ConvertTo());
                }
            return rt;
        }
    }
}
