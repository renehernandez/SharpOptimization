using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpOptimization.AutoDiff.Operators
{
    internal abstract class BinaryOperator : Term
    {

        # region Properties

        public Term Left { get; private set; }

        public Term Right { get; private set; }

        # endregion

        # region Constructor

        protected BinaryOperator(Term left, Term right)
        {
            Left = left;
            Right = right;
        }

        # endregion


    }
}
