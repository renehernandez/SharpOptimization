namespace SharpOptimization.AutoDiff.Funcs
{
    public abstract class UnaryFunc : Func
    {

        public Term Inner { get; private set; }

        protected UnaryFunc(Term inner)
        {
            Inner = inner;
        }

    }
}
