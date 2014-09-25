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

        # region Public Properties
        
        /// <summary>
        /// Gets the line search methods used by Quasi-Newton optimization method.
        /// </summary>
        public Func<CompiledFunc, Vector, Vector, double> Searcher { get; private set; }

        /// <summary>
        /// Gets the correction formula used by Quasi-Newton optimization method.
        /// </summary>
        public Func<CompiledFunc, Matrix, Vector, Vector, Matrix> Corrector { get; private set; }

        # endregion

        # region Constructors

        public QnOptimizer(int iterations, Func<CompiledFunc, Vector, Vector, double> searcher = null, Func<CompiledFunc, Matrix, Vector, Vector, Matrix> corrector= null, double eps= 1e-8) : base(iterations, eps)
        {
            Searcher = searcher ?? LinearSearch.Wolfe;
            Corrector = corrector ?? RangeCorrection.Bfgs;
        }

        # endregion

        # region Protected Methods

        protected override Vector Minimize(CompiledFunc f, Vector x = null, Tuple<Vector, Vector> bounds = null)
        {
            CurrentIteration = 0;
            var b = Matrix.Identity(x.Length);
            
            var x1 = new Vector(x);
            Vector d;
            double a;

            while (CurrentIteration < IterationsNumber)
            {
                CurrentIteration++;

                d = -b.Dot(f.Differentiate(x));
                
                a = Searcher(f, x, d);
                x1 = x + a*d;

                b = Corrector(f, b, x, x1);

                if (!Algebra.IsValid(x1) || Algebra.Norm(x1 - x) <= EPS)
                    break;

                x = x1;
            }

            return x;
        }

        # endregion

    }
}
