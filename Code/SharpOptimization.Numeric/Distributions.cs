using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpOptimization.Numeric
{
    public static class Distributions
    {

        // Random Generator class using multiply with carry method
        private static class MwcRandom
        {

            private static uint mZ;
            private static uint mW;

            static MwcRandom()
            {
              // These values are not magical, just the default values Marsaglia used.              
                // Any pair of unsigned integers should be fine.
                mW = 521288629;
                mZ = 362436069;
            }

            private static uint NextUint()
            {
                mZ = 36969*(mZ & 65535) + (mZ >> 16);
                mW = 18000 * (mW & 65535) + (mW >> 16);

                return (mZ << 16) + mW;
            }

            public static double NextDouble()
            {
                uint u = NextUint();

                return (u + 1.0)/((1 << 32) + 2);
            }

        }

        # region Distribution Delegates

        public static Func<double> ExponentialFunc(double lambda)
        {

            return () =>
            {
                double u = MwcRandom.NextDouble();
                return u*Math.Log(1/(1 - u));
            };
        }

        public static Func<double> UniformFunc(double a, double b)
        {

            return () =>
            {
                double u = MwcRandom.NextDouble();
                return u*(b - a) + a;
            };
        }

        public static Func<double> NormalFunc(double mu, double sigma)
        {
            return () =>
            {
                double u1 = 1 - MwcRandom.NextDouble();
                double u2 = MwcRandom.NextDouble();
                double n = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
                return sigma*n + mu;
            };
        }

        public static Func<double> BernoulliFunc(double p)
        {
            return () =>
            {
                double u = MwcRandom.NextDouble();
                return u <= p ? 1 : 0;
            };
        }

        public static Func<double> BinomialFunc(double n, double p)
        {
            return () =>
            {
                double res = 0;
                var func = BernoulliFunc(p);

                for (int i = 0; i < n; i++)
                    res += func();

                return res;
            };

        }

        # endregion

        # region Distribution Values

        public static double Exponential(double lambda)
        {
            return ExponentialFunc(lambda)();
        }

        public static double Uniform(double a, double b)
        {
            return UniformFunc(a, b)();
        }

        public static double Normal(double mu, double sigma)
        {
            return NormalFunc(mu, sigma)();
        }

        public static double Bernoulli(double p)
        {
            return BernoulliFunc(p)();
        }

        public static double Binomial(double n, double p)
        {
            return BinomialFunc(n, p)();
        }
        # endregion

    }
}
