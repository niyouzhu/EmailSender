using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [DataContract]

    public class MailAttachment
    {
        public MailAttachment(string filePath, string mediaType)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(FileName);
            MediaType = mediaType;
            Content = File.ReadAllBytes(filePath);
        }

        public MailAttachment(string filePath) : this(filePath, "application/octet-stream")
        {

        }

        public MailAttachment(byte[] content) : this(content, "application/octet-stream")
        {
        }
        public MailAttachment(byte[] content, string mediaType)
        {
        }
        [DataMember]
        public string FilePath { get; set; }

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
