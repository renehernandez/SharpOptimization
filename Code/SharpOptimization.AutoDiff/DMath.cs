using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Funcs;

namespace SharpOptimization.AutoDiff
{
    public static class DMath
    {

        public static Func Sin(Term x)
        {
            return new SinFunc(x);
        }

        public static Func Cos(Term x)
        {
            return new CosFunc(x);
        }

        public static Func Exp(Term x)
        {
            return new ExpFunc(x);
        }

        public static Func Pow(Term x, Term y)
        {
            return new PowFunc(x, y);
        }

        public static Func Ln(Term x)
        {
            return new LnFunc(x);
        }

    }

    internal class SinFunc : UnaryFunc
    {
        public SinFunc(Term inner)
            : base(inner, values => Math.Sin(inner.Evaluate(values)), null)
        {
            Inner.Parent = this;
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Inner.Derivative += Derivative*DMath.Cos(Inner);
            Inner.Differentiate();
        }
    }

    internal class CosFunc : UnaryFunc
    {
        public CosFunc(Term inner)
            : base(inner, values => Math.Cos(inner.Evaluate(values)), null)
        {
            Inner.Parent = this;
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Inner.Derivative += Derivative*-DMath.Sin(Inner);
            Inner.Differentiate();
        }
    }

    internal class ExpFunc : UnaryFunc
    {
        public ExpFunc(Term inner) : base(inner, values => Math.Exp(inner.Evaluate(values)), null)
        {
            Inner.Parent = this;
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Inner.Derivative += Derivative*DMath.Exp(Inner);
            Inner.Differentiate();
        }
    }

    internal class LnFunc : UnaryFunc
    {
        public LnFunc(Term inner) : base(inner, values => Math.Log(inner.Evaluate(values)), null)
        {
            Inner.Parent = this;
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Inner.Derivative += Derivative/Inner;
            Inner.Differentiate();
        }
    }

    internal class PowFunc : BinaryFunc
    {
        public PowFunc(Term left, Term right)
            : base(left, right, values => Math.Pow(left.Evaluate(values), right.Evaluate(values)), null)
        {
            Left.Parent = this;
            Right.Parent = this;
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = IdentityFunc.Identity(1);
            }

            Left.Derivative += Derivative * Right * DMath.Pow(Left, Right - 1);
            Right.Derivative += Derivative*DMath.Pow(Left, Right)*DMath.Ln(Left);

            Left.Differentiate();
            Right.Differentiate();
        }
    }
}
