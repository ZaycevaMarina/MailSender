using System;
// Пространства имён, содержащие нужные нам инструменты
using System.Net;
using System.Net.Mail;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = new MailAddress("mnovachuk@yandex.ru");
            var to = new MailAddress("mari_minflash@yahoo.com");

            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            var client = new SmtpClient("smtp.yandex.ru", 587);//("smtp.mail.yahoo.com", 465);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "mnovachuk@yandex.ru",//"mari_minflash@yahoo.com",//
                //SecurePassword = PasswordEdit.SecurePassword
                Password = "RiWa18,02"
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