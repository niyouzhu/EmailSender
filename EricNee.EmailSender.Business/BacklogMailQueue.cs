using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EricNee.EmailSender.DataAccess;
using System.Collections.Concurrent;

namespace EricNee.EmailSender.Business
{
    public class BacklogMailQueue : IMailQueue<EmailMessage>
    {

        public BacklogMailQueue(DataAccessor dataAccessor)
        {
            DataAccessor = dataAccessor;
        }

        public ConcurrentQueue<EmailMessage> Data { get; } = new ConcurrentQueue<EmailMessage>();

        private EmailMessageTypeConverter EmailMessageConverter = new EmailMessageTypeConverter();

        public DataAccessor DataAccessor
        {
            get;
        }

        public bool Dequeue(out EmailMessage message)
        {
            var rt = Data.TryDequeue(out message);
            if (rt)
            {
                lock (_lock)
                    DataAccessor.RemoveFromBacklog((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            }
            return rt;
        }

        private object _lock = new object();

        public void Enqueue(EmailMessage message)
        {
            lock (_lock)
            {
                DataAccessor.AddToBacklog((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            }
            Data.Enqueue(message);
        }

        public void Scan()
        {
            IEnumerable<MailEntry> backlog;
            lock (_lock)
            {
                backlog = DataAccessor.GetBacklogEntries();
            }
            foreach (var item in backlog)
            {
                Data.Enqueue((EmailMessage)EmailMessageConverter.ConvertFrom(item));
            }
        }
    }
}
