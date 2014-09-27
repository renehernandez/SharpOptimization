using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Funcs;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff
{
    public static class DMath
    {

        # region Public Methods

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

        public static Func Sqrt(Term x)
        {
            return new SqrtFunc(x);
        }

        # endregion

    }

    # region Internal DMath Classes

    internal class SinFunc : UnaryFunc
    {
        public SinFunc(Term inner)
            : base(inner, values => Math.Sin(inner.Evaluate(values)))
        {
            Inner.Parent = this;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return values => Math.Sqrt(func(values));
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative += Derivative*DMath.Cos(Inner);
            Inner.Differentiate();
        }

        public override string ToString()
        {
            return string.Format("sin({0})", Inner);
        }
    }

    internal class CosFunc : UnaryFunc
    {
        public CosFunc(Term inner)
            : base(inner, values => Math.Cos(inner.Evaluate(values)))
        {
            Inner.Parent = this;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return values => Math.Cos(func(values));
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative += Derivative*-DMath.Sin(Inner);
            Inner.Differentiate();
        }

        public override string ToString()
        {
            return string.Format("cos({0})", Inner);
        }
    }

    internal class ExpFunc : UnaryFunc
    {
        public ExpFunc(Term inner) : base(inner, values => Math.Exp(inner.Evaluate(values)))
        {
            Inner.Parent = this;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return values => Math.Exp(func(values));
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative += Derivative*DMath.Exp(Inner);
            Inner.Differentiate();
        }

        public override string ToString()
        {
            return string.Format("exp({0})", Inner);
        }
    }

    internal class LnFunc : UnaryFunc
    {
        public LnFunc(Term inner) : base(inner, values => Math.Log(inner.Evaluate(values)))
        {
            Inner.Parent = this;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return values => Math.Log(func(values));
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative += Derivative/Inner;
            Inner.Differentiate();
        }

        public override string ToString()
        {
            return string.Format("ln({0})", Inner);
        }
    }

    internal class SqrtFunc : UnaryFunc
    {
        public SqrtFunc(Term inner) : base(inner, values => Math.Sqrt(inner.Evaluate(values)))
        {
            Inner.Parent = this;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return values => Math.Sqrt(func(values));
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative += Derivative * DMath.Sqrt(Inner) / (2 * Inner);
            Inner.Differentiate();
        }

        public override string ToString()
        {
            return string.Format("sqrt({0})", Inner);
        }
    }

    internal class PowFunc : BinaryFunc
    {
        public PowFunc(Term left, Term right)
            : base(left, right, values => Math.Pow(left.Evaluate(values), right.Evaluate(values)))
        {
            Left.Parent = this;
            Right.Parent = this;
        }

        internal override Func<Vector, double> InternalCompile()
        {
            var left = Left.InternalCompile();
            var right = Right.InternalCompile();

            return values => Math.Pow(left(values), right(values));
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Left.Derivative += Derivative * Right * DMath.Pow(Left, Right - 1);
            Right.Derivative += Derivative*DMath.Pow(Left, Right)*DMath.Ln(Left);

            Left.Differentiate();
            Right.Differentiate();
        }

        public override string ToString()
        {
            return string.Format("({0})**{1}", Left, Right);
        }
    }

# endregion

}
