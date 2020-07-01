using System.Configuration;

namespace EricNee.EmailSender.Business
{
    public class MailSettings : ConfigurationSection
    {
        [ConfigurationProperty("Host")]
        public string Host { get { return (string)this["Host"]; } set { this["Host"] = value; } }
        [ConfigurationProperty("Password")]

        public string Password { get { return (string)this["Password"]; } set { this["Password"] = value; } }
        [ConfigurationProperty("Port", DefaultValue = 25)]

        public int Port { get { return (int)this["Port"]; } set { this["Port"] = value; } }
        [ConfigurationProperty("SSL", DefaultValue = false)]

        public bool SSL { get { return (bool)this["SSL"]; } set { this["SSL"] = value; } }
        [ConfigurationProperty("UserName")]

        public string UserName { get { return (string)this["UserName"]; } set { this["UserName"] = value; } }
        [ConfigurationProperty("SmtpInterval", DefaultValue = 0.1)]

        public double SmtpInterval { get { return (double)this["SmtpInterval"]; } set { this["SmtpInterval"] = value; } }

        public static MailSettings GetSettings()
        {
            return (MailSettings)ConfigurationManager.GetSection("MailSettings");
        }
    }

}