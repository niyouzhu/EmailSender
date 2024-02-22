using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [DataContract(Namespace = "http://me.zhuoyue.me")]
    public class EmailMessage
    {
        //[DataMember]
        [IgnoreDataMember]
        public Guid MessageId { get { if (_messageId == Guid.Empty) _messageId = Guid.NewGuid(); return _messageId; } }
        private Guid _messageId;


        [IgnoreDataMember]
        public DateTime? CreatedTime { get { if (_now == null) _now = DateTime.Now; return _now; } }
        private DateTime? _now;


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
            set
            {
                _to = value;

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
            set { _cc = value; }
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
            set { _bcc = value; }
        }

        private MailAttachmentCollection _attachments;
        [DataMember]
        public MailAttachmentCollection Attachments
        {
            get
            {
                if (_attachments == null)
                    _attachments = new MailAttachmentCollection();
                return _attachments;
            }
            set { _attachments = value; }
        }
    }
}
