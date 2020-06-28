using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace EricNee.EmailSender.IService
{
    [CollectionDataContract]
    public class MailAddressCollection : Collection<MailAddress>
    {
    }
}