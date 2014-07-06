using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Funcs;

namespace SharpOptimization.AutoDiff
{
    public class Constant : Term, IEquatable<Constant>
    {

        protected double Value { get; private set; }

        public Constant(double value)
        {
            Value = value;
        }

        //internal override Func<double[], double> Compile()
        //{
        //    return Evaluate;
        //}

        internal override double Evaluate(params double[] values)
        {
            return Value;
        }

        internal override void ResetDerivative()
        {
            Derivative = IdentityFunc.Identity(0);
        }

        internal override void Differentiate()
        {
            // return;
        }

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
