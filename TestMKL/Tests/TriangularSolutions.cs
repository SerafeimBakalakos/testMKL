using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelMKL.ILP64;
using TestMKL.Benchmarks;

namespace TestMKL.Tests
{
    class TriangularSolutions
    {
        private const bool printAnyway = true;

        private static void TestFullMatrices()
        {
            bool error = true;
            int n = TriangularMatrices.order;
            double[] x = new double[n];

            double[] lower = Conversions.Array2DToFullRowMajor(TriangularMatrices.lower);
            Array.Copy(TriangularMatrices.lower_x, x, n);
            CBlas.Dtrsv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasLower, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref lower[0], n, ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.lower, TriangularMatrices.lower_x, TriangularMatrices.x, x);

            // This should fail
            double[] lowerSing = Conversions.Array2DToFullColMajor(TriangularMatrices.lowerSing);
            x = new double[n];
            Array.Copy(TriangularMatrices.lowerSing_x, x, n);
            CBlas.Dtrsv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasLower, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref lower[0], n, ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.lowerSing, TriangularMatrices.lowerSing_x, TriangularMatrices.x, x);

            double[] upper = Conversions.Array2DToFullColMajor(TriangularMatrices.upper);
            x = new double[n];
            Array.Copy(TriangularMatrices.upper_x, x, n);
            CBlas.Dtrsv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasUpper, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
            n, ref upper[0], n, ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.upper, TriangularMatrices.upper_x, TriangularMatrices.x, x);

            // This should fail
            double[] upperSing = Conversions.Array2DToFullRowMajor(TriangularMatrices.upperSing);
            x = new double[n];
            Array.Copy(TriangularMatrices.upperSing_x, x, n);
            CBlas.Dtrsv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasUpper, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref upper[0], n, ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.upperSing, TriangularMatrices.upperSing_x, TriangularMatrices.x, x);
        }

        private static void TestPackedMatrices()
        {
            bool error = true;
            int n = TriangularMatrices.order;
            double[] x = new double[n];

            double[] lower = Conversions.Array2DToPackedLowerRowMajor(TriangularMatrices.lower);
            Array.Copy(TriangularMatrices.lower_x, x, n);
            CBlas.Dtpsv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasLower, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref lower[0], ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.lower, TriangularMatrices.lower_x, TriangularMatrices.x, x);

            // This should fail
            double[] lowerSing = Conversions.Array2DToPackedLowerColMajor(TriangularMatrices.lowerSing);
            x = new double[n];
            Array.Copy(TriangularMatrices.lowerSing_x, x, n);
            CBlas.Dtpsv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasLower, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref lower[0], ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.lowerSing, TriangularMatrices.lowerSing_x, TriangularMatrices.x, x);

            double[] upper = Conversions.Array2DToPackedUpperColMajor(TriangularMatrices.upper);
            x = new double[n];
            Array.Copy(TriangularMatrices.upper_x, x, n);
            CBlas.Dtpsv(CBLAS_LAYOUT.CblasColMajor, CBLAS_UPLO.CblasUpper, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
            n, ref upper[0], ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.upper, TriangularMatrices.upper_x, TriangularMatrices.x, x);

            // This should fail
            double[] upperSing = Conversions.Array2DToPackedUpperRowMajor(TriangularMatrices.upperSing);
            x = new double[n];
            Array.Copy(TriangularMatrices.upperSing_x, x, n);
            CBlas.Dtpsv(CBLAS_LAYOUT.CblasRowMajor, CBLAS_UPLO.CblasUpper, CBLAS_TRANSPOSE.CblasNoTrans, CBLAS_DIAG.CblasNonUnit,
                n, ref upper[0], ref x[0], 1);
            error = CheckSubstitution(TriangularMatrices.upperSing, TriangularMatrices.upperSing_x, TriangularMatrices.x, x);
        }

        private static bool CheckSubstitution(double[,] matrix, double[] b, double[] xExpected, double[] xComputed,
            double tol = 1e-13)
        {
            if (!Utilities.AreIdentical(xComputed, xExpected, tol))
            {
                PrintSubstitution(matrix, b, xExpected, xComputed, "INCORRECT");
                return true;
            }
            else if (printAnyway)
            {
                PrintSubstitution(matrix, b, xExpected, xComputed, "CORRECT");
            }
            return false;
        }

        private static void PrintSubstitution(double[,] matrix, double[] b, double[] xExpected, double[] xComputed, 
            string result)
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("The following triangular solution is " + result + ":");
            Console.Write("A = ");
            Utilities.PrintArray(matrix);
            Console.WriteLine();
            Console.Write("b = ");
            Utilities.PrintArray(b);
            Console.WriteLine();
            Console.Write("x (expected) = ");
            Utilities.PrintArray(xExpected);
            Console.WriteLine();
            Console.Write("x (computed) = ");
            Utilities.PrintArray(xComputed);
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
        }

        public static void Main()
        {
            TestFullMatrices();
            TestPackedMatrices();
        }
    }
}
