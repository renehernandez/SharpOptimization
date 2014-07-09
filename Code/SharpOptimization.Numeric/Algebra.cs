using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpOptimization.Numeric
{
    public static class Algebra
    {

        public static double Norm(Vector x)
        {
            return Math.Sqrt(x.Dot(x));
        }

        public static bool IsValid(Vector x)
        {
            return x.All(xi => !double.IsInfinity(xi) && ! double.IsNaN(xi));
        }

    }
}
