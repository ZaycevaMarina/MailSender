using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lesson_6_TPL
{
    public partial class MainWindow : Window
    {
        private readonly int _Size = 100;
        private MatrixForMultiply A;
        private MatrixForMultiply B;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void Load(object sender, RoutedEventArgs e)
        {
            A = new MatrixForMultiply(_Size);
            B = new MatrixForMultiply(_Size);
            Task< MatrixForMultiply> task1 = new Task<MatrixForMultiply>(() => A.InitRandom());
            task1.Start();
            Task<MatrixForMultiply> task2 = new Task<MatrixForMultiply>(() => B.InitRandom());
            task2.Start();
            A = task1.Result;
            B = task2.Result;
            tbMatrix1.Text = A.ToString();
            tbMatrix2.Text = B.ToString();
        }
        public void btGenerate_Click(object Sender, RoutedEventArgs E)
        {
            tbMatrix1.Text = "";
            tbMatrix2.Text = "";
            tbStatus.Text = "Генерация двух матриц";
            Task<MatrixForMultiply> task1 = new Task<MatrixForMultiply>(() => A.InitRandom());
            task1.Start();
            Task<MatrixForMultiply> task2 = new Task<MatrixForMultiply>(() => B.InitRandom());
            task2.Start();
            A = task1.Result;
            B = task2.Result;
            tbMatrix1.Text = A.ToString();
            tbMatrix2.Text = B.ToString();
            tbStatus.Text = "Завершена генерация двух матриц";
        }
        public void btCaculate_Click(object Sender, RoutedEventArgs E)
        {
            tbMatrix3.Text = "";
            tbStatus.Text = "Вычисление произведения двух матриц";
            MatrixForMultiply C = A.Multiply(B);
            tbMatrix3.Text = C.ToString();
            tbStatus.Text = "Завершено вычисление произведения двух матриц";
        }
    }
}
