using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer
{
    public abstract class AbstractOptimizer
    {

        public int NumberOfIterations { get; private set; }

        protected AbstractOptimizer(int iterations)
        {
            NumberOfIterations = iterations;
        }

        public Tuple<Vector, double> FindMinimun(CompiledFunc func, Vector input = null)
        {
            var res = Minimize(func, input);

            return new Tuple<Vector, double>(res, func.Eval(res));
        }

        protected abstract Vector Minimize(CompiledFunc func, Vector input);

    }
}
