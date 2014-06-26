using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public class Variable<T> : IEquatable<Variable<T>> where T : struct, IEquatable<T>
    {

        # region Fields

        protected readonly T value;
        protected readonly Lazy<Variable<T>> dx;

        protected static Func<T, T, T> add;
        protected static Func<T, T, T> subtract;
        protected static Func<T, T, T> multiply;
        protected static Func<T, T, T> divide;

        # endregion

        # region Public Properties

        public T Value { get; private set; }

        public Variable<T> Dx 
        {
            get { return dx.Value; }
        }

        # endregion

        # region Constructors

        static Variable()
        {
            add = Add();
            subtract = Substract();
            multiply = Multiply();
            divide = Divide();
        }

        public Variable(T value)
        {
            Value = value;
            dx = new Lazy<Variable<T>>(() => (Variable<T>)default(T));
        }

        public Variable(T value, T dx)
        {
            Value = value;
            this.dx = new Lazy<Variable<T>>(() => (Variable<T>) dx);
        }

        public Variable(T value, Lazy<Variable<T>> dx)
        {
            Value = value;
            this.dx = dx;
        }

        # endregion

        # region Operators

        public static implicit operator T(Variable<T> var)
        {
            return var.value;
        }

        public static implicit operator Variable<T>(T value)
        {
            return new Variable<T>(value);
        }

        # endregion

        # region Public Methods

        public override bool Equals(object obj)
        {
            return Equals(obj as Variable<T>);
        }

        public bool Equals(Variable<T> other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("<{0}|{1}>", value, Dx.value);
        }

        # endregion

        # region Delegates for Operations

        private static Func<T, T, T> Add()
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");

            var body = Expression.Add(paramA, paramB);
            var funcAdd = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            return funcAdd;
        }

        private static Func<T, T, T> Substract()
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");

            var body = Expression.Subtract(paramA, paramB);
            var funcSubtract = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            return funcSubtract;
        }

        private static Func<T, T, T> Multiply()
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");

            var body = Expression.Multiply(paramA, paramB);
            var funcMultiply = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            return funcMultiply;
        }

        private static Func<T, T, T> Divide()
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");

            var body = Expression.Divide(paramA, paramB);
            var funcDivide = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            return funcDivide;
        }

        # endregion

    }
}
