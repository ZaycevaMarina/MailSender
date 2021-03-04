/* В директории лежат файлы. По структуре они содержат три числа, разделенные пробелами. 
 * Первое число — целое, обозначает действие: 1- — умножение и 2 — деление. Остальные два — числа с плавающей точкой. 
 * Задача: написать многопоточное приложение, выполняющее эти действия над числами и сохраняющее результат в файл result.dat. 
 * Файлов в директории заведомо много.*/

using System;
using System.IO;
using System.Threading.Tasks;

namespace Lesson_6_Async
{
    class Program
    {
        private static string __FileDirectoryName = "Files";
        private static int __CountFiles = 1000;
        static async Task Main(string[] args)
        {
            
            #region Генерация файлов
            string dr_str = Directory.GetCurrentDirectory() + "\\" + __FileDirectoryName;
            Console.WriteLine($"В директории {dr_str} генерация файлов вида: \nПервое число — целое, обозначает действие: 1 — умножение и 2 — деление. Остальные два — числа с плавающей точкой. ");
            if (Directory.Exists(dr_str))
                Directory.Delete(dr_str);
            Directory.CreateDirectory(dr_str);
            for (int i = 0; i < __CountFiles; i++)
            {
                await CreateFile(dr_str + "\\" + i + ".txt");
                if (i % (__CountFiles / 100) == 0)
                    Console.Write("\r" + (i / (__CountFiles / 100)) + "%");
            }
            Console.WriteLine($"\rСоздано {__CountFiles} файла(ов)");
            #endregion
            Console.WriteLine("Для завершения нажмите любую кнопку...");
            Console.ReadKey();
        }
        private static Task CreateFile(string file_name)
        {
            Random rnd = new Random();
            return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(file_name))//))
                {
                    sw.Write(rnd.Next(0, 1) + " ");//0-соответсвует знаку *, 1-соответсвует знаку :
                    sw.Write(rnd.NextDouble() + " ");//числа с плавающей точкой
                    sw.Write(rnd.NextDouble());
                }
            });
        }
    }
}
