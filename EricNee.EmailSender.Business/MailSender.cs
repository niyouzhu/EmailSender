using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Business
{
    public class MailSender
    {

        public MailSender(BacklogMailQueue backlogQueue, InProcessMailQueue inProcessQueue, SuccessMailQueue successQueue, FailureMailQueue failureQueue, MailSettings settings)
        {
            BacklogQueue = backlogQueue;
            InProcessQueue = inProcessQueue;
            SuccessQueue = successQueue;
            FailureQueue = failureQueue;
            MailSettings = settings;
        }

        public MailSettings MailSettings { get; }

        private MailClient _mailClient;
        public MailClient MailClient
        {
            get
            {

                if (_mailClient == null)
                    _mailClient = new MailClient(MailSettings);
                return _mailClient;
            }
        }

        public BacklogMailQueue BacklogQueue { get; }

        public InProcessMailQueue InProcessQueue { get; }
        public SuccessMailQueue SuccessQueue { get; }
        public FailureMailQueue FailureQueue { get; }

        private object _lcok = new object();

        public void Scan()
        {
            lock (_lcok)
            {
                if (BacklogQueue.Data.Count == 0)
                    BacklogQueue.Scan();
            }
            EmailMessage message = null;
            try
            {
                if (BacklogQueue.Dequeue(out message))
                {
                    InProcessQueue.Enqueue(message);
                }
                if (InProcessQueue.Dequeue(out message))
                {
                    MailClient.Send(message);
                    SuccessQueue.Enqueue(message);
                }
            }
            catch (Exception)
            {
                FailureQueue.Enqueue(message);
                throw;
            }

        }
    }


}
