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
            // Определим от кого и кому будем отправлять почту
            // Тут надо подставить рабочие адреса электронной почты
            var sender = new MailAddress("novachuk-lily@yandex.ru", "Lika П.");
            var recipient = new MailAddress("mnovachuk@yandex.ru", "Марина Н.");
            // Создадим объект сообщения указав отправителя и адресата
            using var message = new MailMessage(sender, recipient)
            {
                // а также заполним свойства заголовка и тела сообщения
                Subject = "Тестовое сообщение",
                Body = $"Текст письма: {DateTime.Now}",
            };
            // Создадим клиента для общения с почтовым сервером по протоколу SMTP
            // В конструкторе надо указать адрес сервера и порт (по умолчанию 25)
            using (var client = new SmtpClient("smtp.yandex.ru", 25))
            {
                const string login = "novachuk-lily@yandex.ru";
                const string password = "GHE4rjdf";
                // Обычно сервер не разрешает анонимам отправлять почту
                // Нам необходимо добавить информацию с нашими учётными данными
                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = true;
                // После чего можем попросить клиента открыть канал с сервером
                // и отправить наше сообщение
                try
                {
                    client.Send(message);

                    Console.WriteLine("Почта успешно отправлена!");

                }
                catch (SmtpException)
                {
                    Console.WriteLine("Ошибка авторизации");
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("Ошибка адреса сервера");
                }
            }
            Console.ReadLine();
        }
    }
}

/*var from = new MailAddress("shmachilin@yandex.ru", "Павел");
            var to = new MailAddress("shmachilin@gmail.com", "Павел");

            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            var client = new SmtpClient("smtp.yandex.ru", 465);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = "UserName",
                //SecurePassword = 
                Password = "PAssword!"
            };

            client.Send(message);*/
