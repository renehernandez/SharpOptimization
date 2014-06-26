using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimizer
{
    public class Function<T>
    {

        protected Func<Variable<T>, T> func;

        public int Length { get; private set; }


        public Function(Func<Variable<T>, T> func, int length)
        {
            this.func = func;
            Length = length;
        }

        public T Evaluate(Variable<T> vector)
        {
            if(vector.Length != Length)
                throw new InvalidOperationException();

            return func(vector);
        }

    }
}
