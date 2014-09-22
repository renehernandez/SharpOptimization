using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpOptimization.Numeric
{

    // Random Generator class using multiply with carry method
    public static class MwcRandom
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

        public static uint NextUint()
        {
            mZ = 36969 * (mZ & 65535) + (mZ >> 16);
            mW = 18000 * (mW & 65535) + (mW >> 16);

            return (mZ << 16) + mW;
        }

        public static double NextDouble()
        {
            uint u = NextUint();

            return (u + 1.0) / ((1 << 30) * 4L + 2);
        }

    }
}
