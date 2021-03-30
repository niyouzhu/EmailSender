using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EricNee.EmailSender.WindowsService
{
    public partial class MainForm : Form
    {

        private EmailSender.Business.App SenderApp { get; } = new Business.App();

        private EmailSender.Service.App ReceiverApp { get; } = new Service.App();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            SenderApp.Run();
            ReceiverApp.Run();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            SenderApp.Stop();
            ReceiverApp.Stop();
        }
    }
}
