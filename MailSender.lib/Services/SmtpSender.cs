using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender.Services
{
    public class SmtpSender
    {
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public void SendMessage(string SenderAddress, string RecipientAddress, string Subject, string Body)
        {
            MailAddress mail_address_from = new MailAddress(SenderAddress);
            MailAddress mail_address_to = new MailAddress(RecipientAddress);
            using MailMessage message = new MailMessage(mail_address_from, mail_address_to)
            {
                Subject = Subject,
                Body = Body
            };
            using var client = new SmtpClient(ServerAddress, ServerPort)
            {
                EnableSsl = UseSSL,
                Credentials = new NetworkCredential
                {
                    UserName = Login,
                    Password = Password
                }
            };
            client.Send(message);
        }
    }
}
