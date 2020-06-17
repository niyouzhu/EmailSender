using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.DataAccess
{
    public class MailEntry
    {
        [Key]
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public string From { get; set; }

        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }

    }
}
