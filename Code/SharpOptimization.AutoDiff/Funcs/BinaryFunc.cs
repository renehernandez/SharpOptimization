﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff.Funcs
{
    public abstract class BinaryFunc : Func
    {

        public Term Left { get; set; }

        public Term Right { get; set; }

        internal BinaryFunc(Term left, Term right, Func<double[], double> evaluator, Func<double[], double[]> diff) : base(evaluator, diff)
        {
            Left = left;
            Right = right;
            Left.Parent = this;
            Right.Parent = this;
        }

        //public static FuncDelegator Factory(Func<Variable[], double> lambda, Func<double[], double[]> diff)
        //{
        //    return new Func(lambda, diff);
        //}

        internal override void ResetDerivative()
        {
            Left.Derivative = IdentityFunc.Identity(0);
            Right.Derivative = IdentityFunc.Identity(0);

            Left.ResetDerivative();
            Right.ResetDerivative();
        }
    }
}
