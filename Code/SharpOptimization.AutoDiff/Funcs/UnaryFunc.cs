using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff.Funcs
{
    public abstract class UnaryFunc : Func
    {

        public Term Inner { get; set; }

        internal UnaryFunc(Term inner, Func<double[], double> evaluator, Func<double[], double[]> diff) : base(evaluator, diff)
        {
            Inner = inner;
            Inner.Parent = this;
        }

        internal override void ResetDerivative()
        {
            Inner.Derivative = IdentityFunc.Identity(0);
            Inner.ResetDerivative();
        }
    }
}
