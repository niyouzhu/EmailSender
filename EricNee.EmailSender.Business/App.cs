using EricNee.EmailSender.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EricNee.EmailSender.Business
{
    public class App : IDisposable
    {
        public MailSender Sender { get; }

        private Timer _timer;
        private Timer Timer
        {
            get
            {
                if (_timer == null)
                    _timer = new Timer(1000);
                return _timer;

            }
            set { _timer = value; }
        }

        public App() : this(MailSettings.GetSettings())
        {

        }

        public App(MailSettings settings)
        {
            var dataAccessor = new DataAccessor();
            Sender = new MailSender(new BacklogMailQueue(dataAccessor), new InProcessMailQueue(dataAccessor), new SuccessMailQueue(dataAccessor), new FailureMailQueue(dataAccessor), settings);
            Timer.Elapsed += (o, e) =>
            {
                var task = Sender.Send();
                task.ContinueWith(t =>
                {
                    Trace.WriteLine($"Time: {DateTime.Now}; {t.Exception.Flatten()}", "EmailSender");

                }, TaskContinuationOptions.OnlyOnFaulted);


            };

            Timer.Disposed += (o, e) =>
            {
                Sender.Stop();
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
            Timer.Dispose();
            Timer = null;
        }
    }
}
