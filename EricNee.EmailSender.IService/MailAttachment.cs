using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [DataContract]

    public class MailAttachment
    {
        public MailAttachment(string fileName, string mediaType)
        {
            FileName = fileName;
            MediaType = mediaType;
        }

        public MailAttachment(string fileName) : this(fileName, "application/octet-stream")
        {

        }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]

        public byte[] Content { get; set; }
        [DataMember]

        public string MediaType { get; set; }
    }

    [CollectionDataContract]

    public class MailAttachmentCollection : Collection<MailAttachment>
    {
    }
}
