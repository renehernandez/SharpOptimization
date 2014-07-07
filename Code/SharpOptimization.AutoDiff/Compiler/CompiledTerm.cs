using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharpOptimization.AutoDiff.Compiler
{
    public class CompiledTerm
    {

        public ReadOnlyCollection<Variable> Variables { get; private set; }

        public Func<double[], double> Evaluator { get; private set; }

        public Func<double[], double>[] GradientEvaluator { get; private set; }

        public CompiledTerm(Func<double[], double> evaluator, IEnumerable<Func<double[], double>> gradient)
        {
            Evaluator = evaluator;
            GradientEvaluator = gradient.ToArray();
        }

        public CompiledTerm(Func<double[], double> evaluator, params Func<double[], double>[] gradient)
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
