﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL
{
    static class Conversions
    {
        public static double[] Array2DToFullRowMajor(double[,] array2D)
        {
            int numRows = array2D.GetLength(0);
            int numColumns = array2D.GetLength(1);
            double[] array1D = new double[numRows*numColumns];
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    array1D[i * numColumns + j] = array2D[i, j]; 
                }
            }
            return array1D;
        }

        public static double[] Array2DToFullColumnMajor(double[,] array2D)
        {
            int numRows = array2D.GetLength(0);
            int numColumns = array2D.GetLength(1);
            double[] array1D = new double[numRows * numColumns];
            for (int j = 0; j < numColumns; ++j)
            {
                for (int i = 0; i < numRows; ++i)
                {
                    array1D[j * numRows + i] = array2D[i, j];
                }
            }
            return array1D;
        }

        public static double[] Array2DToPackedLowerRowMajor(double[,] array2D)
        {
            if (array2D.GetLength(0) != array2D.GetLength(1))
            {
                throw new ArgumentException("The provided matrix is not square");
            }
            int n = array2D.GetLength(0);
            double[] array1D = new double[(n * (n+1)) / 2];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    array1D[counter] = array2D[i, j];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array1D;
        }

        public static double[] Array2DToPackedLowerColumnMajor(double[,] array2D)
        {
            if (array2D.GetLength(0) != array2D.GetLength(1))
            {
                throw new ArgumentException("The provided matrix is not square");
            }
            int n = array2D.GetLength(0);
            double[] array1D = new double[(n * (n + 1)) / 2];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int j = 0; j < n; ++j)
            {
                for (int i = j; i < n; ++i)
                {
                    array1D[counter] = array2D[i, j];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array1D;
        }

        public static double[] Array2DToPackedUpperRowMajor(double[,] array2D)
        {
            if (array2D.GetLength(0) != array2D.GetLength(1))
            {
                throw new ArgumentException("The provided matrix is not square");
            }
            int n = array2D.GetLength(0);
            double[] array1D = new double[(n * (n + 1)) / 2];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    array1D[counter] = array2D[i, j];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array1D;
        }

        public static double[] Array2DToPackedUpperColumnMajor(double[,] array2D)
        {
            if (array2D.GetLength(0) != array2D.GetLength(1))
            {
                throw new ArgumentException("The provided matrix is not square");
            }
            int n = array2D.GetLength(0);
            double[] array1D = new double[(n * (n + 1)) / 2];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int j = 0; j < n; ++j)
            {
                for (int i = 0; i <= j; ++i)
                {
                    array1D[counter] = array2D[i, j];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array1D;
        }

        public static double[,] FullRowMajorToArray2D(double[] array1D, int numRows, int numColumns)
        {
            double[,] array2D = new double[numRows, numColumns];
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    array2D[i, j] = array1D[i * numColumns + j];
                }
            }
            return array2D;
        }

        public static double[,] FullColumnMajorToArray2D(double[] array1D, int numRows, int numColumns)
        {
            double[,] array2D = new double[numRows, numColumns];
            for (int j = 0; j < numColumns; ++j)
            {
                for (int i = 0; i < numRows; ++i)
                {
                    array2D[i, j] = array1D[j * numRows + i];
                }
            }
            return array2D;
        }

        public static double[,] PackedLowerRowMajorToArray2D(double[] array1D)
        {
            int n = PackedLengthToOrder(array1D.Length);
            double[,] array2D = new double[n, n];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    array2D[i, j] = array1D[counter];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array2D;
        }

        public static double[,] PackedLowerColumnMajorToArray2D(double[] array1D)
        {
            int n = PackedLengthToOrder(array1D.Length);
            double[,] array2D = new double[n, n];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int j = 0; j < n; ++j)
            {
                for (int i = j; i < n; ++i)
                {
                    array2D[i, j] = array1D[counter];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array2D;
        }

        public static double[,] PackedUpperRowMajorToArray2D(double[] array1D)
        {
            int n = PackedLengthToOrder(array1D.Length);
            double[,] array2D = new double[n, n];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int i = 0; i < n; ++i)
            {
                for (int j = i; j < n; ++j)
                {
                    array2D[i, j] = array1D[counter];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array2D;
        }

        public static double[,] PackedUpperColumnMajorToArray2D(double[] array1D)
        {
            int n = PackedLengthToOrder(array1D.Length);
            double[,] array2D = new double[n, n];
            int counter = 0; // Simplifies indexing but the outer and inner loops cannot be interchanged
            for (int j = 0; j < n; ++j)
            {
                for (int i = 0; i <= j; ++i)
                {
                    array2D[i, j] = array1D[counter];
                    ++counter; // Clearer than post-incrementing during indexing.
                }
            }
            return array2D;
        }

        public static double[,] Array2DLowerToSymmetric(double[,] array2D)
        {
            if (array2D.GetLength(0) != array2D.GetLength(1))
            {
                throw new ArgumentException("The provided matrix is not square");
            }
            int n = array2D.GetLength(0);
            double[,] symm = new double[n, n];
            Array.Copy(array2D, symm, n * n);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    symm[j, i] = symm[i, j];
                }
            }
            return symm;
        }

        public static double[,] Array2DUpperToSymmetric(double[,] array2D)
        {
            if (array2D.GetLength(0) != array2D.GetLength(1))
            {
                throw new ArgumentException("The provided matrix is not square");
            }
            int n = array2D.GetLength(0);
            double[,] symm = new double[n, n];
            Array.Copy(array2D, symm, n * n);
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    symm[i, j] = symm[j, i];
                }
            }
            return symm;
        }

        private static int PackedLengthToOrder(int length)
        {
            // length = n*(n+1)/2 => n = ( -1+sqrt(1+8*length) )/2
            double n = (-1.0 + Math.Sqrt(1 + 8 * length)) / 2;
            int order = (int)Math.Round(n);
            if (n*(n+1)/2 != length)
            {
                throw new ArgumentException("The length of the 1D array must be an integer L such that L=n*(n+1)/2");
            }
            return order;
        }
    }
}
