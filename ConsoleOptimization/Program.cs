using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SharpOptimization.AutoDiff;
using SharpOptimization.AutoDiff.Funcs;

namespace ConsoleOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new Variable();
            var x1 = new Variable();
            var y = new Variable();
            var z = new Variable();

            var sphere = Sphere(x, y, z, x1);
            var compiledSphere = sphere.Compile(x, y, z, x1);

            Console.WriteLine("Sphere Evaluation");
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                Sphere(1, 1, 1, 1);
            }
            watch.Stop();
            Console.WriteLine("Normal Function Evaluation: {0}", watch.ElapsedMilliseconds);

            watch.Restart();

            for (int i = 0; i < 1000000; i++)
            {
                sphere.Eval(new []{x, y, z, x1}, 1, 1, 1, 1);
            }

            watch.Stop();
            Console.WriteLine("Term Function Evaluation: {0}", watch.ElapsedMilliseconds);

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                compiledSphere.Eval(1, 1, 1, 1);
            }
            watch.Stop();
            Console.WriteLine("Compiled Function Evaluation: {0}", watch.ElapsedMilliseconds);

            Console.WriteLine("Rosenbrock Evaluation");

            var rosen = Rosenbrock(x, y, z, x1);
            var compiledRosen = rosen.Compile(x, y, z, x1);

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                Rosenbrock(1, 1, 1, 1);
            }
            watch.Stop();
            Console.WriteLine("Normal Function Evaluation: {0}", watch.ElapsedMilliseconds);

            watch.Restart();

            for (int i = 0; i < 1000000; i++)
            {
                rosen.Eval(new[] { x, y, z, x1 }, 1, 1, 1, 1);
            }

            watch.Stop();
            Console.WriteLine("Term Function Evaluation: {0}", watch.ElapsedMilliseconds);

            watch.Restart();
            for (int i = 0; i < 1000000; i++)
            {
                compiledRosen.Eval(1, 1, 1, 1);
            }
            watch.Stop();
            Console.WriteLine("Compiled Function Evaluation: {0}", watch.ElapsedMilliseconds);
        }

        public static int Test(out int y)
        {
            y = 20;
            return 10;
        }

        public static double Sphere(params double[] values)
        {
            return values.Aggregate(0.0, (accum, curr) => accum + curr*curr);
        }

        public static Func Sphere(params Variable[] vars)
        {
            var zero = Term.Zero();

            var result = zero + vars[0] * vars [0];

            return vars.Skip(1).Aggregate(result, (current, x) => current + x*x);
        }

        public static double Rosenbrock(params double[] values)
        {
            double res = 0;
            int top = values.Length;
            int i = 0;

            while (i < top - 1)
            {
                res += 100 * Math.Pow(values[i + 1] - Math.Pow(values[i], 2), 2) + Math.Pow(values[i] - 1, 2);
                i += 1;
            }
            return res;
        }

        public static Func Rosenbrock(params Variable[] vars)
        {
            var res = IdentityFunc.Identity(0);
            int top = vars.Length;
            int i=1;


            while (i < top - 1)
            {
                res += 100 * DMath.Pow(vars[i + 1] - DMath.Pow(vars[i], 2), 2) + DMath.Pow(vars[i] - 1, 2);
                i += 1;
            }

            return res;
        }

        public static double Rastrigin(params double[] values)
        {
            return values.Sum(x => x*x - 10*Math.Cos(2*Math.PI*x) + 10);
        }

        public static Func Rastrigin(params Variable[] vars)
        {
            var res = IdentityFunc.Identity(0);

            return vars.Aggregate(res, (accum, x) => accum + x*x - 10*DMath.Cos(2*Math.PI*x) + 10);
        }

    }
}
