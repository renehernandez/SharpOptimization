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

    }
}
