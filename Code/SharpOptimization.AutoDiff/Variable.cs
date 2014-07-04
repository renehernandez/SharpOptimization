using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.AutoDiff.Funcs;

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

        //internal override Func<double[], double> Compile()
        //{
        //    return Evaluate;
        //}

        internal Variable SetIndex(int index)
        {
            Index = index;
            return this;
        }

        internal override double Evaluate(params double[] values)
        {
            return values[Index];
        }

        //public override double[] Differentiate(Variable[] vars, params double[] values)
        //{
        //    throw new NotImplementedException();
        //}

        public override bool Equals(object obj)
        {
            return Equals(obj as Variable);
        }

        public bool Equals(Variable other)
        {
            return !ReferenceEquals(other, null) && ReferenceEquals(other, this);
        }

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
