using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelMKL.LP64;
using TestMKL.Benchmarks;

namespace TestMKL.Tests
{
    // TODO: Create appropriate exceptions (e.g. Lapack exception)
    // Also automate error checking and sentinel values. 
    // Even better, make shortcut methods that handle most error checking for common LAPACK funcs. 
    // +1 indirection layer but necessary. Also I can remove the need for a wraper library in the future.
    // TODO: These should be methods of benchmark objects/classes, one for each matrix. 
    // All MKL logic would be contained in a matrix class method. Most of the rest would be contained in utility functions.
    static class SystemSolutions
    {
        private const bool printAnyway = true;
        private const string errorMsg1 = "Stupid LAPACK changed my matrix dimensions!";
        private const double ZeroTolerance = 1e-13; // Perhaps a larger number is appropriate, since this will propagate during substitutions.
        private const int DefaultInfo = int.MinValue; // No way there are that many arguments

        public static void Main()
        {
            //SolveInvertible();
            //SolveSingular1();
            //SolveSingular2();
            //SolveSymmPosDef();
            SolveSymmSingular1();
        }

        public static void SolveInvertible()
        {
            int n = DenseMatrices.order;
            double[] matrix = Conversions.Array2DToFullColMajor(DenseMatrices.matrixPivot);
            int[] permutation = new int[n];
            double[] b = new double[n];
            Array.Copy(DenseMatrices.matrixPivot_x, b, n);

            //LU factorization
            int infoFact = DefaultInfo;
            Lapack.Dgetrf(ref n, ref n, ref matrix[0], ref n, ref permutation[0], ref infoFact);
            //if (n != DenseMatrices.order) throw new ApplicationException(errorMsg1);
            bool isInvertible = ProcessLUFactorizationInfo(infoFact, n, matrix);
            CheckLUFactorization(DenseMatrices.matrixPivot, DenseMatrices.matrixPivot_L, DenseMatrices.matrixPivot_U, matrix);

            //Triangular solutions
            if (!isInvertible)
            {
                Console.WriteLine("Matrix is invertible. Cannot solve the system.");
                return;
            }
            int infoSolve = DefaultInfo;
            int nRhs = 1; // rhs is a n x nRhs matrix, stored in b
            int ldb = n; // column major ordering: leading dimension of b is n 
            Lapack.Dgetrs("N", ref n, ref nRhs, ref matrix[0], ref n, ref permutation[0], ref b[0], ref ldb, ref infoSolve);
            ProcessSolutionInfo(infoSolve);
            CheckSolution(DenseMatrices.matrixPivot, DenseMatrices.matrixPivot_x, DenseMatrices.x, b);
        }

        public static void SolveSingular1()
        {
            int n = DenseMatrices.order;
            double[] matrix = Conversions.Array2DToFullColMajor(DenseMatrices.matrixSing1);
            int[] permutation = new int[n];
            double[] b = new double[n];
            Array.Copy(DenseMatrices.matrixSing1_x, b, n);

            //LU factorization
            int infoFact = DefaultInfo;
            Lapack.Dgetrf(ref n, ref n, ref matrix[0], ref n, ref permutation[0], ref infoFact);
            //if (n != DenseMatrices.order) throw new ApplicationException(errorMsg1);
            bool isInvertible = ProcessLUFactorizationInfo(infoFact, n, matrix); // This should throw an exception!!!!!
            CheckLUFactorization(DenseMatrices.matrixSing1, DenseMatrices.matrixSing1_L, DenseMatrices.matrixSing1_U, matrix);

            //Triangular solutions
            if (!isInvertible)
            {
                Console.WriteLine("Matrix is invertible. Cannot solve the system.");
                return;
            }
            int infoSolve = DefaultInfo;
            int nRhs = 1; // rhs is a n x nRhs matrix, stored in b
            int ldb = n; // column major ordering: leading dimension of b is n 
            Lapack.Dgetrs("N", ref n, ref nRhs, ref matrix[0], ref n, ref permutation[0], ref b[0], ref ldb, ref infoSolve);
            ProcessSolutionInfo(infoSolve);
            CheckSolution(DenseMatrices.matrixSing1, DenseMatrices.matrixSing1_x, DenseMatrices.x, b);
        }

        public static void SolveSingular2()
        {
            int n = DenseMatrices.order;
            double[] matrix = Conversions.Array2DToFullColMajor(DenseMatrices.matrixSing2);
            int[] permutation = new int[n];
            double[] b = new double[n];
            Array.Copy(DenseMatrices.matrixSing2_x, b, n);

            //LU factorization
            int infoFact = DefaultInfo;
            Lapack.Dgetrf(ref n, ref n, ref matrix[0], ref n, ref permutation[0], ref infoFact);
            //if (n != DenseMatrices.order) throw new ApplicationException(errorMsg1);
            bool isInvertible = ProcessLUFactorizationInfo(infoFact, n, matrix); // This should throw an exception!!!!!
            CheckLUFactorization(DenseMatrices.matrixSing2, DenseMatrices.matrixSing2_L, DenseMatrices.matrixSing2_U, matrix);

            //Triangular solutions
            if (!isInvertible)
            {
                Console.WriteLine("Matrix is invertible. Cannot solve the system.");
                return;
            }
            int infoSolve = DefaultInfo;
            int nRhs = 1; // rhs is a n x nRhs matrix, stored in b
            int ldb = n; // column major ordering: leading dimension of b is n 
            Lapack.Dgetrs("N", ref n, ref nRhs, ref matrix[0], ref n, ref permutation[0], ref b[0], ref ldb, ref infoSolve);
            ProcessSolutionInfo(infoSolve);
            CheckSolution(DenseMatrices.matrixSing2, DenseMatrices.matrixSing2_x, DenseMatrices.x, b);
        }

        public static void SolveSymmPosDef()
        {
            int n = SymmetricMatrices.order;
            double[] matrix = Conversions.Array2DToPackedUpperColMajor(SymmetricMatrices.matrixPosdef);
            double[] b = new double[n];
            Array.Copy(SymmetricMatrices.matrixPosdef_x, b, n);

            //Cholesky factorization
            int infoFact = DefaultInfo;
            Lapack.Dpptrf("U", ref n, ref matrix[0], ref infoFact);
            bool success = ProcessCholeskyFactorizationInfo(infoFact);
            double[,] uComputed = Conversions.PackedUpperColMajorToArray2D(matrix);

            //Triangular solutions
            if (!success)
            {
                Console.WriteLine("Matrix is not positive. Factorization may have failed. Please use LU instead.");
                return;
            }
            CheckCholeskyFactorization(SymmetricMatrices.matrixPosdef, SymmetricMatrices.matrixPosdef_U, uComputed);
            int infoSolve = DefaultInfo;
            int nRhs = 1; // rhs is a n x nRhs matrix, stored in b
            int ldb = n; // column major ordering: leading dimension of b is n 
            Lapack.Dpptrs("U", ref n, ref nRhs, ref matrix[0], ref b[0], ref ldb, ref infoSolve);
            ProcessSolutionInfo(infoSolve);
            CheckSolution(SymmetricMatrices.matrixPosdef, SymmetricMatrices.matrixPosdef_x, SymmetricMatrices.x, b);
        }

        public static void SolveSymmSingular1()
        {
            int n = SymmetricMatrices.order;
            double[] matrix = Conversions.Array2DToPackedUpperColMajor(SymmetricMatrices.matrixSing1);
            double[] b = new double[n];
            Array.Copy(SymmetricMatrices.matrixSing1_x, b, n);

            //Cholesky factorization
            int infoFact = DefaultInfo;
            Lapack.Dpptrf("U", ref n, ref matrix[0], ref infoFact);
            bool success = ProcessCholeskyFactorizationInfo(infoFact);
            double[,] uComputed = Conversions.PackedUpperColMajorToArray2D(matrix);

            //Triangular solutions
            if (!success)
            {
                Console.WriteLine("Matrix is not positive. Factorization may have failed. Please use LU instead.");
                return;
            }
            CheckCholeskyFactorization(SymmetricMatrices.matrixSing1, SymmetricMatrices.matrixSing1_U, uComputed);
            int infoSolve = DefaultInfo;
            int nRhs = 1; // rhs is a n x nRhs matrix, stored in b
            int ldb = n; // column major ordering: leading dimension of b is n 
            Lapack.Dpptrs("U", ref n, ref nRhs, ref matrix[0], ref b[0], ref ldb, ref infoSolve);
            ProcessSolutionInfo(infoSolve);
            CheckSolution(SymmetricMatrices.matrixSing1, SymmetricMatrices.matrixSing1_x, SymmetricMatrices.x, b);
        }

        public static void SolveSymmIndefinite()
        {
            throw new NotImplementedException();
        }

        //Returns true for invertible, false for singular
        private static bool ProcessLUFactorizationInfo(int info, int n, double[] lu)
        {
            if (info == DefaultInfo)
            {
                Console.WriteLine("After LU factorization: ");
                Utilities.PrintArray(Conversions.FullColMajorToArray2D(lu, n, n));
                throw new ApplicationException("The LAPACK call did not produce an info result. Something went wrong");
            }
            else if (info < 0)
            {
                
                string msg = string.Format("The {0}th parameter has an illegal value", (int)Math.Abs(info));
                throw new ArgumentException(msg);
            }
            else if (info > 0) 
            {
                //TODO: In an OOP world, this should be done using exceptions, not return values. 
                // The client would catch the SingularMatrixException if he wants to continue.
                // The other exceptions would still crush the program
                PrintMsgSingularFactor(info - 1);
                return false;
            }
            else if (Math.Abs(lu[n*n-1]) <= ZeroTolerance) //info = 0, but LAPACK doesn't check the last diagonal entry
            {
                PrintMsgSingularFactor(n-1);
                return false;
            }
            return true;
        }

        private static bool CheckLUFactorization(double[,] matrix, double[,] lExpected, double[,] uExpected,
            double[] luComputed, double tol = 1e-13)
        {
            double[,] lComputed = Conversions.FullLowerColMajorToArray2D(luComputed, true);
            double[,] uComputed = Conversions.FullUpperColMajorToArray2D(luComputed, false);
            bool isLCorrect = Utilities.AreIdentical(lExpected, lComputed, tol);
            bool isUCorrect = Utilities.AreIdentical(uExpected, uComputed, tol);
            if (!(isLCorrect && isUCorrect))
            {
                PrintFactorization(matrix, lExpected, uExpected, lComputed, uComputed, "INCORRECT");
                return true;
            }
            else if (printAnyway)
            {
                PrintFactorization(matrix, lExpected, uExpected, lComputed, uComputed, "CORRECT");
            }
            return false;
        }

        private static void PrintFactorization(double[,] matrix, double[,] lExpected, double[,] uExpected,
            double[,] lComputed, double[,] uComputed, string result)
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("The following LU factorization is " + result + ":");
            Console.Write("A = ");
            Utilities.PrintArray(matrix);
            Console.WriteLine();
            Console.Write("L (expected) = ");
            Utilities.PrintArray(lExpected);
            Console.WriteLine();
            Console.Write("L (computed) = ");
            Utilities.PrintArray(lComputed);
            Console.WriteLine();
            Console.Write("U (expected) = ");
            Utilities.PrintArray(uExpected);
            Console.WriteLine();
            Console.Write("U (computed) = ");
            Utilities.PrintArray(uComputed);
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
        }

        //Returns true for success, false if the matrix was not positive definite
        private static bool ProcessCholeskyFactorizationInfo(int info)
        {
            if (info == DefaultInfo)
            {
                Console.WriteLine("After Cholesky factorization: ");
                //Utilities.PrintArray(Conversions.FullColMajorToArray2D(lu, n, n));
                throw new ApplicationException("The LAPACK call did not produce an info result. Something went wrong");
            }
            else if (info < 0)
            {

                string msg = string.Format("The {0}th parameter has an illegal value", (int)Math.Abs(info));
                throw new ArgumentException(msg);
            }
            else if (info > 0)
            {
                //TODO: In an OOP world, this should be done using exceptions, not return values. 
                // The client would catch the SingularMatrixException if he wants to continue.
                // The other exceptions would still crush the program
                PrintMsgCholeskyFail(info - 1);
                return false;
            }
            return true;
        }

        private static bool CheckCholeskyFactorization(double[,] matrix, double[,] uExpected, double[,] uComputed,
            double tol = 1e-13)
        {
            if (!Utilities.AreIdentical(uExpected, uComputed, tol))
            {
                PrintFactorization(matrix, uExpected, uComputed, "INCORRECT");
                return true;
            }
            else if (printAnyway)
            {
                PrintFactorization(matrix, uExpected, uComputed, "CORRECT");
            }
            return false;
        }

        private static void PrintFactorization(double[,] matrix, double[,] uExpected, double[,] uComputed, string result)
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("The following Cholesky factorization is " + result + ":");
            Console.Write("A = ");
            Utilities.PrintArray(matrix);
            Console.WriteLine();
            Console.Write("U (expected) = ");
            Utilities.PrintArray(uExpected);
            Console.WriteLine();
            Console.Write("U (computed) = ");
            Utilities.PrintArray(uComputed);
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
        }

        private static void ProcessSolutionInfo(int info)
        {
            if (info == DefaultInfo)
            {
                throw new ApplicationException("The LAPACK call did not produce an info result. Something went wrong");
            }
            else if (info < 0)
            {

                string msg = string.Format("The {0}th parameter has an illegal value", -info);
                throw new ArgumentException(msg);
            }
        }

        private static bool CheckSolution(double[,] matrix, double[] b, double[] xExpected, double[] xComputed,
            double tol = 1e-13)
        {
            if (!Utilities.AreIdentical(xComputed, xExpected, tol))
            {
                PrintSolution(matrix, b, xExpected, xComputed, "INCORRECT");
                return true;
            }
            else if (printAnyway)
            {
                PrintSolution(matrix, b, xExpected, xComputed, "CORRECT");
            }
            return false;
        }

        private static void PrintSolution(double[,] matrix, double[] b, double[] xExpected, double[] xComputed,
            string result)
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("The following system solution is " + result + ":");
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

        private static void PrintMsgSingularFactor(int zeroPivotIdx)
        {
            Console.Write("U[{0}, {1}] = 0. The factorization has been completed, but U is exactly singular. ", 
                zeroPivotIdx, zeroPivotIdx);
            Console.WriteLine("Division by 0 will occur if you use the factor U for solving a system of linear equations.");
        }

        private static void PrintMsgCholeskyFail(int leadingMinorIdx)
        {
            Console.WriteLine("The leading minor of order " + leadingMinorIdx + " (and therefore the matrix itself) is not"
                + " positive-definite, and the factorization could not be completed.");
        }
    }
}
