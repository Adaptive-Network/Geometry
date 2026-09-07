using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.Vector3D_Tests
{
    internal sealed class TestCaseData
    {
        internal sealed class CrossData
        {
            public Vector3D A { get; init; }
            public Vector3D B { get; init; }
            public Vector3D Expected { get; init; }
        }

        internal sealed class CrossTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<CrossData> Data
            {
                get
                {
                    yield return new CrossData { A = new Vector3D(1, 0, 0), B = new Vector3D(0, 1, 0), Expected = new Vector3D(0, 0, 1) };
                    yield return new CrossData { A = new Vector3D(0, 1, 0), B = new Vector3D(1, 0, 0), Expected = new Vector3D(0, 0, -1) };
                    yield return new CrossData { A = new Vector3D(0, 0, 0), B = new Vector3D(1, 2, 3), Expected = new Vector3D(0, 0, 0) };
                    yield return new CrossData { A = new Vector3D(1, 2, 3), B = new Vector3D(1, 2, 3), Expected = new Vector3D(0, 0, 0) };
                }
            }
        }

        internal sealed class DotData
        {
            public Vector3D A { get; init; }
            public Vector3D B { get; init; }
            public double Expected { get; init; }
        }

        internal sealed class DotTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<DotData> Data
            {
                get
                {
                    yield return new DotData { A = new Vector3D(1, 0, 0), B = new Vector3D(0, 1, 0), Expected = 0 };
                    yield return new DotData { A = new Vector3D(1, 2, 3), B = new Vector3D(4, 5, 6), Expected = 32 };
                    yield return new DotData { A = new Vector3D(-1, -2, -3), B = new Vector3D(4, 5, 6), Expected = -32 };
                    yield return new DotData { A = new Vector3D(0, 0, 0), B = new Vector3D(4, 5, 6), Expected = 0 };
                }
            }
        }
    }
}
