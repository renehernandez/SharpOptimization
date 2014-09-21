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
            var vector = new Vector();

            Assert.AreEqual(0, vector.Length);
        }

        [Test]
        public void CreateVectorParamsDoubleArray()
        {
            var vector = new Vector(1, 2, 3, 4, 5);

            Assert.AreEqual(5, vector.Length);
        }

        [Test]
        public void CreateVectorFromEnumerable()
        {
            var vector = new Vector(Enumerable.Range(1, 10));

            Assert.AreEqual(10, vector.Length);
        }

        [Test]
        public void DotMultiplication()
        {
            var v1 = new Vector(1, 1, 1);
            var v2 = new Vector(1, 1, 1);

            var resul = v1.Dot(v2);

            Assert.AreEqual(3, resul);
        }

        [Test]
        public void NormalizeVector()
        {
            var v1 = new Vector(1,2,3);

            var normVector = v1.Normalize();

            Assert.AreEqual(1.0, Algebra.Norm(normVector), 1e-8);
        }
    }
}
