﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EricNee.EmailSender.Service;
using System.Net.Mail;
using System.Threading;
using EricNee.EmailSender.IService;

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
                var mailMessage = new EmailMessage() { MessageId = Guid.NewGuid(), Subject = "Hello World", IsHtml = true, CreatedTime = DateTime.Now, From = new IService.MailAddress("Foo@er.com"), Body = "Foobar" };
                mailMessage.To.Add(new IService.MailAddress("Bar@he.com"));
                client.Send(mailMessage);
            }
        }
    }
}
