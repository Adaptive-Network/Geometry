using AdaptiveNetwork.Geometry.Core.Types;

using static AdaptiveNetwork.Geometry.Core.Tests.Types.LineSegment3D_Tests.TestCaseData;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.LineSegment3D_Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="LineSegment3D"/> class, verifying its functionality and behavior.
    /// </summary>
    [TestFixture]
    internal sealed class Tests
    {
        [Test]
        public void DefaultConstructor_ShouldInitializeToOriginPoints()
        {
            LineSegment3D segment = new();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(segment.Start.EquatableWithinTolerance(Point3D.Origin), Is.True);
                Assert.That(segment.End.EquatableWithinTolerance(Point3D.Origin), Is.True);
            }
        }

        [Test]
        public void Constructor_WithStartAndEnd_ShouldInitializeProperties()
        {
            Point3D start = new(1, 2, 3);
            Point3D end = new(4, 5, 6);
            LineSegment3D segment = new(start, end);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(segment.Start.EquatableWithinTolerance(start), Is.True);
                Assert.That(segment.End.EquatableWithinTolerance(end), Is.True);
            }
        }

        [Test]
        public void Constructor_WithStartAndDirection_ShouldInitializeEndAsStartPlusDirection()
        {
            Point3D start = new(1, 2, 3);
            Vector3D direction = new(1, 1, 1);
            LineSegment3D segment = new(start, direction);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(segment.Start.EquatableWithinTolerance(start), Is.True);
                Assert.That(segment.End.EquatableWithinTolerance(new Point3D(2, 3, 4)), Is.True);
            }
        }

        [Test]
        [TestCaseSource(typeof(LengthTestCaseData), nameof(LengthTestCaseData.Data))]
        public void LengthSquared_ShouldReturnSquaredDistanceBetweenStartAndEnd(LengthData data)
        {
            LineSegment3D segment = new(data.Start, data.End);
            double result = segment.LengthSquared();
            Assert.That(result, Is.EqualTo(data.ExpectedLengthSquared));
        }

        [Test]
        [TestCaseSource(typeof(LengthTestCaseData), nameof(LengthTestCaseData.Data))]
        public void Length_ShouldReturnDistanceBetweenStartAndEnd(LengthData data)
        {
            LineSegment3D segment = new(data.Start, data.End);
            double result = segment.Length();
            Assert.That(result, Is.EqualTo(data.ExpectedLength));
        }

        [Test]
        public void GetHashCode_ShouldThrowNotImplementedException()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            Assert.Throws<NotImplementedException>(() => segment.GetHashCode());
        }

        [Test]
        public void Equals_ShouldThrowNotImplementedException()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            Assert.Throws<NotImplementedException>(() => segment.Equals(new object()));
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenSegmentsAreWithinTolerance()
        {
            LineSegment3D a = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D b = new(new Point3D(0.00001, 0, 0), new Point3D(1, 1, 1.00001));
            double tolerance = 0.0001;
            bool result = a.EquatableWithinTolerance(b, tolerance);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenSegmentsAreOutsideTolerance()
        {
            LineSegment3D a = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D b = new(new Point3D(0.001, 0, 0), new Point3D(1, 1, 1));
            double tolerance = 0.00001;
            bool result = a.EquatableWithinTolerance(b, tolerance);
            Assert.That(result, Is.False);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenSegmentsAreExactlyTheSame_WithZeroTolerance()
        {
            LineSegment3D a = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D b = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            bool result = a.EquatableWithinTolerance(b);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenSegmentsAreDifferent_WithZeroTolerance()
        {
            LineSegment3D a = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D b = new(new Point3D(0.00001, 0, 0), new Point3D(1, 1, 1));
            bool result = a.EquatableWithinTolerance(b);
            Assert.That(result, Is.False);
        }

        [Test]
        [TestCaseSource(typeof(VectorTranslateTestCaseData), nameof(VectorTranslateTestCaseData.Data))]
        public void TranslateWithVector_ShouldReturnTranslatedSegment(VectorTranslationData data)
        {
            LineSegment3D result = data.Segment.Translate(data.Vector);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(data.ExpectedStart), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(data.ExpectedEnd), Is.True);
            }
        }

        [Test]
        [TestCaseSource(typeof(VectorTranslateTestCaseData), nameof(VectorTranslateTestCaseData.Data))]
        public void TranslateWithVector_ShouldNotMutateOriginalSegment(VectorTranslationData data)
        {
            Point3D originalStart = data.Segment.Start;
            Point3D originalEnd = data.Segment.End;
            data.Segment.Translate(data.Vector);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(data.Segment.Start.EquatableWithinTolerance(originalStart), Is.True);
                Assert.That(data.Segment.End.EquatableWithinTolerance(originalEnd), Is.True);
            }
        }

        [Test]
        public void TranslateWithCoordinates_ShouldReturnTranslatedSegment()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D result = segment.Translate(1, 2, 3);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(new Point3D(1, 2, 3)), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(new Point3D(2, 3, 4)), Is.True);
            }
        }

        [Test]
        public void TranslateX_ShouldReturnSegmentTranslatedAlongX()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D result = segment.TranslateX(5);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(new Point3D(5, 0, 0)), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(new Point3D(6, 1, 1)), Is.True);
            }
        }

        [Test]
        public void TranslateY_ShouldReturnSegmentTranslatedAlongY()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D result = segment.TranslateY(5);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(new Point3D(0, 5, 0)), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(new Point3D(1, 6, 1)), Is.True);
            }
        }

        [Test]
        public void TranslateZ_ShouldReturnSegmentTranslatedAlongZ()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D result = segment.TranslateZ(5);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(new Point3D(0, 0, 5)), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(new Point3D(1, 1, 6)), Is.True);
            }
        }

        [Test]
        public void TranslateXY_ShouldReturnSegmentTranslatedAlongXAndY()
        {
            LineSegment3D segment = new(new Point3D(0, 0, 0), new Point3D(1, 1, 1));
            LineSegment3D result = segment.TranslateXY(5, 10);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(new Point3D(5, 10, 0)), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(new Point3D(6, 11, 1)), Is.True);
            }
        }

        [Test]
        [TestCaseSource(typeof(RotationTestCaseData), nameof(RotationTestCaseData.Data))]
        public void RotateAroundAxisRadians_ShouldReturnSegmentRotatedAroundPosition(RotationData data)
        {
            LineSegment3D result = data.Segment.RotateAroundAxisRadians(data.Position, data.Axis, data.Radians);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(data.ExpectedStart, 1e-9), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(data.ExpectedEnd, 1e-9), Is.True);
            }
        }

        [Test]
        public void RotateAroundAxisRadians_ShouldNotMutateOriginalSegment()
        {
            LineSegment3D segment = new(new Point3D(1, 0, 0), new Point3D(2, 0, 0));
            Point3D originalStart = segment.Start;
            Point3D originalEnd = segment.End;
            segment.RotateAroundAxisRadians(Point3D.Origin, new Vector3D(0, 0, 1), Math.PI / 2);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(segment.Start.EquatableWithinTolerance(originalStart), Is.True);
                Assert.That(segment.End.EquatableWithinTolerance(originalEnd), Is.True);
            }
        }

        [Test]
        public void RotateAroundAxisDegrees_ShouldMatchRadianEquivalent()
        {
            LineSegment3D segment = new(new Point3D(1, 0, 0), new Point3D(2, 0, 0));
            Point3D position = new(0, 0, 1);
            Vector3D axis = new(0, 0, 1);
            LineSegment3D expected = segment.RotateAroundAxisRadians(position, axis, Math.PI / 2);
            LineSegment3D result = segment.RotateAroundAxisDegrees(position, axis, 90);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(expected.Start, 1e-9), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(expected.End, 1e-9), Is.True);
            }
        }

        [Test]
        public void RotateRadians_ShouldRotateAroundOrigin()
        {
            LineSegment3D segment = new(new Point3D(1, 0, 0), new Point3D(2, 0, 0));
            LineSegment3D result = segment.RotateRadians(new Vector3D(0, 0, 1), Math.PI / 2);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(new Point3D(0, 1, 0), 1e-9), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(new Point3D(0, 2, 0), 1e-9), Is.True);
            }
        }

        [Test]
        public void RotateDegrees_ShouldMatchRadianEquivalent()
        {
            LineSegment3D segment = new(new Point3D(1, 0, 0), new Point3D(2, 0, 0));
            Vector3D axis = new(0, 0, 1);
            LineSegment3D expected = segment.RotateRadians(axis, Math.PI / 2);
            LineSegment3D result = segment.RotateDegrees(axis, 90);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.Start.EquatableWithinTolerance(expected.Start, 1e-9), Is.True);
                Assert.That(result.End.EquatableWithinTolerance(expected.End, 1e-9), Is.True);
            }
        }
    }
}
