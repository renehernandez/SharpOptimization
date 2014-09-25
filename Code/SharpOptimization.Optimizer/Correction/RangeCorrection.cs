using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer.Correction
{
    public static class RangeCorrection
    {

        # region Public Methods

        /// <summary>
        /// Computes the BFGS correction formula for inverse hessiana approximation.
        /// </summary>
        /// <param name="func">Function to find it the hessiana approximation.</param>
        /// <param name="b">Current inverse approximation of the hessiana.</param>
        /// <param name="x">Current vector of the minimization Quasi-Newton algorithm.</param>
        /// <param name="x1">Next vector of the minimization Quasi-Newton algorithm.</param>
        /// <returns>Returns a matrix representing the next step in inverse hessiana approximation.s</returns>
        public static Matrix Bfgs(CompiledFunc func, Matrix b, Vector x, Vector x1)
        {
            var sk = new Matrix(x1 - x);
            var yk = new Matrix(func.Differentiate(x1) - func.Differentiate(x));

            var t = b.Dot(sk.Transpose()).Dot(sk).Dot(b)/sk.Dot(b).Dot(sk.Transpose())[0,0];
            var t1 = yk.Transpose().Dot(yk)/yk.Dot(sk.Transpose())[0,0];

            return b - t + t1;
        }

        # endregion

    }
}
