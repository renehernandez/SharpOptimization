using System;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class DivideFunc : BinaryFunc
    {
        internal DivideFunc(Term left, Term right) : base(left, right, values => left.Evaluate(values) /  right.Evaluate(values), null)
        {
        }

        //internal override Func<double[], double> Compile()
        //{
        //    return evaluator;
        //}
    }
}
