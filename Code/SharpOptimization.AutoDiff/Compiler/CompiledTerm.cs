using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharpOptimization.Numeric;

namespace SharpOptimization.AutoDiff.Compiler
{
    public class CompiledTerm
    {

        public Func<Vector, double> Evaluator { get; private set; }

        public Func<Vector, double>[] GradientEvaluator { get; private set; }

        public CompiledTerm(Func<Vector, double> evaluator, IEnumerable<Func<Vector, double>> gradient)
        {
            Evaluator = evaluator;
            GradientEvaluator = gradient.ToArray();
        }

        public CompiledTerm(Func<Vector, double> evaluator, params Func<Vector, double>[] gradient)
        {
            Evaluator = evaluator;
            GradientEvaluator = gradient;
        }

        public double Eval(params double[] values)
        {
            return Evaluator(values);
        }

        public double[] Differentiate(params double[] values)
        {
            return GradientEvaluator.Select(df => df(values)).ToArray();
        }

    }
}
