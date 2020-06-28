using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace EricNee.EmailSender.WindowsService
{
    [RunInstaller(true)]
    public partial class MailServiceInstaller : System.Configuration.Install.Installer
    {
        public MailServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
