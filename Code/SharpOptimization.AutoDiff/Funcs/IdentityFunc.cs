using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Funcs
{
    public class IdentityFunc : UnaryFunc
    {

        # region Constructors

        internal IdentityFunc(Term inner, Func<Vector, double> evaluator)
            : base(inner, evaluator)
        {
        }

        # endregion

        # region Public Methods

        public static Func Identity(double value)
        {
            var constant = ToConstant(value);
            return new IdentityFunc(constant, constant.Evaluate);
        }

        public override string ToString()
        {
            return Inner.ToString();
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
                Derivative = Identity(1);
            }

            Inner.Derivative += Derivative;
            
            Inner.Differentiate();
        }

        # endregion

    }
}
