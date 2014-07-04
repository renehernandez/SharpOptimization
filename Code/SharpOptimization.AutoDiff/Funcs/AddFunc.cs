using System;
using System.Linq.Expressions;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class AddFunc : BinaryFunc
    {
        public AddFunc(Term left, Term right) : base(left, right, values => left.Evaluate(values) +  right.Evaluate(values), null)
        {
        }

    }
}
