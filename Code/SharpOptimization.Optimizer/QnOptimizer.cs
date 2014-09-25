using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;
using SharpOptimization.Optimizer.Correction;
using SharpOptimization.Optimizer.LineSearch;

namespace SharpOptimization.Optimizer
{
    public class QnOptimizer : AbstractOptimizer
    {

        public Func<CompiledFunc, Vector, Vector, double> Searcher { get; private set; }

        public Func<CompiledFunc, Matrix, Vector, Vector, Matrix> Corrector { get; private set; } 

        public QnOptimizer(int iterations, Func<CompiledFunc, Vector, Vector, double> searcher = null, Func<CompiledFunc, Matrix, Vector, Vector, Matrix> corrector= null, double eps= 1e-8) : base(iterations, eps)
        {
            Searcher = searcher ?? LinearSearch.Wolfe;
            Corrector = corrector ?? RangeCorrection.Bfgs;
        }

        protected override Vector Minimize(CompiledFunc func, Vector x = null, Tuple<Vector, Vector> bounds = null)
        {
            var b = Matrix.Identity(x.Length);
            
            var x1 = new Vector(x);
            Vector d;
            double a;

            while (CurrentIteration < IterationsNumber)
            {
                CurrentIteration++;

                d = -b.Dot(func.Differentiate(x));
                
                a = Searcher(func, x, d);
                x1 = x + a*d;

                b = Corrector(func, b, x, x1);

                if (!Algebra.IsValid(x1) || Algebra.Norm(x1 - x) <= EPS)
                    break;

                x = x1;
            }

            return x;
        }
    }
}
