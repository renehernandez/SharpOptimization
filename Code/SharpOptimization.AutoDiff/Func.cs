using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SharpOptimization.AutoDiff.Funcs;

namespace SharpOptimization.AutoDiff
{
    public class Func : Term
    {

        
        protected Func<double[], double> evaluator;

        protected Func<double[], double[]> diff;


        internal Func(Func<double[], double> evaluator, Func< double[], double[]> diff)
        {
            this.evaluator = evaluator;
            this.diff = diff;
        }

        //internal override Func<double[], double> Compile()
        //{
        //    return evaluator;
        //}

        public double Eval(Variable[] vars, params double[] values)
        {
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i].SetIndex(i);
            }
            return Evaluate(values);
        }

        internal override double Evaluate(params double[] values)
        {
            return evaluator(values);
        }

        public double[] Differentiate(Variable[] vars, params double[] values)
        {
            throw new Exception();
        }

    }
}
