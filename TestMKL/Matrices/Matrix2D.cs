using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMKL.Matrices
{
    public class Matrix2D
    {
        private readonly double[,] data;
        public readonly int numRows;
        public readonly int numCols;

        public Matrix2D(double[,] data)
        {
            this.data = data;
            this.numRows = data.GetLength(0);
            this.numCols = data.GetLength(1);
        }

        public double this[int i, int j]
        {
            get { return data[i, j]; }
            set { data[i, j] = value; }
        }
    }
}
