using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer
{
    public static class Correction
    {

        public static Matrix Bfgs(CompiledFunc func, Matrix b, Vector x, Vector x1)
        {
            var sk = new Matrix(x1 - x);
            var yk = new Matrix(func.Differentiate(x1) - func.Differentiate(x));

            Console.WriteLine("Rows={0}, Columns={1}", sk.Dot(b).Dot(sk.Transpose()).Rows, sk.Dot(b).Dot(sk.Transpose()).Columns);

            var t = b.Dot(sk).Dot(sk)*b/sk.Dot(b).Dot(sk.Transpose())[0][0];

            var t1 = yk.Transpose().Dot(yk)/yk.Dot(sk.Transpose())[0][0];

            return b - t + t1;
        }

    }
}
