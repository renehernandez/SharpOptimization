using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public class Function
    {

        protected Func<Variable[], double> func;

        protected Variable resul;

        protected Lazy<Variable> gradient; 

        public int Length { get; private set; }

        public Function(Func<Variable[], double> func, int length)
        {
            this.func = func;
            Length = length;
            gradient = new Lazy<Variable>();
        }

        public double Eval(params Variable[] vector)
        {
            if (vector.Length != Length)
                throw new InvalidOperationException();

            return func(vector);
        }

    }
}
