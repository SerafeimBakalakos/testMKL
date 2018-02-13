using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMKL.Matrices;

namespace TestMKL.Tests
{
    class MatrixPerformance
    {
        public static void Main()
        {
            int repetitions = 1000000;
            int order = 1000;
            double[,] data = new double[order, order];
            var matrix2D = new Matrix2D(data);
            var matrixRow = new MatrixRowMajor(data);
            var matrixCol = new MatrixColMajor(data);

            long timeRead2D = 0;
            long timeWrite2D = 0;
            long timeReadRowMajor = 0;
            long timeWriteRowMajor = 0;
            long timeReadColMajor = 0;
            long timeWriteColMajor = 0;

            Stopwatch sw = new Stopwatch();
            double val = 10.0;
            double aVariable = 3.0;
            for (int r = 0; r < repetitions; ++r) //Interleaved repetitions to distribute cpu fluctuations more evenly
            {
                // Time reads with a 2D array
                sw.Reset();
                sw.Start();
                for (int i = 0; i < matrix2D.numRows; ++i)
                {
                    for (int j = 0; j < matrix2D.numCols; ++j)
                    {
                        aVariable = matrix2D[i, j];
                    }
                }
                sw.Stop();
                timeRead2D += sw.ElapsedMilliseconds;

                // Time writes with a 2D array
                sw.Reset();
                sw.Start();
                for (int i = 0; i < matrix2D.numRows; ++i)
                {
                    for (int j = 0; j < matrix2D.numCols; ++j)
                    {
                        matrix2D[i, j] = val;
                    }
                }
                sw.Stop();
                timeWrite2D += sw.ElapsedMilliseconds;

                // Time reads with a row major array
                sw.Reset();
                sw.Start();
                for (int i = 0; i < matrixRow.numRows; ++i)
                {
                    for (int j = 0; j < matrixRow.numCols; ++j)
                    {
                        aVariable = matrixRow[i, j];
                    }
                }
                sw.Stop();
                timeReadRowMajor += sw.ElapsedMilliseconds;

                // Time writes with a row major array
                sw.Reset();
                sw.Start();
                for (int i = 0; i < matrixRow.numRows; ++i)
                {
                    for (int j = 0; j < matrixRow.numCols; ++j)
                    {
                        matrixRow[i, j] = val;
                    }
                }
                sw.Stop();
                timeWriteRowMajor += sw.ElapsedMilliseconds;

                // Time reads with a col major array
                sw.Reset();
                sw.Start();
                for (int i = 0; i < matrixCol.numRows; ++i)
                {
                    for (int j = 0; j < matrixCol.numCols; ++j)
                    {
                        aVariable = matrixCol[i, j];
                    }
                }
                sw.Stop();
                timeReadColMajor += sw.ElapsedMilliseconds;

                // Time writes with a col major array
                sw.Reset();
                sw.Start();
                for (int i = 0; i < matrixCol.numRows; ++i)
                {
                    for (int j = 0; j < matrixCol.numCols; ++j)
                    {
                        matrixCol[i, j] = val;
                    }
                }
                sw.Stop();
                timeWriteColMajor += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Average time for 10^6 reads with 2D array = {0} ms.", timeRead2D / (double)repetitions);
            Console.WriteLine("Average time for 10^6 writes with 2D array = {0} ms.", timeWrite2D / (double)repetitions);
            Console.WriteLine("Average time for 10^6 reads with row major 1D = {0} ms.", timeReadRowMajor / (double)repetitions);
            Console.WriteLine("Average time for 10^6 writes with row major 1D array = {0} ms.", timeWriteRowMajor / (double)repetitions);
            Console.WriteLine("Average time for 10^6 reads with col major 1D = {0} ms.", timeReadColMajor / (double)repetitions);
            Console.WriteLine("Average time for 10^6 writes with col major 1D array = {0} ms.", timeWriteColMajor / (double)repetitions);
        }
    }
}
