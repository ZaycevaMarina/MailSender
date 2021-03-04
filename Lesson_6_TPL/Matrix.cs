using System;
using System.Threading.Tasks;

namespace Lesson_6_TPL
{
    internal class MatrixForMultiply
    {
        private readonly int _Size;
        public int[][] Matrix { get; private set; }
        public MatrixForMultiply(int size)
        {
            _Size = size;
            Matrix = new int[_Size][];
            for (int i = 0; i < _Size; i++)
            {
                Matrix[i] = new int[_Size];
            }
        }
        public MatrixForMultiply InitRandom()
        {
            Random rnd = new Random();
            for (int i = 0; i < _Size; i++)
            {
                for (int j = 0; j < _Size; j++)
                    Matrix[i][j] = rnd.Next(0, 10);
            }
            return this;
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _Size; i++)
            {
                for (int j = 0; j < _Size; j++)
                {
                    if (j < _Size - 1)
                        result += Matrix[i][j] + " ";
                    else
                        result += Matrix[i][j];
                }
                if (i < _Size - 1)
                    result += "\n";
            }
            return result;
        }
        private int[] CalculateNewRow(int _i, MatrixForMultiply B)
        {
            int[] new_row = new int[_Size];
            for (int j = 0; j < _Size; j++)
                for (int i = 0; i < _Size; i++)
                    new_row[j] += Matrix[_i][i] * B.Matrix[i][j];
            return new_row;
        }
        public MatrixForMultiply Multiply(MatrixForMultiply B)
        {
            MatrixForMultiply C = new MatrixForMultiply(_Size);
            for (int i = 0; i < _Size; i++)
            {
                Task<int[]> task = Task.Factory.StartNew(() => CalculateNewRow(i, B));
                C.Matrix[i] = task.Result;
            }
            return C;
        }
    }
}
