using EricNee.EmailSender.Core;
using EricNee.EmailSender.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace EricNee.EmailSender.Business
{

    public class EmailMessageTypeConverter : TypeConverter
    {

        private MailAddress Unformat(string mailAddress)
        {
            var regExp = new Regex(@"(?<Name>.*?)\<\<(?<Address>.*?)\>\>");
            var match = regExp.Match(mailAddress);
            if (match.Success && match.Groups["Address"].Success)
            {
                MailAddress rt;
                if (match.Groups["Name"].Success)
                    rt = new MailAddress(match.Groups["Address"].Value, match.Groups["Name"].Value);
                else
                    rt = new MailAddress(match.Groups["Address"].Value);
                return rt;
            }
            throw new EmailException($"{nameof(mailAddress)} is not mail address.");
        }
        private string Format(MailAddress mailAddress)
        {
            if (string.IsNullOrWhiteSpace(mailAddress.DisplayName))
            {
                return $"<<{mailAddress.Address}>>";
            }
            return $"{mailAddress.DisplayName}<<{mailAddress.Address}>>";
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var mailEntry = value as MailEntry;
            if (mailEntry != null)
            {
                var message = new EmailMessage() { Body = mailEntry.Body, CreatedTime = mailEntry.CreatedTime, IsHtml = mailEntry.IsHtml, MessageId = mailEntry.Id, Subject = mailEntry.Subject };
                var strTo = mailEntry.To.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                var to = new MailAddressCollection();
                foreach (var item in strTo)
                {
                    to.Add(Unformat(item));
                }
                var strCC = mailEntry.CC.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                var cc = new MailAddressCollection();
                foreach (var item in strCC)
                {
                    cc.Add(Unformat(item));
                }
                var strBCC = mailEntry.To.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                var bcc = new MailAddressCollection();
                foreach (var item in strBCC)
                {
                    bcc.Add(Unformat(item));
                }

                message.From = Unformat(mailEntry.From);
                message.To.AddRange(to);
                message.CC.AddRange(cc);
                message.BCC.AddRange(bcc);
                return message;
            }
            throw new EmailException($"{nameof(value)} is not correct type.");

        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var mailMessage = value as EmailMessage;
            if (mailMessage != null)
            {
                var entry = new MailEntry() { Body = mailMessage.Body, CreatedTime = mailMessage.CreatedTime, Id = mailMessage.MessageId, IsHtml = mailMessage.IsHtml, Subject = mailMessage.Subject };
                entry.From = Format(mailMessage.From);
                var to = new List<string>();
                foreach (var item in mailMessage.To)
                {
                    to.Add(Format(item));
                }
                entry.To = string.Join("||", to);
                var cc = new List<string>();
                foreach (var item in mailMessage.CC)
                {
                    cc.Add(Format(item));
                }
                entry.CC = string.Join("||", cc);
                var bcc = new List<string>();
                foreach (var item in mailMessage.BCC)
                {
                    bcc.Add(Format(item));
                }
                entry.BCC = string.Join("||", bcc);

                return entry;
            }

            throw new EmailException($"{nameof(value)} is not correct type.");

        }
    }
}
