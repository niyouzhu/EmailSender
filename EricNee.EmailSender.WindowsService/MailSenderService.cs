using EricNee.EmailSender.Business;
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
    public partial class MailSenderService : ServiceBase
    {
        public MailSenderService()
        {
            InitializeComponent();
        }

        private App _app;
        public App App
        {
            get
            {
                if (_app == null)
                {
                    _app = new App();
                    _app.OnException += (e) =>
                    {
                        Trace.WriteLine($"Time: {DateTime.Now}; {e.Ex}", "EmailSender");
                    };
                }
                return _app;
            }
        }
        protected override void OnStart(string[] args)
        {
            App.Run();
        }
        protected override void OnStop()
        {
            App.Stop();
        }
    }
}
