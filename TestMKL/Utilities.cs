using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL
{
    public static class Utilities
    {
        public static bool AreIdentical(double[] a, double[] b, double tolerance = 1e-13)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; ++i)
            {
                if (Math.Abs(a[i] - b[i]) > tolerance) return false;
            }
            return true;
        }

        public static bool AreIdentical(double[,] a, double[,] b, double tolerance = 1e-13)
        {
            if (a.GetLength(0) != b.GetLength(0)) return false;
            if (a.GetLength(1) != b.GetLength(1)) return false;
            for (int i = 0; i < a.GetLength(0); ++i)
            {
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    if (Math.Abs(a[i, j] - b[i, j]) > tolerance) return false;

                }
            }
            return true;
        }

        public static void PrintArray(double[] array, string separator = " ")
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; ++i)
            {
                Console.Write(separator + array[i]);
            }
            Console.WriteLine(separator + "]");
        }

        public static void PrintArray(double[,] array, string rowSeparator = "\n", string colSeparator = " ")
        {
            Console.Write("[");
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                Console.Write(rowSeparator + "[");
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    Console.Write(colSeparator + array[i, j]);
                }
                Console.Write(colSeparator + "]");
            }
            Console.WriteLine(rowSeparator + "]");
        }

        public static double[] Round(double[] array, int decimals)
        {
            double[] rounded = new double[array.Length];
            for (int i = 0; i < array.Length; ++i)
            {
                rounded[i] = Math.Round(array[i], decimals);
            }
            return rounded;
        }

        public static double[,] Round(double[,] array, int decimals)
        {
            double[,] rounded = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    rounded[i, j] = Math.Round(array[i, j], decimals);
                }
            }
            return rounded;
        }

        public static double[] MatrixTimesVector(double[,] matrix, double[] vector)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            if (vector.Length != n) throw new ArgumentException("Invalid dimensions");
            double[] result = new double[m];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }
            return result;
        }
    }
}
