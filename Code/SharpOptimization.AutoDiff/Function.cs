using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public class Function<T> where T: struct, IEquatable<T>
    {

        protected Func<Variable<T>[], T> func;

        protected Variable<T> resul;

        protected Lazy<Variable<T>> gradient; 

        public int Length { get; private set; }

        public Function(Func<Variable<T>[], T> func, int length)
        {
            this.func = func;
            Length = length;
            gradient = new Lazy<Variable<T>>();
        }

        public T Eval(params Variable<T>[] vector)
        {
            if (vector.Length != Length)
                throw new InvalidOperationException();

            return func(vector);
        }

    }
}
