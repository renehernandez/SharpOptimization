﻿using System;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class DivideFunc : BinaryFunc
    {

        # region Constructors

        internal DivideFunc(Term left, Term right) : base(left, right, values => left.Evaluate(values) /  right.Evaluate(values))
        {
        }

        # endregion

        # region Internal Methods

        internal override Func<Vector, double> InternalCompile()
        {
            var left = Left.InternalCompile();
            var right = Right.InternalCompile();

            return values => left(values) / right(values);
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Left.Derivative += Derivative / Right;
            Right.Derivative += Derivative*(- Left/DMath.Pow(Right, 2));

            Left.Differentiate();
            Right.Differentiate();
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            return string.Format("({0})/{1}", Left, Right);
        }

        # endregion

    }
}
