using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EricNee.EmailSender.DataAccess;
using System.Collections.Concurrent;

namespace EricNee.EmailSender.Business
{
    public class SuccessMailQueue : IMailQueue<EmailMessage>
    {
        public SuccessMailQueue(DataAccessor dataAccessor)
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
            throw new NotImplementedException();
        }

        public void Enqueue(EmailMessage message)
        {
            DataAccessor.AddToSuccess((MailEntry)EmailMessageConverter.ConvertTo(message, typeof(MailEntry)));
            Data.Enqueue(message);
        }

        public void Scan()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromStore(EmailMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
