/* Задание 1.
  Написать приложение, считающее в раздельных потоках: 
а) факториал числа N, которое вводится с клавиатуры; 
б) сумму целых чисел до N. */

using System;
using System.Globalization;
using System.Threading;

namespace Lesson_5
{
    class Program
    {
        static int Count_threads = 5;
        static ulong[] Part_results = new ulong[Count_threads];
        static void Main(string[] args)
        {
            #region a)
            Console.WriteLine("a) Подсчёт факториал числа");
            Console.WriteLine("Введите положительное целое число N <= 20:");
            ulong N;
            while (!ulong.TryParse(Console.ReadLine(), out N) || N < 0 || N > 20)
            {
                Console.WriteLine("Введите положительное целое число N <= 20:");
            }

            for (int i_thread = 0; i_thread < Count_threads; i_thread ++)
            {
                int i_thread_current = i_thread;
                ulong n_min = (ulong)((ulong)i_thread_current * (N / (ulong)Count_threads));
                ulong n_max = (ulong)((ulong)i_thread_current + 1) * (N / (ulong)Count_threads);
                if (i_thread == Count_threads - 1)  //Подсчёт оставшихся чисел в последнем потоке
                    n_max = N + 1;  //Во время вычисления факториала последнее число не учитывается
                NumberOperation factorization_class = new NumberOperation(n_min, n_max, i_thread_current);
                Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod))
                {
                    Priority = ThreadPriority.Highest,
                    Name = $"Вторичный поток-подсчёт факториала от {n_min} до {n_max}"
                };
                thread.Start(factorization_class);
            }
            
            Console.WriteLine("Ждем окончания работ потоков");
            Console.ReadKey();
            ulong factorize_result = 1;
            for (int i = 0; i < Count_threads; i++)
                factorize_result *= Part_results[i];
            Console.WriteLine($"\rРезультат: {N}! = {factorize_result.ToString("#,#", new CultureInfo("ru-RU"))}");
            Console.WriteLine("Для переход в пункт б) нажмите любую кнопку...");
            Console.ReadKey();
            Console.Clear();
            #endregion
            #region b)
            Console.WriteLine("a) Подсчёт суммы чисел до заданного числа");
            Console.WriteLine("Введите положительное целое число N:");
            while (!ulong.TryParse(Console.ReadLine(), out N) || N < 0 )
            {
                Console.WriteLine("Введите положительное целое число:");
            }

            for (int i_thread = 0; i_thread < Count_threads; i_thread++)
            {
                int i_thread_current = i_thread;
                ulong n_min = (ulong)((ulong)i_thread_current * (N / (ulong)Count_threads));
                ulong n_max = (ulong)((ulong)i_thread_current + 1) * (N / (ulong)Count_threads);
                if (i_thread == Count_threads - 1)  //Подсчёт оставшихся чисел в последнем потоке
                    n_max = N + 1;  //Во время вычисления суммы последнее число не учитывается
                NumberOperation sum_class = new NumberOperation(n_min, n_max, i_thread_current);
                Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethodsum))
                {
                    Priority = ThreadPriority.Highest,
                    Name = $"Вторичный поток-подсчёт суммы от {n_min} до {n_max}"
                };
                thread.Start(sum_class);
            }

            Console.WriteLine("Ждем окончания работ потоков");
            Console.ReadKey();
            ulong sum_result = 0;
            for (int i = 0; i < Count_threads; i++)
                sum_result += Part_results[i];
            Console.WriteLine($"\rРезультат: сумма чисел равна {sum_result.ToString("#,#", new CultureInfo("ru-RU"))}");
            Console.WriteLine("Для завершения нажмите любую кнопку...");
            Console.ReadKey();
            #endregion
        }
        private static object lockObject = new object();
        static void ThreadMethod(object obj)
        {
            NumberOperation factorization_class = (NumberOperation)obj;
            factorization_class.Factorize();
            lock (lockObject)
            {
                Part_results[factorization_class._Id] = factorization_class.Result;
            }
            Console.WriteLine($"Поток '{Thread.CurrentThread.Name}' завершен.");
        }
        static void ThreadMethodsum(object obj)
        {
            NumberOperation sum_class = (NumberOperation)obj;
            sum_class.Sum();
            lock (lockObject)
            {
                Part_results[sum_class._Id] = sum_class.Result;                
            }
            Console.WriteLine($"Поток '{Thread.CurrentThread.Name}' завершен.{sum_class.Result}");
        }

        class NumberOperation
        {
            private ulong _N_min;
            private ulong _N_max;
            public int _Id { get; private set; }
            public ulong Result { get; private set; }
            public NumberOperation(ulong n_min, ulong n_max, int id)
            {
                _N_min = n_min;
                _N_max = n_max;
                _Id = id;
            }
            public void Factorize()
            {
                Result = 1;
                if (_N_min == 0)
                    _N_min = 1;
                for (ulong number = _N_min; number < _N_max; number++)
                    Result *= number;
            }
            public void Sum()
            {
                Result = 0;
                for (ulong number = _N_min; number < _N_max; number++)
                    Result += number;
            }
        }
    }
}
