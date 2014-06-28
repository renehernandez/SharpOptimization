using System;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class AddFunc : BinaryFunc
    {
        public AddFunc(Term left, Term right) : base(left, right)
        {
        }

        internal override double Evaluate(Variable[] vars, params double[] values)
        {
            return Left.Evaluate(vars, values) + Right.Evaluate(vars, values);
        }

        public override double[] Differentiate(Variable[] vars, params double[] values)
        {
            throw new NotImplementedException();
        }
    }
}
