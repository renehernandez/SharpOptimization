using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SharpOptimization.Numeric
{
    public class Vector : IEnumerable<double>
    {

        # region Private Fields

        private List<double> values;

        # endregion

        # region Public Properties

        public int Length
        {
            get { return values.Count; }
        }

        public double this[int index]
        {
            get { return values[index]; }
            set { values[index] = value; }
        }

        # endregion

        # region Constructors

        public Vector(int n)
        {
            values = new List<double>(Enumerable.Repeat(0.0, n));
        }

        public Vector(params double[] values)
        {
            this.values = new List<double>(values);
        }

        public Vector(IEnumerable<double> values)
        {
            this.values = new List<double>(values);
        }

        //public Vector(params int[] values)
        //{
        //    this.values = new List<double>(values.Select(x => x * 1.0));
        //}

        public Vector(IEnumerable<int> values)
        {
            this.values = new List<double>(values.Select(x => x * 1.0));
        }

        //public Vector(params long[] values)
        //{
        //    this.values = new List<double>(values.Select(x => x * 1.0));
        //}

        public Vector(IEnumerable<long> values)
        {
            this.values = new List<double>(values.Select(x => x * 1.0));
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            var sb = new StringBuilder("[");

            if (values.Count > 0)
            {
                sb.Append(values[0]);

                for (int i = 1; i < values.Count; i++)
                {
                    sb.Append(string.Format(", {0}", values[i]));
                }
            }
            sb.Append("]");

            return sb.ToString();
        }

        public double Dot(Vector right)
        {
            if(Length != right.Length)
                throw new Exception("Different vectors length");

            if (Length == 0)
                throw new Exception("Vector length must be greater than zero for Dot Operation");

            return (this * right).Sum();
        }
        
        public Vector Normalize()
        {
            double norm = Algebra.Norm(this);

            return this/norm;
        }

        public static Vector Ones(int n)
        {
            return new Vector(Enumerable.Repeat(1, n));
        }

        public static Vector Zeros(int n)
        {
            return new Vector(Enumerable.Repeat(0, n));
        }

        public Vector Copy()
        {
            return new Vector(this.Select(x => x));
        }

        # endregion

        # region Operators Implementation

        public static implicit operator double[](Vector vector)
        {
            return vector.values.ToArray();
        }

        public static implicit operator Vector(double[] values)
        {
            return new Vector(values);
        }

        public static implicit operator Vector(int[] values)
        {
            return new Vector(values.Select(i => i * 1.0));
        }

        public static implicit operator Vector(long[] values)
        {
            return new Vector(values.Select(i => i * 1.0));
        }

        public static Vector operator -(Vector vector)
        {
            return new Vector(vector.Select(x => -x));
        }

        public static Vector operator +(double value, Vector vector)
        {
            return vector + value;
        } 

        public static Vector operator +(Vector vector, double value)
        {
            return new Vector(vector.Select(x => x + value));
        } 

        public static Vector operator +(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new Exception("Different vectors length");
            return new Vector(left.Select((x, i) => x + right[i]));
        }

        public static Vector operator -(double value, Vector vector)
        {
            return new Vector(vector.Select(x => value - x));
        } 

        public static Vector operator -(Vector vector, double value)
        {
            return new Vector(vector.Select(x => x - value));
        } 

        public static Vector operator -(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new Exception("Different vectors length");
            return new Vector(left.Select((x, i) => x - right[i]));
        }

        public static Vector operator *(double value, Vector vector)
        {
            return vector*value;
        }

        public static Vector operator *(Vector vector, double value)
        {
            return new Vector(vector.Select(x => x*value));
        } 

        public static Vector operator *(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new Exception("Different vectors length");

            return new Vector(left.Select((x, i) => x*right[i]));
        }

        public static Vector operator /(Vector vector, double value)
        {
            return new Vector(vector.Select(x => x/value));
        }

        public static Vector operator /(double value, Vector vector)
        {
            return new Vector(vector.Select(x => value/x));
        } 

        public static Vector operator /(Vector left, Vector right)
        {
            if (left.Length != right.Length)
                throw new Exception("Different vectors length");

            return new Vector(left.Select((x, i) => x/right[i]));
        }

        # endregion

        # region IEnumerable Interface

        public IEnumerator<double> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        # endregion

    }
}
