using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;
using SharpOptimization.Optimizer.Heuristics;

namespace SharpOptimization.Optimizer
{
    public class PsoOptimizer : AbstractOptimizer
    {

        # region Public Properties

        /// <summary>
        /// Gets the global best fit known by the entire swarm so far.
        /// </summary>
        public double GlobalBestFit { get; private set; }

        /// <summary>
        /// Gets the global best position known by entire swarm so far.
        /// </summary>
        public Vector GlobalBestPosition { get; private set; }

        /// <summary>
        /// Gets the number of particles in the swarm.
        /// </summary>
        public int NumberOfParticles { get; private set; }

        /// <summary>
        /// Gets the number of neighbors by particle.
        /// </summary>
        public int NumberOfNeighborsByParticle { get; private set; }

        /// <summary>
        /// Gets the set of particles representing the swarm.
        /// </summary>
        public Particle[] ParticlesSet { get; private set; }

        # endregion

        # region Constructors

        public PsoOptimizer(int iterations, int particlesNumber, int neighborsNumber, double eps = 1e-8) : base(iterations, eps)
        {
            if(neighborsNumber >= particlesNumber)
                throw new Exception("There must be less neighbors than particles quantity");

            NumberOfParticles = particlesNumber;
            NumberOfNeighborsByParticle = neighborsNumber;
        }

        # endregion

        # region Protected Methods

        protected override Vector Minimize(CompiledFunc f, Vector x = null, Tuple<Vector, Vector> bounds = null)
        {
            SeedPopulation(f, bounds);

            if (x != null)
            {
                double fit = f.Eval(x);

                if (fit < GlobalBestFit)
                {
                    GlobalBestFit = fit;
                    GlobalBestPosition = x;
                    ParticlesSet[0].BestFit = GlobalBestFit;
                    ParticlesSet[0].BestPosition = GlobalBestPosition;
                }
            }

            while (CurrentIteration < IterationsNumber)
            {
                CurrentIteration++;

                foreach (var particle in ParticlesSet)
                {
                    particle.Update();

                    if (particle.BestFit < GlobalBestFit)
                    {
                        GlobalBestFit = particle.BestFit;
                        GlobalBestPosition = particle.BestPosition;
                    }
                }
            }
            return GlobalBestPosition;
        }

        # endregion

        # region Private Methods

        /// <summary>
        /// Creates the particle set with non-uniform values and selects an initial best position and best fit 
        /// </summary>
        /// <param name="f">Function to which the minimum will be looking for.</param>
        /// <param name="bounds">Limits of function space search.</param>
        private void SeedPopulation(CompiledFunc f, Tuple<Vector, Vector> bounds)
        {
            CurrentIteration = 0;

            double min = bounds.Item1.Min();
            double max = bounds.Item2.Max();

            var dist = Distributions.ExponentialFunc(1.0 / (min * 2 + max * 2 + 1.5));

            ParticlesSet =
                Enumerable.Range(0, NumberOfParticles)
                    .Select(i => new Particle(f, f.Dimension, NumberOfNeighborsByParticle, bounds, dist)).ToArray();

            int index = -1;
            GlobalBestPosition = new Vector(f.Dimension);

            for (int i = 0; i < GlobalBestPosition.Length; i++)
                GlobalBestPosition[i] = bounds.Item1[i] + (bounds.Item2[i] - bounds.Item1[i])/2;

            GlobalBestFit = f.Eval(GlobalBestPosition);

            for (int i = 0; i < NumberOfParticles; i++)
            {
                if (ParticlesSet[i].IsFeasible() && ParticlesSet[i].BestFit < GlobalBestFit)
                {
                    index = i;
                    GlobalBestFit = ParticlesSet[i].BestFit;
                }

                // Setting particle neighbors
                for (int j = 0; j < NumberOfNeighborsByParticle; j++)
                    ParticlesSet[i].Neighbors[j] = ParticlesSet[(i + 1 + j) % NumberOfParticles];
            }

            if (index == -1)
            {
                ParticlesSet[0].BestPosition = GlobalBestPosition;
                ParticlesSet[0].BestFit = GlobalBestFit;

                return;
            }

            GlobalBestPosition = ParticlesSet[index].BestPosition;
            GlobalBestFit = ParticlesSet[index].BestFit;      
        }

        # endregion


    }
}
