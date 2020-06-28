using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EricNee.EmailSender.WindowsService
{
    partial class MailService : ServiceBase
    {
        public MailService()
        {
            InitializeComponent();
        }

        public Service.App App { get; } = new Service.App();

        protected override void OnStart(string[] args)
        {
            App.Run();
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            App.Stop();
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
