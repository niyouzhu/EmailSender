using EricNee.EmailSender.Business;
using EricNee.EmailSender.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Service
{
    [ErrorHandler]
    public class MailService : IMailService
    {
        public void Send(IService.EmailMessage message)
        {
            QueueManager.Instance.AddToBacklog(message.ConvertTo());
        }
    }
}
