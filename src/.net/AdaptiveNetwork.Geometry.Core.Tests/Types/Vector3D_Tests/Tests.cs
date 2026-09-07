using AdaptiveNetwork.Geometry.Core.Types;

using static AdaptiveNetwork.Geometry.Core.Tests.Types.Vector3D_Tests.TestCaseData;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.Vector3D_Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Vector3D"/> struct, verifying its functionality and behavior.
    /// </summary>
    [TestFixture]
    internal sealed class Tests
    {
        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;
            Vector3D vector = new(x, y, z);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(vector.X, Is.EqualTo(x));
                Assert.That(vector.Y, Is.EqualTo(y));
                Assert.That(vector.Z, Is.EqualTo(z));
            }
        }

        [Test]
        public void DefaultConstructor_ShouldInitializeToZero()
        {
            Vector3D vector = new();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(vector.X, Is.EqualTo(0));
                Assert.That(vector.Y, Is.EqualTo(0));
                Assert.That(vector.Z, Is.EqualTo(0));
            }
        }

        [Test]
        public void Origin_ShouldReturnVectorAtOrigin()
        {
            Vector3D origin = Vector3D.Origin();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(origin.X, Is.EqualTo(0));
                Assert.That(origin.Y, Is.EqualTo(0));
                Assert.That(origin.Z, Is.EqualTo(0));
            }
        }

        [Test]
        public void ToString_ShouldReturnFormattedString()
        {
            Vector3D vector = new(1.0, 2.0, 3.0);
            string expected = "1, 2, 3";
            string result = vector.ToString();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetHashCode_ShouldThrowNotImplementedException()
        {
            Vector3D vector = new(1.0, 2.0, 3.0);
            Assert.Throws<NotImplementedException>(() => vector.GetHashCode());
        }

        [Test]
        public void Equals_ShouldThrowNotImplementedException()
        {
            Vector3D vector = new(1.0, 2.0, 3.0);
            Assert.Throws<NotImplementedException>(() => vector.Equals(new object()));
        }

        [Test]
        public void EqualityOperator_ShouldThrowNotImplementedException()
        {
            Vector3D a = new(1.0, 2.0, 3.0);
            Vector3D b = new(1.0, 2.0, 3.0);
            Assert.Throws<NotImplementedException>(() => _ = a == b);
        }

        [Test]
        public void InequalityOperator_ShouldThrowNotImplementedException()
        {
            Vector3D a = new(1.0, 2.0, 3.0);
            Vector3D b = new(1.0, 2.0, 3.0);
            Assert.Throws<NotImplementedException>(() => _ = a != b);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenVectorsAreWithinTolerance()
        {
            Vector3D a = new(1.0, 2.0, 3.0);
            Vector3D b = new(1.00001, 2.00001, 3.00001);
            double tolerance = 0.0001;
            bool result = a.EquatableWithinTolerance(b, tolerance);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenVectorsAreOutsideTolerance()
        {
            Vector3D a = new(1.0, 2.0, 3.0);
            Vector3D b = new(1.0001, 2.0001, 3.0001);
            double tolerance = 0.00001;
            bool result = a.EquatableWithinTolerance(b, tolerance);
            Assert.That(result, Is.False);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenVectorsAreExactlyTheSame_WithZeroTolerance()
        {
            Vector3D a = new(1.0, 2.0, 3.0);
            Vector3D b = new(1.0, 2.0, 3.0);
            bool result = a.EquatableWithinTolerance(b);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenVectorsAreDifferent_WithZeroTolerance()
        {
            Vector3D a = new(1.0, 2.0, 3.0);
            Vector3D b = new(1.00001, 2.00001, 3.00001);
            bool result = a.EquatableWithinTolerance(b);
            Assert.That(result, Is.False);
        }

        [Test]
        public void EquatableWithinTolerance_WithPoint3D_ShouldReturnTrue_WhenWithinTolerance()
        {
            Vector3D vector = new(1.0, 2.0, 3.0);
            Point3D point = new(1.00001, 2.00001, 3.00001);
            double tolerance = 0.0001;
            bool result = vector.EquatableWithinTolerance(point, tolerance);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_WithPoint3D_ShouldReturnFalse_WhenOutsideTolerance()
        {
            Vector3D vector = new(1.0, 2.0, 3.0);
            Point3D point = new(1.0001, 2.0001, 3.0001);
            double tolerance = 0.00001;
            bool result = vector.EquatableWithinTolerance(point, tolerance);
            Assert.That(result, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(CrossTestCaseData), nameof(CrossTestCaseData.Data))]
        public void Cross_ShouldReturnPerpendicularVector(CrossData data)
        {
            Vector3D result = Vector3D.Cross(data.A, data.B);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(data.Expected.X));
                Assert.That(result.Y, Is.EqualTo(data.Expected.Y));
                Assert.That(result.Z, Is.EqualTo(data.Expected.Z));
            }
        }

        [Test]
        [TestCaseSource(typeof(DotTestCaseData), nameof(DotTestCaseData.Data))]
        public void Dot_ShouldReturnScalarProduct(DotData data)
        {
            double result = Vector3D.Dot(data.A, data.B);
            Assert.That(result, Is.EqualTo(data.Expected));
        }

        [Test]
        public void Normalize_ShouldReturnUnitVector()
        {
            Vector3D vector = new(3, 0, 4);
            Vector3D result = vector.Normalize();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(0.6));
                Assert.That(result.Y, Is.EqualTo(0));
                Assert.That(result.Z, Is.EqualTo(0.8));
            }
        }

        [Test]
        public void Normalize_ShouldThrowInvalidOperationException_WhenVectorIsZeroLength()
        {
            Vector3D vector = new(0, 0, 0);
            Assert.Throws<InvalidOperationException>(() => vector.Normalize());
        }

        [Test]
        public void Magnitude_ShouldReturnLength()
        {
            Vector3D vector = new(3, 4, 0);
            double result = vector.Magnitude();
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Magnitude_ShouldReturnZero_WhenVectorIsZero()
        {
            Vector3D vector = new(0, 0, 0);
            double result = vector.Magnitude();
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void MagnitudeSquared_ShouldReturnSumOfSquares()
        {
            Vector3D vector = new(1, 2, 3);
            double result = vector.MagnitudeSquared();
            Assert.That(result, Is.EqualTo(14));
        }

        [Test]
        public void AdditionOperator_ShouldReturnComponentWiseSum()
        {
            Vector3D a = new(1, 2, 3);
            Vector3D b = new(4, 5, 6);
            Vector3D result = a + b;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(5));
                Assert.That(result.Y, Is.EqualTo(7));
                Assert.That(result.Z, Is.EqualTo(9));
            }
        }

        [Test]
        public void SubtractionOperator_ShouldReturnComponentWiseDifference()
        {
            Vector3D a = new(4, 5, 6);
            Vector3D b = new(1, 2, 3);
            Vector3D result = a - b;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(3));
                Assert.That(result.Y, Is.EqualTo(3));
                Assert.That(result.Z, Is.EqualTo(3));
            }
        }

        [Test]
        public void MultiplicationOperator_VectorByScalar_ShouldReturnComponentWiseProduct()
        {
            Vector3D vector = new(1, 2, 3);
            Vector3D result = vector * 2;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(2));
                Assert.That(result.Y, Is.EqualTo(4));
                Assert.That(result.Z, Is.EqualTo(6));
            }
        }

        [Test]
        public void MultiplicationOperator_ScalarByVector_ShouldReturnComponentWiseProduct()
        {
            Vector3D vector = new(1, 2, 3);
            Vector3D result = 2 * vector;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(2));
                Assert.That(result.Y, Is.EqualTo(4));
                Assert.That(result.Z, Is.EqualTo(6));
            }
        }

        [Test]
        public void DivisionOperator_ShouldReturnComponentWiseQuotient()
        {
            Vector3D vector = new(2, 4, 6);
            Vector3D result = vector / 2;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(1));
                Assert.That(result.Y, Is.EqualTo(2));
                Assert.That(result.Z, Is.EqualTo(3));
            }
        }

        [Test]
        public void DivisionOperator_ShouldThrowDivideByZeroException_WhenScalarIsZero()
        {
            Vector3D vector = new(2, 4, 6);
            Assert.Throws<DivideByZeroException>(() => _ = vector / 0);
        }

        [Test]
        public void DivisionOperator_ShouldThrowDivideByZeroException_WhenScalarIsNegative()
        {
            Vector3D vector = new(2, 4, 6);
            Assert.Throws<DivideByZeroException>(() => _ = vector / -1);
        }
    }
}
