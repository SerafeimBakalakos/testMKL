using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL.Matrices
{
    //TODO: align data using mkl_malloc
    class MatrixColMajor
    {
        private readonly double[] data;
        public readonly int numRows;
        public readonly int numCols;

        public MatrixColMajor(double[,] data)
        {
            this.data = Conversions.Array2DToFullColMajor(data);
            this.numRows = data.GetLength(0);
            this.numCols = data.GetLength(1);
        }

        public double this[int i, int j]
        {
            get { return data[j * numCols + i]; }
            set { data[j * numCols + i] = value; }
        }
    }
}
