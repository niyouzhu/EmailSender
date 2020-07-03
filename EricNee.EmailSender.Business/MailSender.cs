using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EricNee.EmailSender.Business
{
    public class MailSender
    {

        public MailSender(BacklogMailQueue backlogQueue, InProcessMailQueue inProcessQueue, SuccessMailQueue successQueue, FailureMailQueue failureQueue, RealTimeMailQueue realTimeQueue, MailSettings settings)
        {
            BacklogQueue = backlogQueue;
            InProcessQueue = inProcessQueue;
            SuccessQueue = successQueue;
            FailureQueue = failureQueue;
            RealTimeQueue = realTimeQueue;
            MailSettings = settings;
        }

        public MailSettings MailSettings { get; }

        private MailClient _mailClient;
        public MailClient MailClient
        {
            get
            {

                if (_mailClient == null)
                    _mailClient = new MailClient(MailSettings);
                return _mailClient;
            }
        }

        public BacklogMailQueue BacklogQueue { get; }

        public InProcessMailQueue InProcessQueue { get; }
        public SuccessMailQueue SuccessQueue { get; }
        public FailureMailQueue FailureQueue { get; }
        public RealTimeMailQueue RealTimeQueue { get; }

        private object _backlogLock = new object();

        public Task Send()
        {
            Stopped.Value = false;
            return Process();
        }

        private Task ProcessBacklog()
        {
            return Task.Factory.StartNew(state =>
            {

                if (!((InternalStop)state).Value)
                {
                    EmailMessage message = null;
                    while (BacklogQueue.Dequeue(out message) && !((InternalStop)state).Value)
                    {
                        try
                        {
                            InProcessQueue.Enqueue(message);
                        }
                        catch (Exception)
                        {
                            FailureQueue.Enqueue(message);
                            throw;
                        }
                    }
                }
            }, Stopped, TaskCreationOptions.AttachedToParent);
        }

        private Task ProcessInProcess()
        {
            return Task.Factory.StartNew(state =>
            {

                if (!((InternalStop)state).Value)
                {
                    EmailMessage message = null;
                    while (InProcessQueue.Dequeue(out message) && !((InternalStop)state).Value)
                    {
                        try
                        {
                            RealTimeQueue.Enqueue(message);
                            MailClient.Send(message);
                            SuccessQueue.Enqueue(message);
                            Thread.Sleep((int)(1000 * MailSettings.SmtpInterval));
                        }
                        catch (Exception)
                        {
                            FailureQueue.Enqueue(message);
                            throw;

                        }
                        finally
                        {
                            RealTimeQueue.Dequeue(out message);
                        }
                    }
                }
            }, Stopped, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
        }
        private Task Process()
        {
            return Task.Factory.ContinueWhenAll(new[] { ProcessBacklog(), ProcessInProcess() }, tasks =>
            {
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                        throw task.Exception;
                }

            });
        }

        private InternalStop Stopped { get; } = new InternalStop() { Value = false };

        private class InternalStop
        {
            public bool Value { get; set; } = false;
        }

        private bool _firstRun = true;
        public Task Scan()
        {
            Stopped.Value = false;
            return Task.Factory.StartNew(state =>
            {
                if (!((InternalStop)state).Value)
                {
                    BacklogQueue.Scan();
                    InProcessQueue.Scan(); // for directly insert data to db
                    if (_firstRun)
                    {
                        _firstRun = false;
                        RealTimeQueue.Scan();
                        EmailMessage message;
                        while (!((InternalStop)state).Value && RealTimeQueue.Dequeue(out message))
                        {
                            InProcessQueue.Enqueue(message);
                        }
                    }
                }
            }, Stopped, TaskCreationOptions.AttachedToParent);

        }

        public void Stop()
        {
            Stopped.Value = true;
        }
    }


}
