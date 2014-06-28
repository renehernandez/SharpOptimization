using System;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class MinusFunc : UnaryFunc
    {
        public MinusFunc(Term inner) : base(inner)
        {
        }

        internal override double Evaluate(Variable[] vars, params double[] values)
        {
            return - Inner.Evaluate(vars, values);
        }

        public override double[] Differentiate(Variable[] vars, params double[] values)
        {
            throw new NotImplementedException();
        }
    }
}
