/* В директории лежат файлы. По структуре они содержат три числа, разделенные пробелами. 
 * Первое число — целое, обозначает действие: 1- — умножение и 2 — деление. Остальные два — числа с плавающей точкой. 
 * Задача: написать многопоточное приложение, выполняющее эти действия над числами и сохраняющее результат в файл result.dat. 
 * Файлов в директории заведомо много.*/

using System;
using System.Collections.Generic;
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
            if (!Directory.Exists(dr_str))
            {
                Console.WriteLine($"В директории {dr_str} генерация файлов вида: \nПервое число — целое, обозначает действие: 1 — умножение и 2 — деление. Остальные два — числа с плавающей точкой. ");

                Directory.CreateDirectory(dr_str);
                for (int i = 0; i < __CountFiles; i++)
                {
                    await CreateFile(dr_str + "\\" + i + ".txt");
                    if (i % (__CountFiles / 100) == 0)
                        Console.Write("\r" + (i / (__CountFiles / 100)) + "%");
                }
                Console.WriteLine($"\rСоздано {__CountFiles} файла(ов)");
            }
            #endregion
            double[] results = new double[__CountFiles];
            for(int i = 0; i < __CountFiles; i++)
            {
                results[i] = await Calculate(dr_str + "\\" + i + ".txt");
                
                if (i % (__CountFiles / 100) == 0)
                    Console.Write("\r" + (i / (__CountFiles / 100)) + "%");
            }
            string result_file_name = Directory.GetCurrentDirectory() + "\\" + "result.txt";
            await WriteResultFile(result_file_name, results);
            Console.WriteLine("\rРезультаты записаны в " + result_file_name);
            Console.WriteLine("Для завершения нажмите любую кнопку...");
            Console.ReadKey();
        }

        private static Task WriteResultFile(string file_name, double[] results)
        {
            return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(file_name, false))
                {
                    for (int i = 0; i < __CountFiles; i++)
                        sw.WriteLine(i + " " + results[i]);
                }
            });
        }

        private static Task<double> Calculate(string file_name)
        {
            return Task.Run(() =>
            {
                using (StreamReader sr = new StreamReader(file_name))
                {
                    string[] values = sr.ReadLine().Split(' ');
                    if (values.Length != 3)
                        return -1;
                    bool.TryParse(values[0], out bool operation);//0-соответсвует знаку *, 1-соответсвует знаку :
                    double.TryParse(values[1], out double a);//числа с плавающей точкой
                    double.TryParse(values[2], out double b);
                    if (operation == false)
                        return a * b;
                    else
                    {
                        if (b != 0)
                            return a / b;
                        else
                            return -2;
                    }
                }
            });
        }
        private static Task CreateFile(string file_name)
        {
            Random rnd = new Random();
            return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(file_name))
                {
                    sw.Write(rnd.Next(0, 1) + " ");//0-соответсвует знаку *, 1-соответсвует знаку :
                    sw.Write(rnd.NextDouble() + " ");//числа с плавающей точкой
                    sw.Write(rnd.NextDouble());
                }
            });
        }
    }
}
