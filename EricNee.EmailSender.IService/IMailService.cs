using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [ServiceContract(Namespace ="http://me.zhuoyue.me")]
    public interface IMailService
    {
        [OperationContract]
        void Send(EmailMessage message);
    }
}
