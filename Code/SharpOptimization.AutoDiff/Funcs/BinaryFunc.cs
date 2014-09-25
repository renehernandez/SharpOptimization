using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    public abstract class BinaryFunc : Func
    {

        # region Internal Properties

        internal Term Left { get; set; }

        internal Term Right { get; set; }

        # endregion

        # region Constructors

        internal BinaryFunc(Term left, Term right, Func<Vector, double> evaluator) : base(evaluator)
        {
            Left = left;
            Right = right;
            Left.Parent = this;
            Right.Parent = this;
        }

        # endregion

        # region Internal Methods

        internal override void ResetDerivative()
        {
            Left.Derivative = Func.Constant(0);
            Right.Derivative = Func.Constant(0);

            Left.ResetDerivative();
            Right.ResetDerivative();
        }

        # endregion

    }
}
