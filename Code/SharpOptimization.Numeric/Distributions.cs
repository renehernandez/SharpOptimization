using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpOptimization.Numeric
{
    public static class Distributions
    {

        public static Func<double> ExponentialFunc(double lambda)
        {
            var random = new Random();

            return () =>
            {
                double u = random.NextDouble();
                return u*Math.Log(1/(1 - u));
            };
        }

        public static Func<double> UniformFunc(double a, double b)
        {
            var random = new Random();
            

            return () =>
            {
                double u = random.NextDouble();
                return u*(b - a) + a;
            };
        }

        public static Func<double> NormalFunc(double mu, double sigma)
        {
            var random = new Random();

            return () =>
            {
                double u1 = 1 - random.NextDouble();
                double u2 = random.NextDouble();
                double n = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
                return sigma*n + mu;
            };
        }

        public static Func<double> BernoulliFunc(double p)
        {
            var random = new Random();

            return () =>
            {
                double u = random.NextDouble();
                return u <= p ? 1 : 0;
            };
        }

        public static Func<double> BinomialFunc(double n, double p)
        {
            var random = new Random();

            return () =>
            {
                double res = 0;
                var func = BernoulliFunc(p);

                for (int i = 0; i < n; i++)
                    res += func();

                return res;
            };

        } 

    }
}
