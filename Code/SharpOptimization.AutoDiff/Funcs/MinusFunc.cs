using System;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class MinusFunc : UnaryFunc
    {

        # region Constructor

        internal MinusFunc(Term inner) : base(inner, values => - inner.Evaluate(values), null)
        {
        }

        # endregion

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Inner.Derivative -= Derivative;

            Inner.Differentiate();
        }
    }
}
