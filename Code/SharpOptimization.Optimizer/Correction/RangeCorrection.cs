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

        public static Matrix Bfgs(CompiledFunc func, Matrix b, Vector x, Vector x1)
        {
            var sk = new Matrix(x1 - x);
            var yk = new Matrix(func.Differentiate(x1) - func.Differentiate(x));

            var t = b.Dot(sk.Transpose()).Dot(sk).Dot(b)/sk.Dot(b).Dot(sk.Transpose())[0,0];
            var t1 = yk.Transpose().Dot(yk)/yk.Dot(sk.Transpose())[0,0];

            return b - t + t1;
        }

    }
}
