using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [DataContract]
    public class EmailMessage
    {
        [DataMember]
        public Guid MessageId { get; set; } = Guid.NewGuid();
        [DataMember]

        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [DataMember]

        public string Subject { get; set; }
        [DataMember]

        public string Body { get; set; }
        [DataMember]

        public bool IsHtml { get; set; }
        [DataMember]

        public MailAddress From { get; set; }

        private MailAddressCollection _to;
        [DataMember]
        public MailAddressCollection To
        {
            get
            {
                if (_to == null)
                    _to = new MailAddressCollection();
                return _to;
            }
        }
        private MailAddressCollection _cc;

        [DataMember]
        public MailAddressCollection CC
        {
            get
            {
                if (_cc == null)
                    _cc = new MailAddressCollection();
                return _cc;
            }
        }
        private MailAddressCollection _bcc;

        [DataMember]
        public MailAddressCollection BCC
        {
            get
            {
                if (_bcc == null)
                    _bcc = new MailAddressCollection();
                return _bcc;
            }
        }
    }
}
