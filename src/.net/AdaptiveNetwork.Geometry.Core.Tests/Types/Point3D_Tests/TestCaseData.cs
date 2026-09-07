using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.Point3D_Tests
{
    internal sealed class TestCaseData
    {
        /// <summary>
        /// Represents a test case for translating a point by a vector in 3D space, including the original point, the translation vector, and the expected result after translation.
        /// </summary>
        internal sealed class VectorTranslationData
        {
            /// <summary>
            /// Gets or initializes the original point in 3D space before translation.
            /// </summary>
            public Core.Types.Point3D Point { get; init; }

            /// <summary>
            /// Gets or initializes the vector by which to translate the original point in 3D space.
            /// </summary>
            public Core.Types.Vector3D Vector { get; init; }

            /// <summary>
            /// Gets or initializes the expected result after translating the original point by the specified vector in 3D space.
            /// </summary>
            public Core.Types.Point3D Expected { get; init; }
        }

        /// <summary>
        /// Represents a collection of test cases for translating a point by a vector in 3D space, providing the original point, the translation vector, and the expected result after translation.
        /// </summary>
        internal sealed class VectorTranslateTestCaseData : NUnit.Framework.TestCaseData
        {
            /// <summary>
            /// Gets an enumerable collection of test cases for translating a point by a vector in 3D space, including the original point, the translation vector, and the expected result after translation.
            /// </summary>
            public static IEnumerable<VectorTranslationData> Data
            {
                get
                {
                    yield return new VectorTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Vector = new Core.Types.Vector3D(1, 2, 3), Expected = new Core.Types.Point3D(1, 2, 3) };
                    yield return new VectorTranslationData { Point = new Core.Types.Point3D(1, 1, 1), Vector = new Core.Types.Vector3D(-1, -1, -1), Expected = new Core.Types.Point3D(0, 0, 0) };
                    yield return new VectorTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Vector = new Core.Types.Vector3D(0, 0, 0), Expected = new Core.Types.Point3D(0, 0, 0) };
                    yield return new VectorTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Vector = new Core.Types.Vector3D(1, 0, 0), Expected = new Core.Types.Point3D(1, 0, 0) };
                    yield return new VectorTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Vector = new Core.Types.Vector3D(0, 1, 0), Expected = new Core.Types.Point3D(0, 1, 0) };
                    yield return new VectorTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Vector = new Core.Types.Vector3D(0, 0, 1), Expected = new Core.Types.Point3D(0, 0, 1) };
                }
            }
        }

        /// <summary>
        /// Represents a test case for translating a point by individual coordinate values (Dx, Dy, Dz) in 3D space, including the original point, the translation values, and the expected result after translation.
        /// </summary>
        internal sealed class DimensionTranslationData
        {
            /// <summary>
            /// Gets or initializes the original point in 3D space before translation.
            /// </summary>
            public Core.Types.Point3D Point { get; init; }

            /// <summary>
            /// Gets or initializes the translation value along the X axis in 3D space.
            /// </summary>
            public double Dx { get; init; }

            /// <summary>
            /// Gets or initializes the translation value along the Y axis in 3D space.
            /// </summary>
            public double Dy { get; init; }

            /// <summary>
            /// Gets or initializes the translation value along the Z axis in 3D space.
            /// </summary>
            public double Dz { get; init; }

            /// <summary>
            /// Gets or initializes the expected result after translating the original point by the specified coordinate values in 3D space.
            /// </summary>
            public Core.Types.Point3D Expected { get; init; }
        }

        /// <summary>
        /// Represents a collection of test cases for translating a point by individual coordinate values (Dx, Dy, Dz) in 3D space, providing the original point, the translation values, and the expected result after translation.
        /// </summary>
        internal sealed class DimensionTranslationTestData : NUnit.Framework.TestCaseData
        {
            /// <summary>
            /// Gets an enumerable collection of test cases for translating a point by individual coordinate values (Dx, Dy, Dz) in 3D space, including the original point, the translation values, and the expected result after translation.
            /// </summary>
            public static IEnumerable<DimensionTranslationData> Data
            {
                get
                {
                    yield return new DimensionTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Dx = 1, Dy = 2, Dz = 3, Expected = new Core.Types.Point3D(1, 2, 3) };
                    yield return new DimensionTranslationData { Point = new Core.Types.Point3D(1, 1, 1), Dx = -1, Dy = -1, Dz = -1, Expected = new Core.Types.Point3D(0, 0, 0) };
                    yield return new DimensionTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Dx = 0, Dy = 0, Dz = 0, Expected = new Core.Types.Point3D(0, 0, 0) };
                    yield return new DimensionTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Dx = 1, Dy = 0, Dz = 0, Expected = new Core.Types.Point3D(1, 0, 0) };
                    yield return new DimensionTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Dx = 0, Dy = 1, Dz = 0, Expected = new Core.Types.Point3D(0, 1, 0) };
                    yield return new DimensionTranslationData { Point = new Core.Types.Point3D(0, 0, 0), Dx = 0, Dy = 0, Dz = 1, Expected = new Core.Types.Point3D(0, 0, 1) };
                }
            }
        }

        internal sealed class DistanceSquaredTestCase
        {
            public Point3D A { get; init; }
            public Point3D B { get; init; }
            public double ExpectedDistanceSquared { get; init; }
        }

        internal sealed class DistanceSquaredTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<DistanceSquaredTestCase> Data
            {
                get
                {
                    yield return new DistanceSquaredTestCase { A = new Point3D(0, 0, 0), B = new Point3D(1, 1, 1), ExpectedDistanceSquared = 3 };
                    yield return new DistanceSquaredTestCase { A = new Point3D(1, 2, 3), B = new Point3D(4, 5, 6), ExpectedDistanceSquared = 27 };
                    yield return new DistanceSquaredTestCase { A = new Point3D(-1, -2, -3), B = new Point3D(-4, -5, -6), ExpectedDistanceSquared = 27 };
                    yield return new DistanceSquaredTestCase { A = new Point3D(0, 0, 0), B = new Point3D(0, 0, 0), ExpectedDistanceSquared = 0 };
                }
            }
        }

        internal sealed class DistanceTestCase
        {
            public Point3D A { get; init; }
            public Point3D B { get; init; }
            public double ExpectedDistance { get; init; }
        }

        internal sealed class DistanceTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<DistanceTestCase> Data
            {
                get
                {
                    yield return new DistanceTestCase { A = new Point3D(0, 0, 0), B = new Point3D(1, 1, 1), ExpectedDistance = Math.Sqrt(3) };
                    yield return new DistanceTestCase { A = new Point3D(1, 2, 3), B = new Point3D(4, 5, 6), ExpectedDistance = Math.Sqrt(27) };
                    yield return new DistanceTestCase { A = new Point3D(-1, -2, -3), B = new Point3D(-4, -5, -6), ExpectedDistance = Math.Sqrt(27) };
                    yield return new DistanceTestCase { A = new Point3D(0, 0, 0), B = new Point3D(0, 0, 0), ExpectedDistance = 0 };
                }
            }
        }
    }
}
