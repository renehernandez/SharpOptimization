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

        private bool isConstant;
        private double constantValue;



        # endregion

        # region Constructors

        public Variable()
        {
            isConstant = false;
        }

        # endregion

        internal Variable MakeConstant(double value)
        {
            isConstant = true;
            constantValue = value;
            return this;
        }

        public virtual double Eval(double value)
        {
            return isConstant ? constantValue : value;
        }

        internal override double Evaluate(Variable[] vars, params double[] values)
        {
            return isConstant ? constantValue : values[0];
        }

        public override double[] Differentiate(Variable[] vars, params double[] values)
        {
            throw new NotImplementedException();
        }
    }
}
