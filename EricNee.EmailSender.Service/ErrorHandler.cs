using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace EricNee.EmailSender.Service
{
    public class ErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var fe = new FaultException("There is an error occurred during call, if you want to know more detail please contact Admin.");
            var mf = fe.CreateMessageFault();
            fault = Message.CreateMessage(version, mf, fe.Action);
            Trace.WriteLine(error,"EmailService");
        }
    }
}
