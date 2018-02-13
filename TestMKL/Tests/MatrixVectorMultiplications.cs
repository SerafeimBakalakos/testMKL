using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelMKL.ILP64;
using TestMKL.Benchmarks;

namespace TestMKL.Tests
{
    class MatrixVectorMultiplications
    {
        private const bool printAnyway = true;

        private static void TestFullMatrices()
        {
            bool error = true;
            int n = DenseMatrices.order;
            double[] x = DenseMatrices.x;

            double[] matrixPivot = Conversions.Array2DToFullRowMajor(DenseMatrices.matrixPivot);
            double[] matrixPivot_x = new double[n];
            CBlas.Dgemv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_TRANSPOSE.CblasNoTrans, n, n, 
                1, ref matrixPivot[0], n, ref x[0], 1, 0.0, ref matrixPivot_x[0], 1);
            error = CheckMultiplication(DenseMatrices.matrixPivot, x, DenseMatrices.matrixPivot_x, matrixPivot_x);

            double[] matrixSing = Conversions.Array2DToFullColumnMajor(DenseMatrices.matrixSingular);
            double[] matrixSing_x = new double[n];
            CBlas.Dgemv(CBLAS_LAYOUT.CblasColMajor, CBLAS_TRANSPOSE.CblasNoTrans, n, n,
                1, ref matrixSing[0], n, ref x[0], 1, 0.0, ref matrixSing_x[0], 1);
            error = CheckMultiplication(DenseMatrices.matrixSingular, x, DenseMatrices.matrixSing_x, matrixSing_x);

            double[] matrixPosDef = Conversions.Array2DToFullRowMajor(DenseMatrices.matrixPosdef);
            double[] matrixPosdef_x = new double[n];
            CBlas.Dgemv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_TRANSPOSE.CblasTrans, n, n,
                1, ref matrixPosDef[0], n, ref x[0], 1, 0.0, ref matrixPosdef_x[0], 1);
            error = CheckMultiplication(DenseMatrices.matrixPosdef, x, DenseMatrices.matrixPosdef_x, matrixPosdef_x);
        }

        private static void TestTriangularMatrices()
        {
            bool error = true;
            int n = TriangularMatrices.order;
            double[] x = TriangularMatrices.x;

            double[] lower = Conversions.Array2DToPackedLowerRowMajor(TriangularMatrices.lower);
            double[] lower_x = new double[n];
            Array.Copy(x, lower_x, n);
            CBlas.Dtpmv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasLower, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref lower[0], ref lower_x[0], 1);
            error = CheckMultiplication(TriangularMatrices.lower, x, TriangularMatrices.lower_x, lower_x);

            double[] lowerSing = Conversions.Array2DToPackedLowerColMajor(TriangularMatrices.lowerSing);
            double[] lowerSing_x = new double[n];
            Array.Copy(x, lowerSing_x, n);
            CBlas.Dtpmv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasLower, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref lowerSing[0], ref lowerSing_x[0], 1);
            error = CheckMultiplication(TriangularMatrices.lowerSing, x, TriangularMatrices.lowerSing_x, lowerSing_x);

            double[] upper = Conversions.Array2DToPackedUpperRowMajor(TriangularMatrices.upper);
            double[] upper_x = new double[n];
            Array.Copy(x, upper_x, n);
            CBlas.Dtpmv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasUpper, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref upper[0], ref upper_x[0], 1);
            error = CheckMultiplication(TriangularMatrices.upper, x, TriangularMatrices.upper_x, upper_x);

            double[] upperSing = Conversions.Array2DToPackedUpperColumnMajor(TriangularMatrices.upperSing);
            double[] upperSing_x = new double[n];
            Array.Copy(x, upperSing_x, n);
            CBlas.Dtpmv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasUpper, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref upperSing[0], ref upperSing_x[0], 1);
            error = CheckMultiplication(TriangularMatrices.upperSing, x, TriangularMatrices.upperSing_x, upperSing_x);
        }

        private static void TestSymmMatrices()
        {
            bool error = true;
            int n = SymmetricMatrices.order;
            double[] x = SymmetricMatrices.x;

            double[] matrixPosdef = Conversions.Array2DToPackedLowerRowMajor(SymmetricMatrices.matrixPosdef);
            double[] matrixPosdef_x = new double[n];
            CBlas.Dspmv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasLower, n,
                1.0, ref matrixPosdef[0], ref x[0], 1, 0.0, ref matrixPosdef_x[0], 1);
            error = CheckMultiplication(SymmetricMatrices.matrixPosdef, x, SymmetricMatrices.matrixPosdef_x, matrixPosdef_x);

            double[] matrixSing = Conversions.Array2DToPackedUpperColumnMajor(SymmetricMatrices.matrixSingular);
            double[] matrixSing_x = new double[n];
            CBlas.Dspmv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasUpper, n,
                1.0, ref matrixSing[0], ref x[0], 1, 0.0, ref matrixSing_x[0], 1);
            error = CheckMultiplication(SymmetricMatrices.matrixSingular, x, SymmetricMatrices.matrixSing_x, matrixSing_x);
        }

        private static bool CheckMultiplication(double[,] matrix, double[] x, double[] bExpected, double[] bComputed,
            double tol = 1e-13)
        {
            if (!Utilities.AreIdentical(bComputed, bExpected, tol))
            {
                PrintMultiplication(matrix, x, bExpected, bComputed, "INCORRECT");
                return true;
            }
            else if (printAnyway)
            {
                PrintMultiplication(matrix, x, bExpected, bComputed, "CORRECT");
            }
            return false;
        }

        private static void PrintMultiplication(double[,] matrix, double[] x, double[] bExpected, double[] bComputed, string result)
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("The following matrix multiplication is " +result + ":");
            Console.Write("A = ");
            Utilities.PrintArray(matrix);
            Console.WriteLine();
            Console.Write("x = ");
            Utilities.PrintArray(x);
            Console.WriteLine();
            Console.Write("b (expected) = ");
            Utilities.PrintArray(bExpected);
            Console.WriteLine();
            Console.Write("b (computed) = ");
            Utilities.PrintArray(bComputed);
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
        }

        public static void Main()
        {
            TestFullMatrices();
            TestTriangularMatrices();
            TestSymmMatrices();
        }
    }
}
