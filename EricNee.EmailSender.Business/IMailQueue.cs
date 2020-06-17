using EricNee.EmailSender.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Business
{
    public interface IMailQueue<T> where T : EmailMessage
    {
        DataAccessor DataAccessor { get; }
        void Enqueue(T message);
        bool Dequeue(out T message);

        void Scan();
    }
}
