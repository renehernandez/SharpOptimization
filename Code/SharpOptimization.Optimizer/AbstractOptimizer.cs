﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer
{
    public abstract class AbstractOptimizer
    {

        public int IterationsNumber { get; private set; }

        public int CurrentIteration { get; protected set; }

        public double EPS { get; private set; }

        protected AbstractOptimizer(int iterations, double eps)
        {
            IterationsNumber = iterations;
            EPS = eps;
            CurrentIteration = 0;
        }

        public Tuple<Vector, double> FindMinimun(CompiledFunc func, Vector input = null, Tuple<Vector, Vector> bounds = null)
        {
            var res = Minimize(func, input, bounds);

            return new Tuple<Vector, double>(res, func.Eval(res));
        }

        protected abstract Vector Minimize(CompiledFunc func, Vector x = null, Tuple<Vector, Vector> bounds = null);

    }
}
