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

        public double GlobalBestFit { get; private set; }

        public Vector GlobalBestPosition { get; private set; }

        public int NumberOfParticles { get; private set; }

        public int NumberOfNeighborsByParticle { get; private set; }

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

        # region Private Methods

        private void SeedPopulation(CompiledFunc func, Tuple<Vector, Vector> bounds)
        {
            double min = bounds.Item1.Min();
            double max = bounds.Item2.Max();

            var dist = Distributions.ExponentialFunc(1.0/(min*2 + max*2 + 1.5));

            ParticlesSet =
                Enumerable.Range(0, NumberOfParticles)
                    .Select(i => new Particle(func, func.Dimension, NumberOfNeighborsByParticle, bounds, dist)).ToArray();

            int index = 0;

            for (int i = 0; i < NumberOfParticles; i++)
            {
                if (ParticlesSet[i].BestFit < ParticlesSet[index].BestFit)
                    index = i;
                for (int j = 0; j < NumberOfNeighborsByParticle; j++)
                {
                    ParticlesSet[i].Neighbors[j] = ParticlesSet[(i + 1 + j)%NumberOfParticles];
                }
            }

            GlobalBestPosition = ParticlesSet[index].BestPosition;
            GlobalBestFit = ParticlesSet[index].BestFit;
            CurrentIteration = 0;
        }

        # endregion

        protected override Vector Minimize(CompiledFunc func, Vector x = null, Tuple<Vector, Vector> bounds = null)
        {
            SeedPopulation(func, bounds);

            if (x != null)
            {
                double fit = func.Eval(x);

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
    }
}
