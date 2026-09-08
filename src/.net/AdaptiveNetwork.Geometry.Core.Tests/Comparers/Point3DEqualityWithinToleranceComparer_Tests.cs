using AdaptiveNetwork.Geometry.Core.Comparers;
using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Tests.Comparers
{
    internal sealed class Point3DEqualityWithinToleranceComparer_Tests
    {
        [Test]
        public void Constructor_DoesNotThrow_WhenToleranceIsNonNegative()
        {
            Assert.DoesNotThrow(() => new Point3DEqualityWithinToleranceComparer(0.1));
        }

        [Test]
        public void Constructor_ThrowsArgumentOutOfRangeException_WhenToleranceIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Point3DEqualityWithinToleranceComparer(-0.1));
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenPointsAreEqualWithinNoTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.0);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.0, 2.0, 3.0);
            bool result = comparer.Equals(point1, point2);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenPointsAreNotEqualWithinNoTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.0);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.0, 2.0, 4.0);
            bool result = comparer.Equals(point1, point2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenPointsAreEqualWithinTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.1);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.05, 2.05, 3.05);
            bool result = comparer.Equals(point1, point2);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenPointsAreNotEqualWithinTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.1);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.2, 2.2, 3.2);
            bool result = comparer.Equals(point1, point2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetHashCode_ShouldReturnSameHashCode_ForEqualPointsWithinTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.1);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.05, 2.05, 3.05);
            int hash1 = comparer.GetHashCode(point1);
            int hash2 = comparer.GetHashCode(point2);
            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void HashsetWithComparer_AddsPoint_WhenPointDoesNotExistWithinTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.1);
            HashSet<Point3D> points = new HashSet<Point3D>(comparer);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.2, 2.2, 3.2);
            points.Add(point1);
            bool added = points.Add(point2);
            Assert.That(added, Is.True);
        }

        [Test]
        public void HashsetWithComparer_DoesNotAddPoint_WhenPointExistsWithinTolerance()
        {
            Point3DEqualityWithinToleranceComparer comparer = new Point3DEqualityWithinToleranceComparer(0.1);
            HashSet<Point3D> points = new HashSet<Point3D>(comparer);
            Point3D point1 = new Point3D(1.0, 2.0, 3.0);
            Point3D point2 = new Point3D(1.05, 2.05, 3.05);
            points.Add(point1);
            bool added = points.Add(point2);
            Assert.That(added, Is.False);
        }
    }
}
