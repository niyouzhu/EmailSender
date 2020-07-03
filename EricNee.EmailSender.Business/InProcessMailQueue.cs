using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EricNee.EmailSender.DataAccess;
using System.Collections.Concurrent;

namespace EricNee.EmailSender.Business
{
    public class InProcessMailQueue : IMailQueue<EmailMessage>
    {
        public ConcurrentQueue<EmailMessage> Data { get; } = new ConcurrentQueue<EmailMessage>();

        private EmailMessageTypeConverter EmailMessageConverter = new EmailMessageTypeConverter();
        public InProcessMailQueue(DataAccessor dataAccessor)
        {
            DataAccessor = dataAccessor;
        }
        public DataAccessor DataAccessor
        {
            get;
        }

        public bool Dequeue(out EmailMessage message)
        {
            lock (_lock)
            {
                var rt = Data.TryDequeue(out message);
                if (rt)
                    DataAccessor.RemoveFromInProcess((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
                return rt;
            }

        }

        public void Enqueue(EmailMessage message)
        {
            lock (_lock)
            {
                Data.Enqueue(message);
                DataAccessor.AddToInProcess((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));

            }
        }
        private object _lock = new object();
        public void Scan()
        {
            lock (_lock)
            {
                var inProcess = DataAccessor.GetInProcessEntries();
                foreach (var item in inProcess)
                {
                    if (!Data.Select(it => it.MessageId).Contains(item.Id))
                        Data.Enqueue((EmailMessage)EmailMessageConverter.ConvertFrom(item));
                }

            }

        }

    }
}
