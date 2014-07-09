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
            Derivative = IdentityFunc.Identity(0);
        }

        internal override Func<Vector, double> InternalCompile()
        {
            //var values = Expression.Parameter(typeof (Vector));
            //var body = Expression.ArrayIndex(values, Expression.Constant(Index));
            //var lambda = Expression.Lambda<Func<Vector, double>>(body, values);
            //return lambda.Compile();
            return values => values[Index];
        }

        internal override void Differentiate()
        {
            //return;
        }


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
