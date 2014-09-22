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

        public double GlobalFitValue { get; private set; }

        public Vector GlobalBestVector { get; private set; }

        public int NumberOfParticles { get; private set; }

        public int NumberOfNeighborsByParticle { get; private set; }

        public int NumberOfDimensions { get; private set; }

        public Particle[] ParticlesSet { get; private set; }

        public Tuple<Vector, Vector> Bounds { get; private set; }

        # endregion

        # region Constructors

        public PsoOptimizer(int iterations, int dimensions, int particlesNumber, int neighborsNumber, Tuple<Vector, Vector> bounds, double eps = 1e-8) : base(iterations, eps)
        {
            if(neighborsNumber >= particlesNumber)
                throw new Exception("There must be less neighbors than particles quantity");

            NumberOfParticles = particlesNumber;
            NumberOfNeighborsByParticle = neighborsNumber;
            NumberOfDimensions = dimensions;
            Bounds = bounds;
        }

        # endregion

        # region Private Methods

        private void SeedPopulation(CompiledFunc func)
        {
            ParticlesSet =
                Enumerable.Range(0, NumberOfParticles)
                    .Select(i => new Particle(func, NumberOfDimensions, NumberOfNeighborsByParticle, Bounds, Distributions.NormalFunc(Bounds.Item1[0], Bounds.Item2[0]))).ToArray();

            for(int i = 0; i < NumberOfParticles; i++)
                for (int j = 0; j < NumberOfNeighborsByParticle; j++)
                {
                    ParticlesSet[i].Neighbors[j] = ParticlesSet[(i + 1 + j) % NumberOfParticles];
                }

        }

        # endregion

        protected override Vector Minimize(CompiledFunc func, Vector x)
        {
            throw new NotImplementedException();
        }
    }
}
