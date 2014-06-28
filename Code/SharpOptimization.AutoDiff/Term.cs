using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Funcs;

namespace SharpOptimization.AutoDiff
{
    public abstract class Term
    {

        public double Eval(Variable[] vars, params double[] values)
        {
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i].MakeConstant(values[i]);
            }
            return Evaluate(vars, values);
        }

        internal abstract double Evaluate(Variable[] vars, params double[] values);

        public abstract double[] Differentiate(Variable[] vars, params double[] values);

        # region Operators

        public static Term operator -(Term inner)
        {
            return new MinusFunc(inner);
        }

        public static Term operator +(double left, Term right)
        {
            return new Variable().MakeConstant(left) + right;
        }

        public static Term operator +(Term left, double right)
        {
            return left + new Variable().MakeConstant(right);
        }

        public static Term operator +(Term left, Term right)
        {
            return new AddFunc(left, right);
        }

        public static Term operator -(double left, Term right)
        {
            return new Variable().MakeConstant(left) - right;
        }

        public static Term operator -(Term left, double right)
        {
            return left - new Variable().MakeConstant(right);
        }

        public static Term operator -(Term left, Term right)
        {
            return new SubtractFunc(left, right);
        }

        public static Term operator /(double left, Term right)
        {
            return new Variable().MakeConstant(left) / right;
        }

        public static Term operator /(Term left, double right)
        {
            return left/ new Variable().MakeConstant(right);
        }

        public static Term operator /(Term left, Term right)
        {   
            return new DivideFunc(left, right);
        }

        public static Term operator *(double left, Term right)
        {
            return new Variable().MakeConstant(left) * right;
        }

        public static Term operator *(Term left, double right)
        {
            return left * new Variable().MakeConstant(right);
        }

        public static Term operator *(Term left, Term right)
        {
            return new MultiplyFunc(left, right);
        }

        # endregion

    }
}
