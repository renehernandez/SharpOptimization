using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Compiler
{
    public class CompiledFunc
    {

        # region Public Properties

        public Func<Vector, double> Evaluator { get; private set; }

        public Func<Vector, double>[] GradientEvaluator { get; private set; }

        public int Dimension { get { return GradientEvaluator.Length; } }

        # endregion

        # region Constructors

        public CompiledFunc(Func<Vector, double> evaluator, IEnumerable<Func<Vector, double>> gradient)
        {
            Evaluator = evaluator;
            GradientEvaluator = gradient.ToArray();
        }

        public CompiledFunc(Func<Vector, double> evaluator, params Func<Vector, double>[] gradient)
        {
            Evaluator = evaluator;
            GradientEvaluator = gradient;
        }

        # endregion

        # region Public Methods

        public double Eval(params double[] values)
        {
            return Evaluator(values);
        }

        public Vector Differentiate(params double[] values)
        {
            return GradientEvaluator.Select(df => df(values)).ToArray();
        }

        # endregion

    }
}
