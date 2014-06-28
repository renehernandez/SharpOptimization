namespace SharpOptimization.AutoDiff.Funcs
{
    internal abstract class BinaryFunc : Func
    {

        # region Properties

        public Term Left { get; private set; }

        public Term Right { get; private set; }

        # endregion

        # region Constructor

        protected BinaryFunc(Term left, Term right)
        {
            Left = left;
            Right = right;
        }

        # endregion
    }
}
