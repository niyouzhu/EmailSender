using EricNee.EmailSender.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace EricNee.EmailSender.Service
{
    public class App : IDisposable
    {

        private ServiceHost _host;
        public ServiceHost Host
        {
            get
            {
                if (_host == null)
                {
                    _host = new ServiceHost(typeof(MailService));
                }
                return _host;

            }
        }
        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed && _host != null)
            {
                _disposed = true;
                _host.Close();
                _host = null;
            }
        }

        public void Run()
        {
            Host.Open();
        }

        public void Stop()
        {
            Dispose();
        }
    }
}
