namespace EricNee.EmailSender.WindowsService
{
    partial class MailServiceInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MailServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MailServiceServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.MailSenderServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MailServiceProcessInstaller
            // 
            this.MailServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.MailServiceProcessInstaller.Password = null;
            this.MailServiceProcessInstaller.Username = null;
            // 
            // MailServiceServiceInstaller
            // 
            this.MailServiceServiceInstaller.Description = "Mail Service";
            this.MailServiceServiceInstaller.DisplayName = "Mail Service";
            this.MailServiceServiceInstaller.ServiceName = "MailService";
            this.MailServiceServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // MailSenderServiceInstaller
            // 
            this.MailSenderServiceInstaller.Description = "Mail Sender";
            this.MailSenderServiceInstaller.DisplayName = "Mail Sender";
            this.MailSenderServiceInstaller.ServiceName = "MailSender";
            this.MailSenderServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // MailServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MailServiceProcessInstaller,
            this.MailServiceServiceInstaller,
            this.MailSenderServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MailServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MailServiceServiceInstaller;
        private System.ServiceProcess.ServiceInstaller MailSenderServiceInstaller;
    }
}