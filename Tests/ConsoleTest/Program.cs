using System;
using System.Net;
using System.Net.Mail;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = new MailAddress("_@yandex.ru");
            var to = new MailAddress("_@yahoo.com");

            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            var client = new SmtpClient("smtp.yandex.ru", 587);//("smtp.mail.yahoo.com", 465);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "_@yandex.ru",
                //SecurePassword = PasswordEdit.SecurePassword
                Password = "="
            };

            try
            {
                client.Send(message);

                Console.WriteLine("Почта успешно отправлена!");

            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Ошибка авторизации " + ex.Message);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Ошибка адреса сервера");
            }
            Console.ReadLine();
        }
    }
}