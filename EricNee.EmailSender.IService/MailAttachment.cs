using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [DataContract(Namespace = "http://me.zhuoyue.me")]
    [Serializable]

    public class MailAttachment
    {

        public MailAttachment() { }

        public MailAttachment(string filePath, string mediaType)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(FilePath);
            MediaType = mediaType;
            Content = File.ReadAllBytes(FilePath);
        }

        public MailAttachment(string filePath) : this(filePath, "application/octet-stream")
        {

        }

        public MailAttachment(byte[] content) : this(content, null)
        {
        }
        public MailAttachment(byte[] content, string fileName, string mediaType)
        {
            Content = content;
            FileName = fileName;
            MediaType = mediaType;
        }
        public MailAttachment(byte[] content, string fileName) : this(content, fileName, "application/octet-stream")
        {
            Content = content;
        }
        [IgnoreDataMember]
        public string FilePath { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] Content { get; set; }

        [DataMember]
        public string MediaType { get { if (string.IsNullOrWhiteSpace(_mediaType)) _mediaType = "application/octet-stream"; return _mediaType; } set { _mediaType = value; } }

        private string _mediaType;

    }

    [CollectionDataContract(Namespace = "http://me.zhuoyue.me")]
    [Serializable]

    public class MailAttachmentCollection : Collection<MailAttachment>
    {
    }
}
