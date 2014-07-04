﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff.Funcs
{
    public class IdentityFunc : UnaryFunc
    {

        # region Constructors

        internal IdentityFunc(Term inner, Func<double[], double> evaluator, Func<double[], double[]> diff)
            : base(inner, evaluator, diff)
        {
        }

        # endregion

        // Agregar derivacion cuando se implemente
        public static Func Identity(double value)
        {
            var constant = ToConstant(value);
            return new IdentityFunc(constant, constant.Evaluate, null);
        }

    }
}
