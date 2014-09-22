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

        # region Public Properties

        public double C1 { get { return 2.05; } }

        public double C2 { get { return 2.05; } }

        public double Chi { get { return 0.72984; } }

        public int NumberOfDimensions { get; private set; }

        public Tuple<Vector, Vector> Bounds { get; private set; }

        public Vector BestPosition { get; set; }

        public Vector CurrentPosition { get; private set; }

        public Vector Velocity { get; private set; }

        public double BestFit { get; set; }

        public double CurrentFit { get; private set; }

        public CompiledFunc Func { get; private set; }

        public Particle[] Neighbors { get; set; }

        # endregion

        # region Constructors

        public Particle(CompiledFunc func, int dimensions, int neighborsNumber, Tuple<Vector, Vector> bounds, Func<double> random)
        {
            Func = func;
            NumberOfDimensions = dimensions;
            Bounds = bounds;
            Neighbors = new Particle[neighborsNumber];

            CurrentPosition = BestPosition = new Vector(Enumerable.Range(0, dimensions).Select(i => random()));
            Velocity = new Vector(Enumerable.Range(0, dimensions).Select(i => random()));

            CurrentFit = BestFit = func.Eval(CurrentPosition);
        }

        # endregion

        # region Public Methods

        public void Update()
        {
            var neighborsBest = BestPositionFromNeighbors();

            double e1 = Distributions.Uniform(0, 1);
            double e2 = Distributions.Uniform(0, 1);

            Velocity = Chi*(Velocity + C1*e1*(BestPosition - CurrentPosition) + C2*e2*(neighborsBest - CurrentPosition));
            CurrentPosition += Velocity;

            if (IsFeasible())
                CurrentFit = Func.Eval(CurrentPosition);

            if (CurrentFit >= BestFit) return;
            
            BestFit = CurrentFit;
            BestPosition = CurrentPosition;
        }

        # endregion

        # region Private Methods

        private bool IsFeasible()
        {
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                if (Bounds.Item1[i] > CurrentPosition[i] || Bounds.Item2[i] < CurrentPosition[i])
                    return false;
            }

            return true;
        }

        private Vector BestPositionFromNeighbors()
        {
            int pos = 0;

            for (int i = 1; i < Neighbors.Length; i++)
                if (Neighbors[i].BestFit < Neighbors[pos].BestFit)
                {
                    pos = i;
                }

            return Neighbors[pos].BestPosition;
        }

        # endregion

    }
}
