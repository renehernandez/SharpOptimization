using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff
{
    public static class DMath
    {

        public static Func Sin(Term term)
        {
            return new Func(values => Math.Sin(term.Evaluate(values)), null);
        }

        public static Func Cos(Term term)
        {
            return new Func(values => Math.Cos(term.Evaluate(values)), null);
        }

        public static Func Exp(Term term)
        {
            return new Func(values => Math.Exp(term.Evaluate(values)), null);
        }

        public static Func Pow(Term x, Term y)
        {
            return new Func(values => Math.Pow(x.Evaluate(values), y.Evaluate(values)), null);
        }

    }
}
