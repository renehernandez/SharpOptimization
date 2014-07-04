using System;
using System.Collections.ObjectModel;

namespace SharpOptimization.AutoDiff.Compiler
{
    public class CompiledTerm
    {

        public ReadOnlyCollection<Variable> Variables { get; private set; }

        public Func<double[], double> Evaluator { get; private set; }

        public CompiledTerm(Variable[] vars, Func<double[], double> evaluator)
        {
            Variables = new ReadOnlyCollection<Variable>(vars);
            Evaluator = evaluator;
        }

        public double Eval(params double[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Variables[i].SetIndex(i);

            return Evaluator(values);
        }

    }
}
