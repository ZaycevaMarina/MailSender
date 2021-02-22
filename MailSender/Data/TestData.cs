using System.Collections.Generic;
using System.Linq;
using MailSender.Models;


namespace MailSender.Data
{
    class TestData
    {
        public static IList<Server> Servers { get; } = new List<Server>
         {
             new Server
             {
                 Id = 1,
                 Name = "Яндекс",
                 Address = "smpt.yandex.ru",
                 Port = 567,
                 UseSSL = true,
                 Login = "username@yandex.ru",
                 Password = "PassWord",
             },
             new Server
             {
                 Id = 2,
                 Name = "Mail",
                 Address = "smpt.mail.com",
                 Port = 465,
                 UseSSL = true,
                 Login = "username@mail.ru",
                 Password = "PassWord",
             },
             new Server
             {
                 Id = 2,
                 Name = "Yahoo",
                 Address = "smpt.mail.yahoo.com",
                 Port = 465,
                 UseSSL = true,
                 Login = "username@yahoo.com",
                 Password = "PassWord",
             },
             new Server
             {
                 Id = 2,
                 Name = "gMail",
                 Address = "smpt.gmail.com",
                 Port = 465,
                 UseSSL = true,
                 Login = "username@gmail.com",
                 Password = "PassWord",
             }
        };
        public static IList<Sender> Senders { get; } = new List<Sender>
         {
             new Sender
             {
                 Id = 1,
                 Name = "Марина З.",
                 Address = "mari_minflash@yahoo.com",
                 Comment = "Почта от Марины З."
                 },
                 new Sender
                 {
                 Id = 2,
                 Name = "Марина Н.",
                 Address = "mnovachuk@yandex.ru",
                 Comment = "Почта от Марины Н."
             },
         };
        public static IList<Recipient> Recipients { get; } = new List<Recipient>
         {
             new Recipient
             {
                 Id = 1,
                 Name = "Аксёнов",
                 Address = "aksyenov@yandex.ru",
                 Comment = "Почта для Аксёнова"
             },
             new Recipient
             {
                 Id = 2,
                 Name = "Попов",
                 Address = "petrov@yahoo.com",
                 Comment = "Почта для Попова"
             },
             new Recipient
             {
                 Id = 3,
                 Name = "Кузнецов",
                 Address = "kyznetsov@gmail.com",
                 Comment = "Почта для Кузнецова"
             },
         };
        public static IList<Letter> Messages { get; } = Enumerable
        .Range(1, 10)
        .Select(i => new Letter
        {
            Id = i,
            Title = $"Тема {i}",
            Text = $"Текст {i}"
        })
        .ToList();

        public static IList<Port> Ports { get; } = new List<Port> 
        { 
            new Port { ServerPort = 25 },
            new Port { ServerPort = 465 },
            new Port { ServerPort = 587 },
        };
    }
}
