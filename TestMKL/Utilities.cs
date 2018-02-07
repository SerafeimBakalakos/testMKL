using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKLtest
{
    static class Utilities
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
    }
}
