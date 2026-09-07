using AdaptiveNetwork.Geometry.Core.Types;

using static AdaptiveNetwork.Geometry.Core.Tests.Types.Point3D_Tests.TestCaseData;


namespace AdaptiveNetwork.Geometry.Core.Tests.Types.Point3D_Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Point3D"/> struct, verifying its functionality and behavior.
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
            Point3D? _point = null;
            using (Assert.EnterMultipleScope())
            {
                Assert.DoesNotThrow(() => _point = new Point3D(x, y, z));
                Assert.That(_point, Is.Not.Null);
                Assert.That(_point!.Value, Is.InstanceOf<Point3D>());
                Assert.That(_point!.Value.X, Is.EqualTo(x));
                Assert.That(_point!.Value.Y, Is.EqualTo(y));
                Assert.That(_point!.Value.Z, Is.EqualTo(z));
            }
        }

        [Test]
        public void Origin_ShouldReturnPointAtOrigin()
        {
            Point3D origin = Point3D.Origin;
            Assert.Multiple(() =>
            {
                Assert.That(origin.X, Is.EqualTo(0));
                Assert.That(origin.Y, Is.EqualTo(0));
                Assert.That(origin.Z, Is.EqualTo(0));
            });
        }

        [Test]
        public void ToString_ShouldReturnFormattedString()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            string expected = "1, 2, 3";
            string result = point.ToString();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetHashCode_ShouldThrowNotImplementedException()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            Assert.Throws<NotImplementedException>(() => point.GetHashCode());
        }

        [Test]
        public void Equals_ShouldThrowNotImplementedException()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            Assert.Throws<NotImplementedException>(() => point.Equals(new object()));
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenPointsAreWithinTolerance()
        {
            Point3D point1 = new(1.0, 2.0, 3.0);
            Point3D point2 = new(1.00001, 2.00001, 3.00001);
            double tolerance = 0.0001;
            bool result = point1.EquatableWithinTolerance(point2, tolerance);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenPointsAreOutsideTolerance()
        {
            Point3D point1 = new(1.0, 2.0, 3.0);
            Point3D point2 = new(1.0001, 2.0001, 3.0001);
            double tolerance = 0.00001;
            bool result = point1.EquatableWithinTolerance(point2, tolerance);
            Assert.That(result, Is.False);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenPointsAreExactlyTheSame_WithZeroTolerance()
        {
            Point3D point1 = new(1.0, 2.0, 3.0);
            Point3D point2 = new(1.0, 2.0, 3.0);
            bool result = point1.EquatableWithinTolerance(point2);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenPointsAreDifferent_WithZeroTolerance()
        {
            Point3D point1 = new(1.0, 2.0, 3.0);
            Point3D point2 = new(1.00001, 2.00001, 3.00001);
            bool result = point1.EquatableWithinTolerance(point2);
            Assert.That(result, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(VectorTranslateTestCaseData), nameof(VectorTranslateTestCaseData.Data))]
        public void TranslateWithVector_ShouldReturnTranslatedPoint(VectorTranslationData testCase)
        {
            Point3D point = testCase.Point;
            Vector3D vector = testCase.Vector;
            Point3D expected = testCase.Expected;
            Point3D result = point.Translate(vector);
            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(expected.X));
                Assert.That(result.Y, Is.EqualTo(expected.Y));
                Assert.That(result.Z, Is.EqualTo(expected.Z));
            });
        }

        [Test]
        [TestCaseSource(typeof(VectorTranslateTestCaseData), nameof(VectorTranslateTestCaseData.Data))]
        public void TranslateWithVector_ShouldNotMutateOriginalPoint(VectorTranslationData testCase)
        {
            Point3D point = testCase.Point;
            Point3D original = point;
            point.Translate(testCase.Vector);
            Assert.That(point.EquatableWithinTolerance(original), Is.True);
        }

        [Test]
        [TestCaseSource(typeof(DimensionTranslationTestData), nameof(DimensionTranslationTestData.Data))]
        public void TranslateWithCoordinates_ShouldReturnTranslatedPoint(DimensionTranslationData testCase)
        {
            Point3D point = testCase.Point;
            double dx = testCase.Dx;
            double dy = testCase.Dy;
            double dz = testCase.Dz;
            Point3D expected = testCase.Expected;
            Point3D result = point.Translate(dx, dy, dz);
            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(expected.X));
                Assert.That(result.Y, Is.EqualTo(expected.Y));
                Assert.That(result.Z, Is.EqualTo(expected.Z));
            });
        }

        [Test]
        public void TranslateX_ShouldReturnPointTranslatedAlongX()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            double dx = 5.0;
            Point3D result = point.TranslateX(dx);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(6.0));
                Assert.That(result.Y, Is.EqualTo(2.0));
                Assert.That(result.Z, Is.EqualTo(3.0));
            }
        }

        [Test]
        public void TranslateY_ShouldReturnPointTranslatedAlongY()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            double dy = 5.0;
            Point3D result = point.TranslateY(dy);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(1.0));
                Assert.That(result.Y, Is.EqualTo(7.0));
                Assert.That(result.Z, Is.EqualTo(3.0));
            }
        }

        [Test]
        public void TranslateZ_ShouldReturnPointTranslatedAlongZ()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            double dz = 5.0;
            Point3D result = point.TranslateZ(dz);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(1.0));
                Assert.That(result.Y, Is.EqualTo(2.0));
                Assert.That(result.Z, Is.EqualTo(8.0));
            }
        }

        [Test]
        public void TranslateXY_ShouldReturnPointTranslatedAlongXAndY()
        {
            Point3D point = new(1.0, 2.0, 3.0);
            double dx = 5.0;
            double dy = 10.0;
            Point3D result = point.TranslateXY(dx, dy);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(6.0));
                Assert.That(result.Y, Is.EqualTo(12.0));
                Assert.That(result.Z, Is.EqualTo(3.0));
            }
        }

        [Test]
        [TestCaseSource(typeof(DistanceSquaredTestCaseData), nameof(DistanceSquaredTestCaseData.Data))]
        public void DistanceToSquared_ShouldTranslatePointAlongSquared(DistanceSquaredTestCase data)
        {
            Point3D a = data.A;
            Point3D b = data.B;
            double expectedDistanceSquared = data.ExpectedDistanceSquared;

            using (Assert.EnterMultipleScope())
            {
                double actualDistanceSquared = a.DistanceSquaredTo(b);
                Assert.That(actualDistanceSquared, Is.EqualTo(expectedDistanceSquared));
            }
        }

        [Test]
        [TestCaseSource(typeof(DistanceTestCaseData), nameof(DistanceTestCaseData.Data))]
        public void DistanceTo_ShouldTranslatePointAlongDistance(DistanceTestCase data)
        {
            Point3D a = data.A;
            Point3D b = data.B;
            double expectedDistance = data.ExpectedDistance;
            using (Assert.EnterMultipleScope())
            {
                double actualDistance = a.DistanceTo(b);
                Assert.That(actualDistance, Is.EqualTo(expectedDistance));
            }
        }
    }
}
