using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpOptimization.Numeric
{
    public static class Algebra
    {

        # region Public Methods

        public static double Norm(this Vector x)
        {
            return Math.Sqrt(x.Dot(x));
        }

        public static bool IsValid(this Vector x)
        {
            return x.All(xi => !double.IsInfinity(xi) && ! double.IsNaN(xi));
        }

        # endregion

    }
}
