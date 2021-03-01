using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    public abstract class User
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
