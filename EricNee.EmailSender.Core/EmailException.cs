using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.Core
{
    public class EmailException : Exception
    {
        public EmailException(Exception ex) : this(null, ex)
        {

        }

        public EmailException(string msg, Exception ex) : base(msg, ex)
        {

        }

        public EmailException(string msg) : base(msg) { }
    }
}
