using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    public class Recipient:User
    {
        public int Id { get; set; }
        public string TypeOfDelivery { get; set; }
        public string Comment { get; set; }
    }
}
