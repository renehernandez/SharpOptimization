using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer
{
    public class QnOptimizer : AbstractOptimizer
    {
        public QnOptimizer(int iterations) : base(iterations)
        {
        }

        protected override Vector Minimize(CompiledFunc func, Vector input)
        {
            throw new NotImplementedException();
        }
    }
}
