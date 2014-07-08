﻿using System;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class MultiplyFunc: BinaryFunc
    {

        public MultiplyFunc(Term left, Term right) : base(left, right, values => left.Evaluate(values) * right.Evaluate(values))
        {
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var left = Left.InternalCompile();
            var right = Right.InternalCompile();

            return values => left(values) * right(values);
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
