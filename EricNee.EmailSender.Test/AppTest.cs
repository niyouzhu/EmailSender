using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EricNee.EmailSender.Business;
using System.Net.Mail;

namespace EricNee.EmailSender.Test
{
    [TestClass]
    public class AppTest
    {


        [TestMethod]
        public void TestStart()
        {
            var app = new App(new MailSettings() { Host = "smtp.ssab.com" });
            app.Run();

        }

        [TestMethod]
        public void TestStart2()
        {
            var app = new App();
            app.Run();

        }
        [TestMethod]

        public void TestAddToBacklog()
        {
            var bcc = new MailAddressCollection() { new MailAddress("eric.nee@ssab.com", "Foo"), new MailAddress("eric.nee@ssab.com") };
            var cc = new MailAddressCollection() { new MailAddress("eric.nee@ssab.com", "Foo"), new MailAddress("eric.nee@ssab.com") };
            var to = new MailAddressCollection() { new MailAddress("eric.nee@ssab.com", "Foo"), new MailAddress("eric.nee@ssab.com") };
            var from = new MailAddress("eric.nee@ssab.com");
            var message = new EmailMessage() { From = from, Body = "Hellworlkd", CreatedTime = DateTime.Now, MessageId = Guid.NewGuid(), IsHtml = false, Subject = "Foobar" };
            message.BCC.AddRange(bcc);
            message.CC.AddRange(cc);
            message.To.AddRange(to);
            QueueManager.Instance.AddToBacklog(message);
        }

        [TestMethod]

        public void TestAddToBacklog2()
        {
            for (int i = 0; i < 1000; i++)
            {
                TestAddToBacklog();
            }
        }

        [TestMethod]
        public void TestStop()
        {
        }
    }
}
