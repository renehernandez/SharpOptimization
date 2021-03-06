﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace SharpOptimization.Numeric
{
    public class Matrix : IEnumerable<Vector>
    {

        private List<Vector> vectors;

        # region Public Properties

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public double this[int row, int column]
        {
            get { return vectors[row][column]; }
            set { vectors[row][column] = value; }
        }

        # endregion

        # region Constructors

        public Matrix(int rows, int column)
        {
            Rows = rows;
            Columns = column;

            vectors = Enumerable.Range(0, Rows).Select(x => new Vector(Columns)).ToList();
        }

        public Matrix(int square) : this(square, square)
        {
        }


        public Matrix(IEnumerable<Vector> vectors):this(vectors.ToArray())
        {
        }

        public Matrix(params Vector[] vectors)
        {
            if (vectors.Length == 0)
            {
                this.vectors = new List<Vector>();
                Rows = 0;
                Columns = 0;
            }
            else
            {
                var v0 = vectors[0];
                if(vectors.Any(v => v.Length != v0.Length))
                    throw new Exception("All rows must have same length");

                this.vectors = new List<Vector>(vectors);
                this.Rows = vectors.Length;
                this.Columns = v0.Length;
            }
        }

        # endregion

        # region Public Methods

        public Vector GetRow(int i)
        {
            if(i < 0 || i >= Rows)
                throw new Exception("Invalid row index within matrix");
            return vectors[i];
        }

        public Vector GetColumn(int i)
        {
            if(i < 0 || i >= Columns)
                throw new Exception("Invalid column index within matrix");

            return new Vector(vectors.Select(v => v[i]));
        }

        public static Matrix Identity(int n)
        {
            return new Matrix(Enumerable.Range(0, n).Select(i => SetOneValue(n, i)));
        }

        public Matrix Dot(Matrix matrix)
        {
            if(Columns != matrix.Rows)
                throw new Exception("Unmatched dimensions for dot operation");

            var res = new Matrix(Rows, matrix.Columns);

            double temp = 0;

            for (int i = 0; i < res.Rows; i++)
                for (int j = 0; j < res.Columns; j++)
                {
                    for (int k = 0; k < Columns; k++)
                        temp += this[i, k]*matrix[k, j];

                    res[i, j] = temp;
                    temp = 0;
                }

            return res;
        }

        public Matrix Dot(Vector vector)
        {
            return Dot(new Matrix(vector).Transpose());
        }

        public Matrix Transpose()
        {
            var res = new Matrix(Columns, Rows);

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    res[j, i] = this[i, j];
                }

            return res;
        }

        public Matrix Copy()
        {
            return new Matrix(vectors.Select(v => v.Copy()));
        }


        public override string ToString()
        {
            var sb = new StringBuilder("[\n");
            for (int i = 0; i < Rows; i++)
            {
                sb.AppendFormat("{0}\n", GetRow(i));
            }
            sb.Append("]");

            return sb.ToString();
        }

        # endregion

        # region Private Methods

        private static Vector SetOneValue(int n, int pos)
        {
            var vector = Vector.Zeros(n);
            vector[pos] = 1;
            return vector;
        }

        # endregion

        # region Operators Implementation

        public static implicit operator Vector(Matrix matrix)
        {
            if(matrix.Rows == 0 || matrix.Columns == 0)
                throw new Exception("Cannot convert from an empty matrix to vector type");

            if (matrix.Rows == 1)
                return matrix.GetRow(0);

            if (matrix.Columns == 1)
                return matrix.GetColumn(0);

            throw new Exception("Cannot convert from a matrix with more than one row and column to vector type");
        }

        public static implicit operator Matrix(Vector vector)
        {
            return new Matrix(vector);
        }

        public static Matrix operator -(Matrix matrix)
        {
            return new Matrix(matrix.Select(v => -v));
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            if(left.Rows != right.Rows || left.Columns != right.Columns)
                throw new Exception("Matrices dimensions do not match each other");

            return new Matrix(left.Select((v, i) => v + right.GetRow(i)));
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
                throw new Exception("Matrices dimensions do not match each other");

            return new Matrix(left.Select((v, i) => v - right.GetRow(i)));
        }

        public static Matrix operator *(double value, Matrix matrix)
        {
            return new Matrix(matrix.Select(v => value*v));
        }

        public static Matrix operator *(Matrix matrix, double value)
        {
            return value*matrix;
        }

        public static Matrix operator /(Matrix matrix, double value)
        {
            return new Matrix(matrix.Select(v => v / value)); ;
        }

        # endregion

        # region IEnumerable Interface

        public IEnumerator<Vector> GetEnumerator()
        {
            return vectors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        # endregion
    }
}
