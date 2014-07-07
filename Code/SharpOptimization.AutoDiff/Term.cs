using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.AutoDiff.Funcs;

namespace SharpOptimization.AutoDiff
{
    public abstract class Term
    {

        internal Term Parent { get; set; }

        internal Func Derivative { get; set; }

        public static Constant Zero()
        {
            return new Constant(0);
        }

        public static Constant One()
        {
            return new Constant(1);
        }

        public static Constant ToConstant(double value)
        {
            return new Constant(value);
        }


        internal abstract double Evaluate(params double[] values);

        internal abstract Func<double[], double> InternalCompile();

        //internal abstract double[] Differentiate(params double[] values);

        internal abstract void ResetDerivative();

        internal abstract void Differentiate();

        # region Operators

        public static Func operator -(Term inner)
        {
            return new MinusFunc(inner);
        }

        public static Func operator +(double left, Term right)
        {
            return new AddFunc(ToConstant(left), right);
        }

        public static Func operator +(Term left, double right)
        {
            return new AddFunc(left, ToConstant(right));
        }

        public static Func operator +(Term left, Term right)
        {
            return new AddFunc(left, right);
        }

        public static Func operator -(double left, Term right)
        {
            return new SubtractFunc(ToConstant(left), right);
        }

        public static Func operator -(Term left, double right)
        {
            return new SubtractFunc(left, ToConstant(right));
        }

        public static Func operator -(Term left, Term right)
        {
            return new SubtractFunc(left, right);
        }

        public static Func operator /(double left, Term right)
        {
            return new DivideFunc(ToConstant(left), right);
        }

        public static Func operator /(Term left, double right)
        {
            return new DivideFunc(left, ToConstant(right));
        }

        public static Func operator /(Term left, Term right)
        {
            return new DivideFunc(left, right);
        }

        public static Func operator *(double left, Term right)
        {
            return new MultiplyFunc(ToConstant(left), right);
        }

        public static Func operator *(Term left, double right)
        {
            return new MultiplyFunc(left, ToConstant(right));
        }

        public static Func operator *(Term left, Term right)
        {
            return new MultiplyFunc(left, right);
        }

        # endregion

        # region Conversions

        public static implicit operator Term(double value)
        {
            return ToConstant(value);
        }


        # endregion

    }
}
