using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff.Operators
{
    internal class MultiplyOperator: BinaryOperator
    {
        public MultiplyOperator(Term left, Term right) : base(left, right)
        {
        }

        public override double Evaluate(params double[] values)
        {
            return Left.Evaluate(values)*Right.Evaluate(values);
        }

        public override double[] Differentiate(params double[] values)
        {
            throw new NotImplementedException();
        }
    }
}
