using System;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class MinusFunc : UnaryFunc
    {

        # region Constructor

        internal MinusFunc(Term inner) : base(inner, values => - inner.Evaluate(values))
        {
        }

        # endregion

        # region Internal Methods

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return values => -func(values);
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative -= Derivative;

            Inner.Differentiate();
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            return string.Format("-({0})", Inner);
        }

        # endregion

    }
}
