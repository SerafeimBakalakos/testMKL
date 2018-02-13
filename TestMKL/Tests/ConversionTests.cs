using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL.Tests
{
    class ConversionTests
    {
        private const int order = 3;
        private static double[,] full = new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }};
        private static double[] fullRow = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static double[] fullCol = new double[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };

        private static double[,] lower = new double[,] {
            { 1, 0, 0 },
            { 4, 5, 0 },
            { 7, 8, 9 }};
        private static double[] lowerPackedRow = new double[] { 1, 4, 5, 7, 8, 9 };
        private static double[] lowerPackedCol = new double[] { 1, 4, 7, 5, 8, 9 };

        private static double[,] upper = new double[,] {
            { 1, 2, 3 },
            { 0, 5, 6 },
            { 0, 0, 9 }};
        private static double[] upperPackedRow = new double[] { 1, 2, 3, 5, 6, 9 };
        private static double[] upperPackedCol = new double[] { 1, 2, 5, 3, 6, 9 };

        private static double[,] symm = new double[,] {
            { 1, 2, 3 },
            { 2, 5, 6 },
            { 3, 6, 9 }};
        private static double[] symmLowerRow = new double[] { 1, 2, 5, 3, 6, 9 };
        private static double[] symmLowerCol = new double[] { 1, 2, 3, 5, 6, 9 };
        private static double[] symmUpperRow = new double[] { 1, 2, 3, 5, 6, 9 };
        private static double[] symmUpperCol = new double[] { 1, 2, 5, 3, 6, 9 };

        private static void PrintMessage(string from, string to, bool isCorrect)
        {
            if (isCorrect)
            {
                Console.WriteLine("Conversion from " + from + " to " + to + " is CORRECT");
            }
            else
            {
                Console.WriteLine("Conversion from " + from + " to " + to + " is INCORRECT");
            }
        }

        private static void TestFullConvertions()
        {
            double[] full2RowMajor = Conversions.Array2DToFullRowMajor(full);
            PrintMessage("2D full array", "row major 1D array", Utilities.AreIdentical(full2RowMajor, fullRow));
            double[,] rowMajor2Full = Conversions.FullRowMajorToArray2D(fullRow, order, order);
            PrintMessage("row major 1D array", "2D full array", Utilities.AreIdentical(rowMajor2Full, full));

            double[] full2ColMajor = Conversions.Array2DToFullColumnMajor(full);
            PrintMessage("2D full array", "col major 1D array", Utilities.AreIdentical(full2ColMajor, fullCol));
            double[,] colMajor2Full = Conversions.FullColumnMajorToArray2D(fullCol, order, order);
            PrintMessage("col major 1D array", "2D full array", Utilities.AreIdentical(colMajor2Full, full));
        }

        private static void TestTriangularConvertions()
        {
            double[] lower2RowMajor = Conversions.Array2DToPackedLowerRowMajor(lower);
            PrintMessage("2D lower array", "row major 1D array", Utilities.AreIdentical(lower2RowMajor, lowerPackedRow));
            double[,] rowMajor2Lower = Conversions.PackedLowerRowMajorToArray2D(lowerPackedRow);
            PrintMessage("row major 1D array", "2D lower array", Utilities.AreIdentical(rowMajor2Lower, lower));

            double[] lower2ColMajor = Conversions.Array2DToPackedLowerColMajor(lower);
            PrintMessage("2D lower array", "col major 1D array", Utilities.AreIdentical(lower2ColMajor, lowerPackedCol));
            double[,] colMajor2Lower = Conversions.PackedLowerColumnMajorToArray2D(lowerPackedCol);
            PrintMessage("col major 1D array", "2D lower array", Utilities.AreIdentical(colMajor2Lower, lower));

            double[] upper2RowMajor = Conversions.Array2DToPackedUpperRowMajor(upper);
            PrintMessage("2D upper array", "row major 1D array", Utilities.AreIdentical(upper2RowMajor, upperPackedRow));
            double[,] rowMajor2Upper = Conversions.PackedUpperRowMajorToArray2D(upperPackedRow);
            PrintMessage("row major 1D array", "2D upper array", Utilities.AreIdentical(rowMajor2Upper, upper));

            double[] upper2ColMajor = Conversions.Array2DToPackedUpperColumnMajor(upper);
            PrintMessage("2D upper array", "col major 1D array", Utilities.AreIdentical(upper2ColMajor, upperPackedCol));
            double[,] colMajor2Upper = Conversions.PackedUpperColumnMajorToArray2D(upperPackedCol);
            PrintMessage("col major 1D array", "2D upper array", Utilities.AreIdentical(colMajor2Upper, upper));
        }

        private static void TestSymmetricConvertions()
        {
            double[] symm2LowerRowMajor = Conversions.Array2DToPackedLowerRowMajor(symm);
            PrintMessage("2D symmetric array", "lower row major 1D array", Utilities.AreIdentical(symm2LowerRowMajor, symmLowerRow));
            double[,] lowerRowMajor2Symm = Conversions.Array2DLowerToSymmetric(Conversions.PackedLowerRowMajorToArray2D(symmLowerRow));
            PrintMessage("lower row major 1D array", "2D symmetric array", Utilities.AreIdentical(lowerRowMajor2Symm, symm));

            double[] symm2LowerColMajor = Conversions.Array2DToPackedLowerColMajor(symm);
            PrintMessage("2D symmetric array", "lower col major 1D array", Utilities.AreIdentical(symm2LowerColMajor, symmLowerCol));
            double[,] lowerColMajor2Symm = Conversions.Array2DLowerToSymmetric(Conversions.PackedLowerColumnMajorToArray2D(symmLowerCol));
            PrintMessage("lower col major 1D array", "2D symmetric array", Utilities.AreIdentical(lowerColMajor2Symm, symm));

            double[] symm2UpperRowMajor = Conversions.Array2DToPackedUpperRowMajor(symm);
            PrintMessage("2D symmetric array", "upper row major 1D array", Utilities.AreIdentical(symm2UpperRowMajor, symmUpperRow));
            double[,] upperRowMajor2Symm = Conversions.Array2DUpperToSymmetric(Conversions.PackedUpperRowMajorToArray2D(symmUpperRow));
            PrintMessage("upper row major 1D array", "2D symmetric array", Utilities.AreIdentical(upperRowMajor2Symm, symm));

            double[] symm2UpperColMajor = Conversions.Array2DToPackedUpperColumnMajor(symm);
            PrintMessage("2D symmetric array", "upper col major 1D array", Utilities.AreIdentical(symm2UpperColMajor, symmUpperCol));
            double[,] upperColMajor2Symm = Conversions.Array2DUpperToSymmetric(Conversions.PackedUpperColumnMajorToArray2D(symmUpperCol));
            PrintMessage("upper col major 1D array", "2D symmetric array", Utilities.AreIdentical(lowerColMajor2Symm, symm));
        }

        public static void Main()
        {
            TestFullConvertions();
            Console.WriteLine();
            TestTriangularConvertions();
            Console.WriteLine();
            TestSymmetricConvertions();
        }
    }
}
