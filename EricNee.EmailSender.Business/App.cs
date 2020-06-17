using EricNee.EmailSender.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace EricNee.EmailSender.Business
{
    public class App : IDisposable
    {
        public MailSender Sender { get; }

        private Timer Timer { get; } = new Timer(100);

        public App() : this(MailSettings.GetSettings())
        {

        }

        public App(MailSettings settings)
        {
            var dataAccessor = new DataAccessor();
            Sender = new MailSender(new BacklogMailQueue(dataAccessor), new InProcessMailQueue(dataAccessor), new SuccessMailQueue(dataAccessor), new FailureMailQueue(dataAccessor), settings);
            Timer.Elapsed += (o, e) =>
            {
                try
                {
                    Sender.Scan();

                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex,"EmailSender");
                }
            };
        }



        private bool _disposed;
        private object _lock = new object();
        public void Dispose()
        {
            lock (_lock)
            {
                if (!_disposed)
                {
                    Timer.Dispose();
                    _disposed = true;

                }
            }
        }

        public void Run()
        {
            //Sender.Scan();
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
