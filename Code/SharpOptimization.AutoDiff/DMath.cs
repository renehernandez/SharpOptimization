using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public static class DMath
    {

        public static Variable Sqrt(double d)
        {
            return Sqrt(new Variable(d, 1));
        }

        public static Variable Sqrt(Variable x)
        {
            return new Variable(Math.Sqrt(x), new Lazy<Variable>(() => 0.5 * x.Dx / Sqrt(x)));
        }


        public static Variable Sin(double d)
        {
            return Sin(new Variable(d, 1));
        }

        public static Variable Sin(Variable x)
        {
            return new Variable(Math.Sin(x), new Lazy<Variable>(()=> Math.Cos(x) * x.Dx));
        }

        public static Variable Cos(double d)
        {
            return Cos(new Variable(d, 1));
        }

        public static Variable Cos(Variable x)
        {
            return new Variable(Math.Cos(x), new Lazy<Variable>(() => -Math.Sin(x) * x.Dx));
        }

    }
}
