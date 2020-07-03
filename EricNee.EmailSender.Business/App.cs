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
        public event Action<ExceptionEventArgs> OnException;

        public MailSender Sender { get; }

        private Timer _scanningTimer;
        private Timer ScanningTimer
        {
            get
            {
                if (_scanningTimer == null)
                    _scanningTimer = new Timer(1000);
                return _scanningTimer;

            }
            set { _scanningTimer = value; }
        }

        private Timer _sendingTimer;
        private Timer SendingTimer
        {
            get
            {
                if (_sendingTimer == null)
                    _sendingTimer = new Timer(1000);
                return _sendingTimer;

            }
            set { _sendingTimer = value; }
        }
        public App() : this(MailSettings.GetSettings())
        {

        }

        public App(MailSettings settings)
        {
            var dataAccessor = new DataAccessor();
            Sender = new MailSender(new BacklogMailQueue(dataAccessor), new InProcessMailQueue(dataAccessor), new SuccessMailQueue(dataAccessor), new FailureMailQueue(dataAccessor), new RealTimeMailQueue(dataAccessor), settings);
            bool scanning = false;
            //object scanningLock = new object();
            ScanningTimer.Elapsed += (o, e) =>
            {
                if (!scanning)
                {
                    scanning = true;
                    var task = Sender.Scan();
                    task.ContinueWith(t =>
                    {
                        scanning = false;
                        if (t.IsFaulted)
                        {
                            var eventArgs = new ExceptionEventArgs(t.Exception);
                            OnException?.Invoke(eventArgs);
                        }
                    });
                }

            };
            bool sending = false;
            SendingTimer.Elapsed += (o, e) =>
            {
                if (!sending)
                {
                    sending = true;
                    var task = Sender.Send();
                    task.ContinueWith(t =>
                    {
                        sending = false;
                        if (t.IsFaulted)
                        {
                            var eventArgs = new ExceptionEventArgs(t.Exception);
                            OnException?.Invoke(eventArgs);
                        }

                    });
                }


            };

            ScanningTimer.Disposed += (o, e) =>
            {
                Sender.Stop();
            };

            SendingTimer.Disposed += (o, e) =>
            {
                Sender.Stop();
            };
        }



        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Stop();
            }
        }

        public void Run()
        {
            ScanningTimer.Start();
            SendingTimer.Start();
        }

        public void Stop()
        {
            ScanningTimer.Stop();
            ScanningTimer.Dispose();
            ScanningTimer = null;
            SendingTimer.Stop();
            SendingTimer.Dispose();
            SendingTimer = null;

        }
    }
}
