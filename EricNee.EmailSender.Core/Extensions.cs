using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Core
{
    class Extensions
    {
    }
}

namespace System.Net.Mail
{
    public static class Extensions
    {
        public static void AddRange(this MailAddressCollection source, MailAddressCollection range)
        {
            foreach (var item in range)
            {
                source.Add(item);
            }
        }

        public static void AddRange(this AttachmentCollection source, AttachmentCollection range)
        {
            foreach (var item in range)
            {
                source.Add(item);
            }
        }
    }
}
