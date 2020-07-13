using System.Configuration;

namespace EricNee.EmailSender.Business
{
    public class MailSettings : ConfigurationSection
    {
        [ConfigurationProperty("host")]
        public string Host { get { return (string)this["host"]; } set { this["host"] = value; } }
        [ConfigurationProperty("password")]

        public string Password { get { return (string)this["password"]; } set { this["password"] = value; } }
        [ConfigurationProperty("port", DefaultValue = 25)]

        public int Port { get { return (int)this["port"]; } set { this["port"] = value; } }
        [ConfigurationProperty("ssl", DefaultValue = false)]

        public bool SSL { get { return (bool)this["ssl"]; } set { this["ssl"] = value; } }
        [ConfigurationProperty("userName")]

        public string UserName { get { return (string)this["userName"]; } set { this["userName"] = value; } }
        [ConfigurationProperty("smtpInterval", DefaultValue = 0.1)]

        public double SmtpInterval { get { return (double)this["smtpInterval"]; } set { this["smtpInterval"] = value; } }

        public static MailSettings GetSettings()
        {
            return (MailSettings)ConfigurationManager.GetSection("mailSettings");
        }
    }

}