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

        //internal override Func<double[], double> Compile()
        //{
        //    return evaluator;
        //}
    }
}
