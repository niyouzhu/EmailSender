using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EricNee.EmailSender.Service;
using System.Net.Mail;
using System.Threading;
using EricNee.EmailSender.IService;
using System.IO;

namespace EricNee.EmailSender.Test
{
    [TestClass]
    public class ServiceTest
    {



        [TestMethod]
        public void TestService()
        {
            using (var client = new MailServiceClient())
            {
                var mailMessage = new EmailMessage() {  Subject = "Hello World", IsHtml = true, From = new IService.MailAddress("system@gyzet.com"), Body = "Foobar" };
                mailMessage.Attachments.Add(new MailAttachment("HelloWorld.txt", "application/octet-stream") { Content = File.ReadAllBytes("HelloWorld.txt") });
                mailMessage.Attachments.Add(new MailAttachment("HelloWorld.jpg", "application/octet-stream") { Content = File.ReadAllBytes("HelloWorld.jpg") });
                mailMessage.To.Add(new IService.MailAddress("niyouzhu@gyzet.com"));
                client.Send(mailMessage);
            }
        }

        [TestMethod]
        public void TestApp()
        {
            using (var app = new App())
            {
                app.Run();
                Thread.Sleep(1000000);
            }
        }

        [TestMethod]
        public void TestAppWithoutProgramming()
        {
            Thread.Sleep(1000000);
        }
    }
}
