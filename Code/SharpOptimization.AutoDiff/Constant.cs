using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Funcs;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff
{
    public class Constant : Term, IEquatable<Constant>
    {

        # region Protected Properties

        protected double Value { get; private set; }

        # endregion

        # region Constructors

        public Constant(double value)
        {
            Value = value;
        }

        # endregion

        # region Internal Properties

        internal override double Evaluate(Vector values)
        {
            return Value;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            return values => Value;
        }

        internal override void ResetDerivative()
        {
            Derivative = Func.Constant(0);
        }

        internal override void Differentiate()
        {
            // return;
        }

        # endregion

        # region Public Methods

        public static implicit operator double(Constant constant)
        {
            return constant.Value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Constant);
        }

        public bool Equals(Constant other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        # endregion

        # region Operators

        public static bool operator ==(Constant x, Constant y)
        {
            return x != null && x.Equals(y);
        }

        public static bool operator !=(Constant x, Constant y)
        {
            return !(x == y);
        }


        public static bool operator ==(Constant x, double y)
        {
            return x != null && x.Value.Equals(y);
        }

        public static bool operator !=(Constant x, double y)
        {
            return !(x == y);
        }

        public static bool operator ==(double x, Constant y)
        {
            return y != null && y.Value.Equals(x);
        }

        public static bool operator !=(double x, Constant y)
        {
            return !(x == y);
        }

        # endregion
    }
}
