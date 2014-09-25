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

        # region Public Properties

        /// <summary>
        /// Returns the maximum number of iterations allowed for the algorithm execution.
        /// </summary>
        public int IterationsNumber { get; private set; }

        /// <summary>
        /// Returns the current iteration of the algorithm execution.
        /// </summary>
        public int CurrentIteration { get; protected set; }
        
        /// <summary>
        /// Returns the Epsilon value, i.e, the threshold for consider values different from zero
        /// </summary>
        public double EPS { get; private set; }

        # endregion

        # region Constructors

        protected AbstractOptimizer(int iterations, double eps)
        {
            IterationsNumber = iterations;
            EPS = eps;
            CurrentIteration = 0;
        }

        # endregion

        # region Public Methods

        /// <summary>
        /// Looks for the vector x that minimizes function f, i.e, vector x such that f(x) &lt;= f(y) for all 
        /// y values in the function search space.
        /// </summary>
        /// <param name="f">Function to which the minimum will be looking for.</param>
        /// <param name="input">Optional vector used as started point for the algorithm to look for.</param>
        /// <param name="bounds">Optional limits of function space search.</param>
        /// <returns>Returns a tuple with the minimun vector found by optimizer and the value of the function in this vector.</returns>
        public Tuple<Vector, double> FindMinimun(CompiledFunc f, Vector input = null, Tuple<Vector, Vector> bounds = null)
        {
            var res = Minimize(f, input, bounds);

            return new Tuple<Vector, double>(res, f.Eval(res));
        }

        # endregion

        # region Protected Methods

        /// <summary>
        /// Algorithmic implementation of optimization model to look for the minimun function value.
        /// </summary>
        /// <param name="f">Function to which the minimum will be looking for.</param>
        /// <param name="x">Optional vector used as started point for the algorithm to look for.</param>
        /// <param name="bounds">Optional limits of function space search.</param>
        /// <returns>Returns the minimun vector found by optimizer.</returns>
        protected abstract Vector Minimize(CompiledFunc f, Vector x = null, Tuple<Vector, Vector> bounds = null);

        # endregion

    }
}
