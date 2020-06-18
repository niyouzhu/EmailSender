﻿namespace EricNee.EmailSender.WindowsService
{
    partial class ProjectInstaller
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
            this.EmailSenderProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EmailSenderInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EmailSenderProcessInstaller
            // 
            this.EmailSenderProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.EmailSenderProcessInstaller.Password = null;
            this.EmailSenderProcessInstaller.Username = null;
            // 
            // EmailSenderInstaller
            // 
            this.EmailSenderInstaller.Description = "EmailSender";
            this.EmailSenderInstaller.DisplayName = "EmailSender";
            this.EmailSenderInstaller.ServiceName = "EmailSender";
            this.EmailSenderInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EmailSenderInstaller,
            this.EmailSenderProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EmailSenderProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EmailSenderInstaller;
    }
}