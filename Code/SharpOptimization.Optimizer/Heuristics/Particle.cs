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

        /// <summary>
        /// Gets the constant related to the best position of each particle.
        /// </summary>
        public double C1 { get { return 2.05; } }

        /// <summary>
        /// Gets the constant related to the best position know by the neighbors.
        /// </summary>
        public double C2 { get { return 2.05; } }

        /// <summary>
        /// Gets the constriction factor applied to velocity formula.
        /// </summary>
        public double Chi { get { return 0.72984; } }

        /// <summary>
        /// Gets the dimension of vector representing the particle position.
        /// </summary>
        public int NumberOfDimensions { get; private set; }

        /// <summary>
        /// Gets the thresholds for the possible positions of the particle.
        /// </summary>
        public Tuple<Vector, Vector> Bounds { get; private set; }

        /// <summary>
        /// Gets or sets the best position known so far by the particle.
        /// </summary>
        public Vector BestPosition { get; set; }

        /// <summary>
        /// Gets the current position of the particle.
        /// </summary>
        public Vector CurrentPosition { get; private set; }

        /// <summary>
        /// Gets the velocity of the particle.
        /// </summary>
        public Vector Velocity { get; private set; }

        /// <summary>
        /// Gets or sets the best fit reached so far by the particle. 
        /// </summary>
        public double BestFit { get; set; }

        /// <summary>
        /// Gets the current fit of the particle.
        /// </summary>
        public double CurrentFit { get; private set; }

        /// <summary>
        /// Gets the function used to search by particles.
        /// </summary>
        public CompiledFunc Func { get; private set; }

        /// <summary>
        /// Gets the neighbors set of the particle.
        /// </summary>
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

        /// <summary>
        /// Updates the current position of the particle following the formulaes defined in the standard paper.
        /// </summary>
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

        /// <summary>
        /// Checks if the current particle position is within the bounds of function search space.
        /// </summary>
        /// <returns>True if the particle position is feasible, false otherwise.</returns>
        public bool IsFeasible()
        {
            for (int i = 0; i < NumberOfDimensions; i++)
            {
                if (Bounds.Item1[i] > CurrentPosition[i] || Bounds.Item2[i] < CurrentPosition[i])
                    return false;
            }

            return true;
        }

        # endregion

        # region Private Methods

        /// <summary>
        /// Finds the best value from the neighbors set of the current particle and particle itself.
        /// </summary>
        /// <returns>Returns a vector representing the minimun value known so far by any of the neighbors and the particle.</returns>
        private Vector BestPositionFromNeighbors()
        {
            int pos = 0;

            for (int i = 1; i < Neighbors.Length; i++)
                if (Neighbors[i].BestFit < Neighbors[pos].BestFit)
                {
                    pos = i;
                }

            return Neighbors[pos].BestFit < BestFit ? Neighbors[pos].BestPosition : BestPosition;
        }

        # endregion

    }
}
