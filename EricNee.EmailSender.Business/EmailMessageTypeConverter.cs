using EricNee.EmailSender.Core;
using EricNee.EmailSender.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
            throw new EmailException($"{nameof(mailAddress)} is not valid mail address.");
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
                foreach (var item in strTo)
                {
                    message.To.Add(Unformat(item));
                }
                var strCC = mailEntry.CC.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in strCC)
                {
                    message.CC.Add(Unformat(item));
                }
                var strBCC = mailEntry.BCC.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in strBCC)
                {
                    message.BCC.Add(Unformat(item));
                }
                message.From = Unformat(mailEntry.From);
                var strAttachments = mailEntry.Attachments.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in strAttachments)
                {
                    message.Attachments.Add(UnformatAttachment(item));
                }
                return message;
            }
            throw new EmailException($"{nameof(value)} is not correct type.");

        }

        private Attachment UnformatAttachment(string mailAttachment)
        {
            var regExp = new Regex(@"(?<FileName>.*?)\<\<(?<MediaType>.*)\>\>\<\<(?<Content>.*?)\>\>");
            var match = regExp.Match(mailAttachment);
            if (match.Success)
            {
                string fileName = null;
                if (match.Groups["FileName"].Success)
                {
                    fileName = match.Groups["FileName"].Value;
                }
                string mediaType = null;
                if (match.Groups["MediaType"].Success)
                {
                    mediaType = match.Groups["MediaType"].Value;
                }
                string content = null;
                if (match.Groups["Content"].Success)
                {
                    content = match.Groups["Content"].Value;
                }
                var stream = new MemoryStream();
                var bytes = Convert.FromBase64CharArray(content.ToCharArray(), 0, content.Length);
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                return new Attachment(stream, fileName, mediaType);
            }
            throw new EmailException($"{nameof(mailAttachment)} is not valid attachment.");
        }

        private string FormatAttachment(Attachment mailAttachment)
        {
            var bufferSize = 4096;
            var buffer = new byte[bufferSize];
            var length = mailAttachment.ContentStream.Length;
            int pageCount;
            int remainder = (int)(length % bufferSize);
            if (remainder == 0)
            {
                pageCount = (int)(length / bufferSize);
            }
            else
            {
                pageCount = ((int)(length / bufferSize)) + 1;
            }
            var bytes = new byte[length];
            mailAttachment.ContentStream.Position = 0;
            for (int i = 0; i < pageCount; i++)
            {
                if (i == (pageCount - 1) && remainder != 0)
                {
                    mailAttachment.ContentStream.Read(buffer, 0, remainder);
                    buffer.Take(remainder).ToArray().CopyTo(bytes, i * bufferSize);
                }
                else
                {
                    mailAttachment.ContentStream.Read(buffer, 0, bufferSize);
                    buffer.CopyTo(bytes, i * bufferSize);
                }
            }
            mailAttachment.ContentStream.Position = 0;
            return $"{mailAttachment.Name}<<{mailAttachment.ContentType.MediaType}>><<{Convert.ToBase64String(bytes)}>>";
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
                var attachments = new List<string>();
                foreach (var item in mailMessage.Attachments)
                {
                    attachments.Add(FormatAttachment(item));
                }
                entry.Attachments = string.Join("||", attachments);

                return entry;
            }

            throw new EmailException($"{nameof(value)} is not correct type.");

        }
    }
}
