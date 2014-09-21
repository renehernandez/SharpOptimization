using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpOptimization.Numeric;

namespace SharpOptimization.UnitTests
{
    [TestFixture]
    public class MatrixTest
    {

        [Test]
        public void CreateEmptyMatrix()
        {
            var matrix = new Matrix();

            Assert.AreEqual(0, matrix.Rows);
            Assert.AreEqual(0, matrix.Columns);
        }

        [Test]
        public void CreateNbyMMatrix()
        {
            var matrix = new Matrix(10, 5);

            Assert.AreEqual(10, matrix.Rows);
            Assert.AreEqual(5, matrix.Columns);
        }

        [Test]
        public void CreateSquareMatrix()
        {
            var matrix = new Matrix(20);

            Assert.AreEqual(20, matrix.Rows);
            Assert.AreEqual(20, matrix.Columns);
        }

        [Test]
        public void CreateMatrixFromVectorArray()
        {
            var matrix = new Matrix(Enumerable.Range(0, 5).Select(i => new Vector(6)));

            Assert.AreEqual(5, matrix.Rows);
            Assert.AreEqual(6, matrix.Columns);
        }

        [Test]
        public void MatrixTransposeDimensions()
        {
            var matrix = new Matrix(2, 3);

            var transpose = matrix.Transpose();

            Assert.AreEqual(3, transpose.Rows);
            Assert.AreEqual(2, transpose.Columns);
        }

        [Test]
        public void MatrixTransposeValues()
        {
            var matrix = new Matrix(new []{1, 2}, new []{3, 4});

            var transpose = matrix.Transpose();

            Assert.AreEqual(1, transpose[0, 0]);
            Assert.AreEqual(2, transpose[1, 0]);
            Assert.AreEqual(3, transpose[0, 1]);
            Assert.AreEqual(4, transpose[1, 1]);
        }

        [Test]
        public void IdentityMatrix()
        {
            var iden = Matrix.Identity(5);

            for(int i = 0; i < 5; i++)
                Assert.AreEqual(1, iden[i, i]);

            for(int i = 0; i < 5; i++)
                for(int j = 0; j < 5; j++)
                    if(i != j)
                        Assert.AreEqual(0, iden[i,j]);
        }

        [Test]
        public void DotValues()
        {
            var m1 = new Matrix(new[] {1, 2, 3}, new[] {1, 2, 3});
            var m2 = new Matrix(new[] { 4, 5 }, new[] { 4, 5 }, new []{4, 5});

            var res = m1.Dot(m2);

            Assert.AreEqual(2, res.Rows);
            Assert.AreEqual(2, res.Columns);

            Assert.AreEqual(24, res[0, 0]);
            Assert.AreEqual(30, res[0, 1]);
            Assert.AreEqual(24, res[1, 0]);
            Assert.AreEqual(30, res[1, 1]);

        }
    }
}
