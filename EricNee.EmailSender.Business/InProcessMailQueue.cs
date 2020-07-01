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
            var rt = Data.TryDequeue(out message);
            if (rt)
            {
                DataAccessor.RemoveFromInProcess((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            }
            return rt;
        }

        public void Enqueue(EmailMessage message)
        {
            DataAccessor.AddToInProcess((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            Data.Enqueue(message);
        }

        public void Scan()
        {
            var inInprocess = DataAccessor.GetInProcessEntries();
            foreach (var item in inInprocess)
            {
                if (!Data.Select(it => it.MessageId).Contains(item.Id))
                    Data.Enqueue((EmailMessage)EmailMessageConverter.ConvertFrom(item));
            }
        }
    }
}
