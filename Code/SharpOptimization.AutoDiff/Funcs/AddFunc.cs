﻿using System;
using System.Linq.Expressions;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class AddFunc : BinaryFunc
    {
        public AddFunc(Term left, Term right)
            : base(left, right, values => left.Evaluate(values) + right.Evaluate(values), null)
        {
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }
            Left.Derivative += Derivative;
            Right.Derivative += Derivative;
            Left.Differentiate();
            Right.Differentiate();
        }
    }
}
