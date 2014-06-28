using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public class Variable : Term
    {

        # region Public Properties


        # endregion

        # region Constructors

        static Variable()
        {
            //add = Add();
            //subtract = Substract();
            //multiply = Multiply();
            //divide = Divide();
        }

        public Variable()
        {
        }

        # endregion

        public override double Evaluate(params double[] values)
        {
            Contract.Requires(values.Length == 1);
            return values[0];
        }

        public override double[] Differentiate(params double[] values)
        {
            Contract.Requires(values.Length == 1);
            return new[] {1.0};
        }
    }
}
