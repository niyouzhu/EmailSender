using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Business
{
    public class ExceptionEventArgs : EventArgs
    {
        public ExceptionEventArgs(Exception ex) : base()
        {
            Ex = ex;
        }

        public Exception Ex { get; }
    }
}
