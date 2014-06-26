using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public class Variable
    {

        # region Fields

        protected readonly Lazy<Variable> dx;

        //protected static Func<T, T, T> add;
        //protected static Func<T, T, T> subtract;
        //protected static Func<T, T, T> multiply;
        //protected static Func<T, T, T> divide;

        # endregion

        # region Public Properties

        public double Value { get; private set; }

        public Variable Dx 
        {
            get { return dx.Value; }
        }

        # endregion

        # region Constructors

        static Variable()
        {
            //add = Add();
            //subtract = Substract();
            //multiply = Multiply();
            //divide = Divide();
        }

        public Variable(double value)
        {
            Value = value;
            dx = new Lazy<Variable>(() => 0.0);
        }

        public Variable(double value, double dx)
        {
            Value = value;
            this.dx = new Lazy<Variable>(() => dx);
        }

        public Variable(double value, Lazy<Variable> dx)
        {
            Value = value;
            this.dx = dx;
        }

        # endregion

        # region Operators

        public static implicit operator double(Variable var)
        {
            return var.Value;
        }

        public static implicit operator Variable(double value)
        {
            return new Variable(value);
        }

        # endregion

        # region Public Methods

        public override bool Equals(object obj)
        {
            return Equals(obj as Variable);
        }

        public bool Equals(Variable other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("<{0}|{1}>", Value, Dx.Value);
        }

        # endregion

        # region Delegates for Operations

        //private static Func<T, T, T> Add()
        //{
        //    var paramA = Expression.Parameter(typeof(T), "a");
        //    var paramB = Expression.Parameter(typeof(T), "b");

        //    var body = Expression.Add(paramA, paramB);
        //    var funcAdd = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

        //    return funcAdd;
        //}

        //private static Func<T, T, T> Substract()
        //{
        //    var paramA = Expression.Parameter(typeof(T), "a");
        //    var paramB = Expression.Parameter(typeof(T), "b");

        //    var body = Expression.Subtract(paramA, paramB);
        //    var funcSubtract = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

        //    return funcSubtract;
        //}

        //private static Func<T, T, T> Multiply()
        //{
        //    var paramA = Expression.Parameter(typeof(T), "a");
        //    var paramB = Expression.Parameter(typeof(T), "b");

        //    var body = Expression.Multiply(paramA, paramB);
        //    var funcMultiply = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

        //    return funcMultiply;
        //}

        //private static Func<T, T, T> Divide()
        //{
        //    var paramA = Expression.Parameter(typeof(T), "a");
        //    var paramB = Expression.Parameter(typeof(T), "b");

        //    var body = Expression.Divide(paramA, paramB);
        //    var funcDivide = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

        //    return funcDivide;
        //}

        # endregion

    }
}
