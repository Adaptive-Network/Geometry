using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.QuaternionD_Tests
{
    /// <summary>
    /// Contains test case data for unit tests related to the <see cref="QuaternionD"/> struct, including test cases for the dot product of two quaternions.
    /// </summary>
    internal sealed class TestCaseData
    {
        /// <summary>
        /// Represents a test case for the dot product of two quaternions, including the two quaternions and the expected result of their dot product.
        /// </summary>
        internal sealed class DotData
        {
            /// <summary>
            /// Gets or initializes the first quaternion for the dot product operation.
            /// </summary>
            public QuaternionD A { get; init; }

            /// <summary>
            /// Gets or initializes the second quaternion for the dot product operation.
            /// </summary>
            public QuaternionD B { get; init; }

            /// <summary>
            /// Gets or initializes the expected result of the dot product operation between the two quaternions.
            /// </summary>
            public double Expected { get; init; }
        }

        /// <summary>
        /// Represents a collection of test cases for the dot product of two quaternions, providing the two quaternions and the expected result of their dot product.
        /// </summary>
        internal sealed class DotTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<DotData> Data
            {
                get
                {
                    yield return new DotData { A = new QuaternionD(1, 0, 0, 0), B = new QuaternionD(0, 1, 0, 0), Expected = 0 };
                    yield return new DotData { A = new QuaternionD(1, 2, 3, 4), B = new QuaternionD(5, 6, 7, 8), Expected = 70 };
                    yield return new DotData { A = new QuaternionD(-1, -2, -3, -4), B = new QuaternionD(5, 6, 7, 8), Expected = -70 };
                    yield return new DotData { A = new QuaternionD(0, 0, 0, 0), B = new QuaternionD(5, 6, 7, 8), Expected = 0 };
                }
            }
        }
    }
}
