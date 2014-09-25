using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpOptimization.AutoDiff.Compiler;
using SharpOptimization.Numeric;

namespace SharpOptimization.Optimizer.LineSearch
{
    public static class LinearSearch
    {

        public static double C1 
        {
            get { return 1e-4; }
        }

        public static double C2
        {
            get { return 0.9; }
        }

        public static double EPS
        {
            get { return 1e-8; }
        }

        public static double MaxAlpha
        {
            get { return 100; }
        }

        public static double Wolfe(CompiledFunc func, Vector x, Vector dir)
        {
            double a = 0;
            double ai = 1;
            double fPrev = 0, fCurr = 0, diff = 0;

            double fZero = func.Eval(x);
            var normDir = dir.Normalize();

            double diffZero = (func.Differentiate(x)*normDir).Sum();

            while(ai < MaxAlpha)
            {
                fPrev = func.Eval(x + a*dir);
                fCurr = func.Eval(x + ai*dir);

                if (fCurr > fZero + C1*ai*diffZero || (fCurr > fPrev && ai > 1))
                    return Zoom(func, x, dir, a, ai, fZero, diffZero);

                diff = (func.Differentiate(x + ai*dir)*normDir).Sum();

                if (Math.Abs(diff) <= -C2*diffZero)
                    return ai;

                if (diff >= 0)
                    return Zoom(func, x, dir, ai, a, fZero, diffZero);

                a = ai;
                ai *= 1.5;
            }

            return ai;
        }

        private static double Zoom(CompiledFunc func, Vector x, Vector dir, double aLow, double aHigh, double fZero, double diffZero)
        {
            var normDir = dir.Normalize();
            double aMid = 0;
            double fValue = 0;
            double diff = 0;

            while (Math.Abs(aLow - aHigh) > EPS)
            {
                aMid = aLow + (aHigh - aLow)/2;
                fValue = func.Eval(x + aMid*dir);

                if (fValue > fZero + C1*aMid*diffZero || fValue >= func.Eval(x + aLow*dir))
                    aHigh = aMid;
                else
                {
                    diff = (func.Differentiate(x + aMid*dir)*normDir).Sum();

                    if (Math.Abs(diff) <= -C2*diffZero)
                        return aMid;

                    if (diff*(aHigh - aLow) >= 0)
                        aHigh = aLow;

                    aLow = aMid;
                }
            }

            return aMid;
        }

    }
}
