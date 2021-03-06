﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.AutoDiff.Funcs;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff
{
    public class Variable : Term, IEquatable<Variable>
    {

        # region Public Properties

        protected int Index { get; private set; }

        protected double ConstantValue { get; private set; }

        # endregion

        # region Constructors

        public Variable()
        {
            Index = -1;
        }

        # endregion

        # region Internal Methods

        internal Variable SetIndex(int index)
        {
            Index = index;
            return this;
        }

        internal override double Evaluate(Vector values)
        {
            return values[Index];
        }

        internal override void ResetDerivative()
        {
            Derivative = Func.Constant(0);
        }

        internal override Func<Vector, double> InternalCompile()
        {
            return values => values[Index];
        }

        internal override void Differentiate()
        {
            //return;
        }

        # endregion

        # region Public Methods

        public override bool Equals(object obj)
        {
            return Equals(obj as Variable);
        }

        public bool Equals(Variable other)
        {
            return !ReferenceEquals(other, null) && ReferenceEquals(other, this);
        }

        public override string ToString()
        {
            return string.Format("x{0}", Index);
        }

        # endregion

        # region Operators

        public static bool operator ==(Variable x, Variable y)
        {
            return x != null && x.Equals(y);
        }

        public static bool operator !=(Variable x, Variable y)
        {
            return !(x == y);
        }

        # endregion

    }
}
