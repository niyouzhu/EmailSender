using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EricNee.EmailSender.Service;
using System.Net.Mail;
using System.Threading;

namespace EricNee.EmailSender.Test
{
    [TestClass]
    public class ServiceTest
    {



        [TestMethod]
        public void TestService()
        {
            var app = new App();
            app.Run();
            Thread.Sleep(1000 * 60 * 10);

        }
    }
}
