using System;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class DivideFunc : BinaryFunc
    {
        internal DivideFunc(Term left, Term right) : base(left, right, values => left.Evaluate(values) /  right.Evaluate(values), null)
        {
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Left.Derivative += Derivative / Right;
            Right.Derivative += Derivative*(- Left/DMath.Pow(Right, 2));

            Left.Differentiate();
            Right.Differentiate();
        }
    }
}
