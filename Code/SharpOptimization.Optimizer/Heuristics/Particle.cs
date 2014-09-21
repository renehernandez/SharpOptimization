using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer.Heuristics
{
    public class Particle
    {

        public double C1 { get { return 2.05; } }

        public double C2 { get { return 2.05; } }

        public double Chi { get { return 0.72984; } }

        public int NumberOfDimensions { get; set; }

        public Tuple<Vector, Vector> Bounds { get; set; }

        public Vector BestVector { get; set; }

        public double BestFit { get; set; }

        public CompiledFunc Func { get; set; }

        public Particle(CompiledFunc func, int dimensions, Tuple<Vector, Vector> bounds)
        {
            Func = func;
            NumberOfDimensions = dimensions;
        }

    }
}
