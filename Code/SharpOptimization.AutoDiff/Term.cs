﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.AutoDiff.Funcs;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff
{
    public abstract class Term
    {

        # region Internal Properties

        internal Term Parent { get; set; }

        internal Func Derivative { get; set; }

        # endregion

        # region Public Methods

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

        # endregion

        # region Internal Methods

        internal abstract double Evaluate(Vector values);

        internal abstract Func<Vector, double> InternalCompile();

        internal abstract void ResetDerivative();

        internal abstract void Differentiate();

        # endregion

        # region Operators Implementation

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

        public static implicit operator Term(double value)
        {
            return ToConstant(value);
        }

        # endregion

    }
}
