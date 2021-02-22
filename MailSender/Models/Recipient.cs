using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    class Recipient:User
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
