using EricNee.EmailSender.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EricNee.EmailSender.Server.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            app.Run();
            Thread.Sleep(100000);

        }
    }
}
