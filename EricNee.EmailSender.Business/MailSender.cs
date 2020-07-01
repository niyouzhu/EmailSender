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

        public MailSender(BacklogMailQueue backlogQueue, InProcessMailQueue inProcessQueue, SuccessMailQueue successQueue, FailureMailQueue failureQueue, MailSettings settings)
        {
            BacklogQueue = backlogQueue;
            InProcessQueue = inProcessQueue;
            SuccessQueue = successQueue;
            FailureQueue = failureQueue;
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

        private object _backlogLock = new object();

        public Task Send()
        {
            Stopped.Value = false;
            return Task.Factory.ContinueWhenAll(new[] { Scan(), Process() }, (tasks) =>
            {
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                    {
                        throw task.Exception.Flatten();
                    }
                }

            });
        }

        public object _inProcessLock = new object();

        private Task ProcessBacklog()
        {
            return Task.Factory.StartNew(state =>
            {

                if (!((InternalStop)state).Value)
                {
                    lock (_backlogLock)
                    {
                        EmailMessage message = null;
                        try
                        {
                            while (BacklogQueue.Dequeue(out message) && !((InternalStop)state).Value)
                            {
                                InProcessQueue.Enqueue(message);
                            }
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
                    lock (_inProcessLock)
                    {
                        EmailMessage message = null;
                        try
                        {
                            while (InProcessQueue.Dequeue(out message) && !((InternalStop)state).Value)
                            {
                                MailClient.Send(message);
                                SuccessQueue.Enqueue(message);
                                Thread.Sleep((int)(1000 * MailSettings.SmtpInterval));
                            }
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
        private Task Process()
        {
            return Task.Factory.ContinueWhenAll(new[] { ProcessBacklog(), ProcessInProcess() }, tasks =>
            {
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                        throw task.Exception.Flatten();
                }

            });
        }

        private InternalStop Stopped { get; } = new InternalStop() { Value = false };

        private class InternalStop
        {
            public bool Value { get; set; } = false;
        }

        private Task Scan()
        {
            return Task.Factory.StartNew(state =>
            {
                if (!((InternalStop)state).Value)
                {
                    lock (_backlogLock)
                    {
                        BacklogQueue.Scan();
                    }
                    lock (_inProcessLock)
                    {
                        InProcessQueue.Scan(); // for directly insert data to db
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
