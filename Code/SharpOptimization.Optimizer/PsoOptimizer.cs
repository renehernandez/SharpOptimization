using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer
{
    public class PsoOptimizer : AbstractOptimizer
    {

        public double GlobalFitValue { get; set; }

        public Vector GlobalBestVector { get; set; }

        public PsoOptimizer(int iterations, double eps = 1e-8) : base(iterations, eps)
        {
            
        }

        protected override Vector Minimize(CompiledFunc func, Vector x)
        {
            throw new NotImplementedException();
        }
    }
}
