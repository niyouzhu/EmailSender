﻿using System;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.EmailSender.IService
{
    [DataContract(Namespace = "http://me.zhuoyue.me")]
    [Serializable]
    public class MailAddress
    {
        public MailAddress(string address) : this(address, null)
        {

        }
        public MailAddress(string address, string displayName)
        {
            Address = address;
            DisplayName = displayName;

        }

        public MailAddress() { }
        //
        // Summary:
        //     Gets the e-mail address specified when this instance was created.
        //
        // Returns:
        //     A System.String that contains the e-mail address.
        [DataMember]
        public string Address { get; set; }

        //
        // Summary:
        //     Gets the display name composed from the display name and address information
        //     specified when this instance was created.
        //
        // Returns:
        //     A System.String that contains the display name; otherwise, System.String.Empty
        //     ("") if no display name information was specified when this instance was created.
        [DataMember]
        public string DisplayName { get; set; }

    }
}