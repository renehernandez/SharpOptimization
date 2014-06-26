using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public class Variable<T>
    {

        protected T[] values;

        protected static Func<T, T, T> add;
        protected static Func<T, T, T> subtract;
        protected static Func<T, T, T> multiply;
        protected static Func<T, T, T> divide;

        public int Length
        {
            get { return values.Length; }
        }

        public T this[int index]
        {
            get { return values[index]; }
            set { values[index] = value; }
        }

        static Variable()
        {
            add = Add();
            subtract = Substract();
            multiply = Multiply();
            divide = Divide();
        }

        public Variable(params T[] values)
        {
            this.values = values;
        }

        public Variable(IEnumerable<T> values)
        {
            this.values = values.ToArray();
        }

        public static Variable<T> operator +(Variable<T> first, Variable<T> second)
        {
            return new Variable<T>(Enumerable.Range(0, first.Length).Select(i => add(first[i], second[i])));
        }

        public static Variable<T> operator -(Variable<T> first, Variable<T> second)
        {
            return new Variable<T>(Enumerable.Range(0, first.Length).Select(i => subtract(first[i], second[i])));
        }

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

    }
}
