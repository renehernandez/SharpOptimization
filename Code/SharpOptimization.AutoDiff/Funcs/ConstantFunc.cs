using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    internal class ConstantFunc : UnaryFunc
    {

        # region Constructors

        internal ConstantFunc(Term inner, Func<Vector, double> evaluator) : base(inner, evaluator)
        {
        }

        # endregion

        # region Internal Methods

        internal override Func<Vector, double> InternalCompile()
        {
            var func = Inner.InternalCompile();

            return func;
        }

        internal override void Differentiate()
        {
            if (Parent == null)
            {
                Derivative = Func.Constant(1);
            }

            Inner.Derivative += Derivative;

            Inner.Differentiate();
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            return Inner.ToString();
        }

        # endregion


    }
}
