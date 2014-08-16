using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpOptimization.Numeric;

namespace SharpOptimization.UnitTests
{

    [TestFixture]
    public class VectorTest
    {

        [Test]
        public void CreateEmptyVector()
        {
            Vector vector = new Vector();

            Assert.AreEqual(0, vector.Length);
        }

        [Test]
        public void CreateVectorParamsDoubleArray()
        {
            Vector vector = new Vector(1, 2, 3, 4, 5);

            Assert.AreEqual(5, vector.Length);
        }

    }
}
