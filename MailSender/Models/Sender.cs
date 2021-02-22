using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    class Sender:User
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }
}
