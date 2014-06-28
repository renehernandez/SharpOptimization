using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Operators;

namespace SharpOptimization.AutoDiff
{
    public abstract class Term
    {


        public abstract double Evaluate(params double[] values);

        public abstract double[] Differentiate(params double[] values);

        # region Operators

        public static Term operator +(Term left, Term right)
        {
            return new AddOperator(left, right);
        }

        public static Term operator -(Term left, Term right)
        {
            return new SubtractOperator(left, right);
        }

        public static Term operator /(Term left, Term right)
        {
            
            return new DivideOperator(left, right);
        }

        public static Term operator *(Term left, Term right)
        {
            return new MultiplyOperator(left, right);
        }

        # endregion

    }
}
