using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace EricNee.EmailSender.IService
{
    [CollectionDataContract(Namespace = "http://me.zhuoyue.me")]
    [Serializable]
    public class MailAddressCollection : Collection<MailAddress>
    {
    }
}