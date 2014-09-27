using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.AutoDiff.Funcs;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff
{
    public abstract class Func : Term
    {

        # region Protected Properties

        protected Func<Vector, double> Evaluator { get; set; }

        # endregion

        # region Constructors

        internal Func(Func<Vector, double> evaluator)
        {
            Evaluator = evaluator;
        }

        # endregion

        # region Public Methods

        public static Func Constant(double value)
        {
            var constant = ToConstant(value);
            return new ConstantFunc(constant, constant.Evaluate);
        }

        public CompiledFunc Compile(params Variable[] vars)
        {
            MakeDifferentiation(vars);
            var gradient = vars.Select(v => v.Derivative.InternalCompile());
            return new CompiledFunc(InternalCompile(), gradient);
        }

        public double Eval(Variable[] vars, params double[] values)
        {
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i].SetIndex(i);
            }
            return Evaluate(values);
        }

        public double[] Differentiate(Variable[] vars, params double[] values)
        {
            MakeDifferentiation(vars);

            return vars.Select(v => v.Derivative.Evaluate(values)).ToArray();
        }

        # endregion

        # region Internal Methods

        internal override double Evaluate(Vector values)
        {
            return Evaluator(values);
        }

        # endregion

        # region Private Methods

        private void MakeDifferentiation(Variable[] vars)
        {
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i].SetIndex(i);
            }

            ResetDerivative();
            Differentiate();
        }

        # endregion
    }
}
