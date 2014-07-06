using System;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class MultiplyFunc: BinaryFunc
    {

        public MultiplyFunc(Term left, Term right) : base(left, right, values => left.Evaluate(values) * right.Evaluate(values), null)
        {
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }
            Left.Derivative += Derivative*Right;
            Right.Derivative += Derivative*Left;

            Left.Differentiate();
            Right.Differentiate();
        }
    }
}
