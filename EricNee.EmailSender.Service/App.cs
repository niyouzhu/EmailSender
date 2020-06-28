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

        public ServiceHost Host { get; private set; } = new ServiceHost(typeof(MailService), new Uri("http://localhost:8090"));
        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed && Host != null)
            {
                _disposed = true;
                Host.Close();
                Host = null;
            }
        }

        public void Run()
        {
            var binding = new System.ServiceModel.BasicHttpBinding();
            var smb = Host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (smb == null)
            {
                smb = new ServiceMetadataBehavior() { HttpGetEnabled = true };
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                Host.Description.Behaviors.Add(smb);
            };
            Host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            Host.AddServiceEndpoint(typeof(IMailService), binding, "MailService.svc");
            Host.Open();
        }

        public void Stop()
        {
            Dispose();
        }
    }
}
