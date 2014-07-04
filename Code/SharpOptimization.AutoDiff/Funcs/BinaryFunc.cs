using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff.Funcs
{
    public class BinaryFunc : Func
    {

        public Term Left { get; set; }

        public Term Right { get; set; }

        internal BinaryFunc(Term left, Term right, Func<double[], double> evaluator, Func<double[], double[]> diff) : base(evaluator, diff)
        {
            Left = left;
            Right = right;
        }

        //public static FuncDelegator Factory(Func<Variable[], double> lambda, Func<double[], double[]> diff)
        //{
        //    return new Func(lambda, diff);
        //}
    }
}
