using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.LineSegment3D_Tests
{
    /// <summary>
    /// Contains test case data for unit tests related to the <see cref="LineSegment3D"/> struct, including test cases for calculating the length of a line segment and translating a line segment by a vector in 3D space.
    /// </summary>
    internal sealed class TestCaseData
    {
        /// <summary>
        /// Represents a test case for calculating the length of a line segment in 3D space, including the start and end points of the segment, as well as the expected length and squared length.
        /// </summary>
        internal sealed class LengthData
        {
            /// <summary>
            /// Gets or initializes the starting point of the line segment in 3D space.
            /// </summary>
            public Point3D Start { get; init; }

            /// <summary>
            /// Gets or initializes the ending point of the line segment in 3D space.
            /// </summary>
            public Point3D End { get; init; }

            /// <summary>
            /// Gets or initializes the expected squared length of the line segment, calculated as the sum of the squares of the differences in each coordinate between the start and end points.
            /// </summary>
            public double ExpectedLengthSquared { get; init; }

            /// <summary>
            /// Gets or initializes the expected length of the line segment, calculated as the square root of the expected squared length.
            /// </summary>
            public double ExpectedLength { get; init; }
        }

        /// <summary>
        /// Represents a collection of test cases for calculating the length of a line segment in 3D space, providing the start and end points of the segment, as well as the expected length and squared length.
        /// </summary>
        internal sealed class LengthTestCaseData : NUnit.Framework.TestCaseData
        {
            /// <summary>
            /// Gets an enumerable collection of test cases for calculating the length of a line segment in 3D space, including the start and end points of the segment, as well as the expected length and squared length.
            /// </summary>
            public static IEnumerable<LengthData> Data
            {
                get
                {
                    yield return new LengthData { Start = new Point3D(0, 0, 0), End = new Point3D(3, 4, 0), ExpectedLengthSquared = 25, ExpectedLength = 5 };
                    yield return new LengthData { Start = new Point3D(1, 2, 3), End = new Point3D(1, 2, 3), ExpectedLengthSquared = 0, ExpectedLength = 0 };
                    yield return new LengthData { Start = new Point3D(-1, -1, -1), End = new Point3D(1, 1, 1), ExpectedLengthSquared = 12, ExpectedLength = Math.Sqrt(12) };
                    yield return new LengthData { Start = new Point3D(0, 0, 0), End = new Point3D(1, 0, 0), ExpectedLengthSquared = 1, ExpectedLength = 1 };
                }
            }
        }

        /// <summary>
        /// Represents a test case for translating a line segment by a vector in 3D space, including the original line segment, the translation vector, and the expected start and end points of the translated segment.
        /// </summary>
        internal sealed class VectorTranslationData
        {
            /// <summary>
            /// Gets or initializes the original line segment in 3D space.
            /// </summary>
            public LineSegment3D Segment { get; init; } = null!;

            /// <summary>
            /// Gets or initializes the translation vector to be applied to the line segment in 3D space.
            /// </summary>
            public Vector3D Vector { get; init; }

            /// <summary>
            /// Gets or initializes the expected starting point of the translated line segment in 3D space after applying the translation vector.
            /// </summary>
            public Point3D ExpectedStart { get; init; }

            /// <summary>
            /// Gets or initializes the expected ending point of the translated line segment in 3D space after applying the translation vector.
            /// </summary>
            public Point3D ExpectedEnd { get; init; }
        }

        /// <summary>
        /// Represents a collection of test cases for translating a line segment by a vector in 3D space, providing the original line segment, the translation vector, and the expected start and end points of the translated segment.
        /// </summary>
        internal sealed class VectorTranslateTestCaseData : NUnit.Framework.TestCaseData
        {
            /// <summary>
            /// Gets an enumerable collection of test cases for translating a line segment by a vector in 3D space, including the original line segment, the translation vector, and the expected start and end points of the translated segment.
            /// </summary>
            public static IEnumerable<VectorTranslationData> Data
            {
                get
                {
                    yield return new VectorTranslationData { Segment = new LineSegment3D(new Point3D(0, 0, 0), new Point3D(1, 1, 1)), Vector = new Vector3D(1, 2, 3), ExpectedStart = new Point3D(1, 2, 3), ExpectedEnd = new Point3D(2, 3, 4) };
                    yield return new VectorTranslationData { Segment = new LineSegment3D(new Point3D(1, 1, 1), new Point3D(2, 2, 2)), Vector = new Vector3D(-1, -1, -1), ExpectedStart = new Point3D(0, 0, 0), ExpectedEnd = new Point3D(1, 1, 1) };
                    yield return new VectorTranslationData { Segment = new LineSegment3D(new Point3D(0, 0, 0), new Point3D(1, 1, 1)), Vector = new Vector3D(0, 0, 0), ExpectedStart = new Point3D(0, 0, 0), ExpectedEnd = new Point3D(1, 1, 1) };
                }
            }
        }

        /// <summary>
        /// Represents a test case for rotating a line segment around an axis through a specified position, including the original segment, the position, axis, and angle of rotation, and the expected start and end points of the rotated segment.
        /// </summary>
        internal sealed class RotationData
        {
            public LineSegment3D Segment { get; init; } = null!;
            public Point3D Position { get; init; }
            public Vector3D Axis { get; init; }
            public double Radians { get; init; }
            public Point3D ExpectedStart { get; init; }
            public Point3D ExpectedEnd { get; init; }
        }

        internal sealed class RotationTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<RotationData> Data
            {
                get
                {
                    // Zero/identity: no rotation leaves the segment unchanged.
                    yield return new RotationData { Segment = new LineSegment3D(new Point3D(1, 0, 0), new Point3D(2, 0, 0)), Position = Point3D.Origin, Axis = new Vector3D(0, 0, 1), Radians = 0, ExpectedStart = new Point3D(1, 0, 0), ExpectedEnd = new Point3D(2, 0, 0) };
                    // Positive: 90 degrees around the Z axis through the origin.
                    yield return new RotationData { Segment = new LineSegment3D(new Point3D(1, 0, 0), new Point3D(2, 0, 0)), Position = Point3D.Origin, Axis = new Vector3D(0, 0, 1), Radians = Math.PI / 2, ExpectedStart = new Point3D(0, 1, 0), ExpectedEnd = new Point3D(0, 2, 0) };
                    // Negative: -90 degrees around the Z axis through the origin.
                    yield return new RotationData { Segment = new LineSegment3D(new Point3D(1, 0, 0), new Point3D(2, 0, 0)), Position = Point3D.Origin, Axis = new Vector3D(0, 0, 1), Radians = -Math.PI / 2, ExpectedStart = new Point3D(0, -1, 0), ExpectedEnd = new Point3D(0, -2, 0) };
                    // Boundary: a full 2*pi rotation returns the segment to its original position.
                    yield return new RotationData { Segment = new LineSegment3D(new Point3D(1, 0, 0), new Point3D(2, 0, 0)), Position = Point3D.Origin, Axis = new Vector3D(0, 0, 1), Radians = 2 * Math.PI, ExpectedStart = new Point3D(1, 0, 0), ExpectedEnd = new Point3D(2, 0, 0) };
                    // Position other than the origin: rotate 90 degrees around an axis through Start itself, so Start stays fixed.
                    yield return new RotationData { Segment = new LineSegment3D(new Point3D(1, 0, 0), new Point3D(2, 0, 0)), Position = new Point3D(1, 0, 0), Axis = new Vector3D(0, 0, 1), Radians = Math.PI / 2, ExpectedStart = new Point3D(1, 0, 0), ExpectedEnd = new Point3D(1, 1, 0) };
                }
            }
        }
    }
}
