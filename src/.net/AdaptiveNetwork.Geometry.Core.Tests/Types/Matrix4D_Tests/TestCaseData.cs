using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.Matrix4D_Tests
{
    internal sealed class TestCaseData
    {
        internal sealed class DeterminantData
        {
            public Matrix4D Matrix { get; init; }
            public double Expected { get; init; }
        }

        internal sealed class DeterminantTestCaseData : NUnit.Framework.TestCaseData
        {
            public static IEnumerable<DeterminantData> Data
            {
                get
                {
                    yield return new DeterminantData { Matrix = Matrix4D.Identity, Expected = 1 };
                    yield return new DeterminantData { Matrix = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3)), Expected = 1 };
                    yield return new DeterminantData { Matrix = Matrix4D.CreateScale(new Vector3D(2, 3, 4)), Expected = 24 };
                    yield return new DeterminantData { Matrix = new Matrix4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), Expected = 0 };
                }
            }
        }
    }
}
