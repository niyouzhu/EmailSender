using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EricNee.EmailSender.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                System.Windows.Forms.Application.Run(new MainForm());
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new MailService(),
                new MailSenderService()
                };
                ServiceBase.Run(ServicesToRun);
            }
           
        }
    }
}
