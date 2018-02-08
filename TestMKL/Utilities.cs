﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL
{
    public static class Utilities
    {
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

        public static bool AreIdentical(double[] a, double[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }

        public static bool AreIdentical(double[,] a, double[,] b)
        {
            if (a.GetLength(0) != b.GetLength(0)) return false;
            if (a.GetLength(1) != b.GetLength(1)) return false;
            for (int i = 0; i < a.GetLength(0); ++i)
            {
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    if (a[i, j] != b[i, j]) return false;

                }
            }
            return true;
        }
    }
}
