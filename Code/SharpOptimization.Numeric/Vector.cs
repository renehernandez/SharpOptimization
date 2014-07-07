using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SharpOptimization.Numeric
{
    public class Vector<T>
    {

        private static Func<T, T, T> addFunc;

        private static Func<T, T, T> subtractFunc;

        private static Func<T, T, T> multiplyFunc;

        private static Func<T, T, T> divFunc;

        static Vector()
        {
            addFunc = Add();
        } 

        public static Func<T, T, T> Add()
        {
            var type = typeof (T);

            var left = Expression.Parameter(type);
            var right = Expression.Parameter(type);

            var lambda = Expression.Lambda<Func<T, T, T>>(Expression.Add(left, right), left, right);

            return lambda.Compile();
        } 

    }
}
