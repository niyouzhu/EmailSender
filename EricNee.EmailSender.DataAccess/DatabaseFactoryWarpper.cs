using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.DataAccess
{
    public class DatabaseFactoryWarpper
    {
        public static Database CreateInstance()
        {
            return DatabaseFactory.CreateDatabase("EmailSender");
        }
    }
}
