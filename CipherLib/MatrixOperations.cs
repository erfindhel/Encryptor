using System;

namespace MatrixOperations
{
    public static class Matrix
    {
        /// <summary>
        /// Умножает матрицу A на вектор B
        /// </summary>
        /// <param name="A">Матрица А</param>
        /// <param name="B">Вектор В</param>
        /// <returns>результирующий вектор</returns>
        public static int[] MultMatr(int[,] A, int[] B)
        {
            int[] C = new int[B.Length];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < B.GetLength(0); j++)
                {
                    int s = 0;
                    for (int k = 0; k < A.GetLength(1); k++)
                        s += A[i, k] * B[k];
                    C[i] = s;
                }
            return C;
        }

        /// <summary>
        /// Calculate the inverse of a matrix using Gauss-Jordan elimination.
        /// </summary>
        /// <param name="data">The matrix to invert.</param>
        /// <param name="inverse">The inverse of the matrix.</param>
        /// <returns>false of the matrix is singular, true otherwise.</returns>
        public static bool Invert(double[,] data, double[,] inverse)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (inverse == null)
                throw new ArgumentNullException("null");

            int drows = data.GetLength(0),
                dcols = data.GetLength(1),
                irows = inverse.GetLength(0),
                icols = inverse.GetLength(1),
                n, r;
            double scale;



            //Validate the matrix sizes
            if (drows != dcols)
                throw new ArgumentException("data is not a square matrix", "data");
            if (irows != icols)
                throw new ArgumentException("inverse is not a square matrix", "inverse");
            if (drows != irows)
                throw new ArgumentException("data and inverse must have identical sizes.");

            n = drows;

            //Initialize the inverse to the identity
            for (r = 0; r < n; ++r)
                for (int c = 0; c < n; ++c)
                    inverse[r, c] = (r == c) ? 1.0 : 0.0;

            //Process the matrix one column at a time
            for (int c = 0; c < n; ++c)
            {
                //Scale the current row to start with 1

                //Swap rows if the current value is too close to 0.0

                if (Math.Abs(data[c, c]) <= 2.0 * double.Epsilon)
                {
                    for (r = c + 1; r < n; ++r)
                        if (Math.Abs(data[r, c]) <= 2.0 * double.Epsilon)
                        {
                            RowSwap(data, n, c, r);
                            RowSwap(inverse, n, c, r);
                            break;
                        }
                    if (r >= n)
                        return false;
                }
                scale = 1.0 / data[c, c];
                RowScale(data, n, scale, c);
                RowScale(inverse, n, scale, c);

                //Zero out the rest of the column

                for (r = 0; r < n; ++r)
                {
                    if (r != c)
                    {
                        scale = -data[r, c];
                        RowScaleAdd(data, n, scale, c, r);
                        RowScaleAdd(inverse, n, scale, c, r);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Swap 2 rows in a matrix.
        /// </summary>
        /// <param name="data">The matrix to operate on.</param>
        /// <param name="cols">
        /// The number of columns in <paramref name="data"/>.
        /// </param>
        /// <param name="r0">One of the 2 rows to swap.</param>
        /// <param name="r1">One of the 2 rows to swap.</param>
        private static void RowSwap(double[,] data, int cols,
                                    int r0, int r1)
        {
            double tmp;

            for (int i = 0; i < cols; ++i)
            {
                tmp = data[r0, i];
                data[r0, i] = data[r1, i];
                data[r1, i] = tmp;
            }
        }

        /// <summary>
        /// Perform scale and add a row in a matrix to another
        /// row:  data[r1,] = a*data[r0,] + data[r1,].
        /// </summary>
        /// <param name="data">The matrix to operate on.</param>
        /// <param name="cols">
        /// The number of columns in <paramref name="data"/>.
        /// </param>
        /// <param name="a">
        /// The scale factor to apply to row <paramref name="r0"/>.
        /// </param>
        /// <param name="r0">The row to scale.</param>
        /// <param name="r1">The row to add and store to.</param>
        private static void RowScaleAdd(double[,] data, int cols,
                                        double a, int r0, int r1)
        {
            for (int i = 0; i < cols; ++i)
                data[r1, i] += a * data[r0, i];
        }

        /// <summary>
        /// Scale a row in a matrix by a constant factor.
        /// </summary>
        /// <param name="data">The matrix to operate on.</param>
        /// <param name="cols">The number of columns in the matrix.</param>
        /// <param name="a">
        /// The factor to scale row <paramref name="r"/> by.
        /// </param>
        /// <param name="r">The row to scale.</param>
        private static void RowScale(double[,] data, int cols,
                                     double a, int r)
        {
            for (int i = 0; i < cols; ++i)
                data[r, i] *= a;
        }
    }
}
