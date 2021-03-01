﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Text;

namespace CsvReader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static List<CsvObject> __CsvObjects = new List<CsvObject>();
        public void miOpenFileCsv_Click(object Sender, RoutedEventArgs E)
        {
            var open_dialog = new OpenFileDialog
            {
                Filter = "Csv (*.csv)|*.csv|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Выбор csv-файла для чтения"
            };

            if (open_dialog.ShowDialog() != true)
                return;

            var file_name = open_dialog.FileName;

            if (!File.Exists(file_name))
                return;

            var load_data_thread = new Thread(() => LoadData(file_name));
            load_data_thread.Start();
        }
        private void LoadData(string FileName)
        {
            var csv_objects = GetUsers(FileName);            
            Data.Dispatcher.Invoke(() => Data.ItemsSource = csv_objects);
        }

        private static IEnumerable<CsvObject> GetUsers(string file_name)
        {
            __CsvObjects.Clear();
            using (StreamReader sr = new StreamReader(file_name, System.Text.Encoding.UTF8))
            {                
                string csv_line = "";
                while ((csv_line = sr.ReadLine()) != null)
                {
                    string[] values = csv_line.Split(';');
                    if (values.Length < 4)
                        continue;
                    string[] some_info = null;
                    for (int i = 4; i < values.Length; i++)
                        some_info[i - 4] = values[i];
                    __CsvObjects.Add(new CsvObject(
                    values[0],
                    values[1],
                    values[2],
                    values[3],
                    some_info
                    ));
                }
            }
            return __CsvObjects;
        }
        public void miSaveFileAsTxt_Click(object Sender, RoutedEventArgs E)
        {
            var file_name = Environment.CurrentDirectory + "\\file.txt";
            if (File.Exists(file_name))
                File.Delete(file_name);

            var write_file_thread = new Thread(() => WriteTxtFile(file_name));
            write_file_thread.Start();
        }
        private void WriteTxtFile(string file_name)
        {
            if(__CsvObjects.Count == 0)
            {
                WriteStatus("Нет данных для сохранения в txt-файл");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (CsvObject csv_line in __CsvObjects)
            {
                sb.AppendLine(csv_line.ToCSV(';'));
            }
            WriteStatus("Сохранение txt-файла");
            using (StreamWriter sw = new StreamWriter(file_name, false, Encoding.UTF8))
            {
                sw.Write(sb.ToString());
            }
            WriteStatus("Txt-файл успешно сохранён");
        }
        public void mi_CreateFileCsv_Click(object Sender, RoutedEventArgs E)
        {
            var file_name = Environment.CurrentDirectory + "\\file.csv";
            if (File.Exists(file_name))
                File.Delete(file_name);            
            
            var write_file_thread = new Thread(() => WriteCsvFile(file_name, 100_000));
            write_file_thread.Start();
        }

        private void  WriteCsvFile(string file_name, int count)
        {
            List<CsvObject> list = new List<CsvObject>();
            string[] Emails = new string[] { "@yandex.ru", "@gmail.com", "@mail.ru", "outlook.com", "@yahoo.com", };
            Random rnd = new Random();
            WriteStatus("Генерация файла");
            for (int i = 0; i < count; i++)
            {
                list.Add(new CsvObject("Фамилия_" + i, "Имя_" + i, "Отчество_" + i, i + Emails[rnd.Next(0, Emails.Length)], null));
            }
            StringBuilder sb = new StringBuilder();
            foreach (CsvObject csv_line in list)
            {
                sb.AppendLine(csv_line.ToCSV(';'));
            }
            WriteStatus("Сохранение csv-файла");
            using (StreamWriter sw = new StreamWriter(file_name, false, Encoding.UTF8))
            {
                sw.Write(sb.ToString());
            }
            WriteStatus("Csv-файл успешно создан");
        }

        private void WriteStatus(string status_srt)
        {
            Data.Dispatcher.Invoke(() => tb_status.Text = status_srt);
        }

        class CsvObject
        {
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string Patronymic { get; private set; }
            public string Email { get; private set; }
            public string[] SomeInfo { get; private set; }

            public CsvObject(string first_name, string last_name, string patronymic, string email, string[] some_info)
            {
                FirstName = first_name;
                LastName = last_name;
                Patronymic = patronymic;
                Email = email;
                SomeInfo = some_info;
            }

            public string ToCSV(char delimiter)
            {
                if (delimiter != ';' && delimiter != ',')
                    delimiter = ';';
                string str_some_info = "";
                if (SomeInfo != null)
                {
                    for (int i = 0; i < SomeInfo.Length - 1; i++)
                        str_some_info += SomeInfo[i] + delimiter;
                    if (SomeInfo.Length > 0)
                        str_some_info += SomeInfo[SomeInfo.Length - 1];

                    return $"{FirstName}{delimiter}" +
                        $"{LastName}{delimiter}" +
                        $"{Patronymic}{delimiter}" +
                        $"{Email}{delimiter}" +
                        $"{str_some_info}";
                }
                else
                    return $"{FirstName}{delimiter}" +
                       $"{LastName}{delimiter}" +
                       $"{Patronymic}{delimiter}" +
                       $"{Email}";
            }
        }
    }
}
