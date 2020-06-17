using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Business
{
    public class QueueManager
    {
        public QueueManager(BacklogMailQueue backlog)
        {
            Backlog = backlog;
        }

        public BacklogMailQueue Backlog { get; }

        public void AddToBacklog(EmailMessage message)
        {
            Backlog.Enqueue(message);
        }

        private static QueueManager _instance;
        public static QueueManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QueueManager(new BacklogMailQueue(new DataAccess.DataAccessor()));
                return _instance;
                ;
            }
        }
    }
}
