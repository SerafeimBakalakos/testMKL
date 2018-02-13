using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL.Matrices
{
    class MatrixRowMajor
    {
        private readonly double[] data;
        public readonly int numRows;
        public readonly int numCols;

        public MatrixRowMajor(double[,] data)
        {
            this.data = Conversions.Array2DToFullRowMajor(data);
            this.numRows = data.GetLength(0);
            this.numCols = data.GetLength(1);
        }

        public double this[int i, int j]
        {
            get { return data[i * numRows + j]; }
            set { data[i * numRows + j] = value; }
        }
    }
}
