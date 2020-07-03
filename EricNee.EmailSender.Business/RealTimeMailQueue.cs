using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EricNee.EmailSender.DataAccess;
using System.Collections.Concurrent;

namespace EricNee.EmailSender.Business
{
    public class RealTimeMailQueue : IMailQueue<EmailMessage>
    {
        public RealTimeMailQueue(DataAccessor dataAccessor)
        {
            DataAccessor = dataAccessor;
        }

        public ConcurrentQueue<EmailMessage> Data { get; } = new ConcurrentQueue<EmailMessage>();

        private EmailMessageTypeConverter EmailMessageConverter = new EmailMessageTypeConverter();

        public DataAccessor DataAccessor
        {
            get;
        }

        public void Enqueue(EmailMessage message)
        {
            lock (_lock)
            {
                Data.Enqueue(message);
                DataAccessor.AddToRealTime((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            }
        }
        private object _lock = new object();

        public bool Dequeue(out EmailMessage message)
        {
            lock (_lock)
            {
                var rt = Data.TryDequeue(out message);
                if (rt)
                {
                    DataAccessor.RemoveFromRealTime((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
                }
                return rt;
            }
        }

        public void Scan()
        {
            lock (_lock)
            {
                var realTime = DataAccessor.GetRealTimeEntries();
                foreach (var item in realTime)
                {
                    if (!Data.Select(it => it.MessageId).Contains(item.Id))
                        Data.Enqueue((EmailMessage)EmailMessageConverter.ConvertFrom(item));
                }
            }
        }

        public void RemoveFromStore(EmailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
