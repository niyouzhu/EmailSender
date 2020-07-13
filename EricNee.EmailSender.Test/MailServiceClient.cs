using EricNee.EmailSender.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace EricNee.EmailSender.Test
{
    public class MailServiceClient : ClientBase<IMailService>, IMailService
    {
        public void Send(EmailMessage message)
        {
            Channel.Send(message);
        }
    }
}
