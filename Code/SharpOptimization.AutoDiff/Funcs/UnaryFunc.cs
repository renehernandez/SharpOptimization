using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    public abstract class UnaryFunc : Func
    {

        # region Internal Properties

        internal Term Inner { get; set; }

        # endregion

        # region Constructors

        internal UnaryFunc(Term inner, Func<Vector, double> evaluator) : base(evaluator)
        {
            Inner = inner;
            Inner.Parent = this;
        }

        # endregion

        # region Internal Methods

        internal override void ResetDerivative()
        {
            Inner.Derivative = IdentityFunc.Identity(0);
            Inner.ResetDerivative();
        }

        # endregion

    }
}
