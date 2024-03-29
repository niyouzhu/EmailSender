﻿using System;
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
            lock (_lock)
            {
                var rt = Data.TryDequeue(out message);
                if (rt)
                {
                    DataAccessor.RemoveFromBacklog((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
                }
                return rt;
            }
        }


        public void Enqueue(EmailMessage message)
        {
            lock (_lock)
            {
                Data.Enqueue(message);
                DataAccessor.AddToBacklog((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            }
        }

        private object _lock = new object();

        public void Scan()
        {
            lock (_lock)
            {
                var backlog = DataAccessor.GetBacklogEntries();
                foreach (var item in backlog)
                {
                    if (!Data.Select(it => it.MessageId).Contains(item.Id))
                        Data.Enqueue((EmailMessage)EmailMessageConverter.ConvertFrom(item));
                }
            }
        }

    }
}
