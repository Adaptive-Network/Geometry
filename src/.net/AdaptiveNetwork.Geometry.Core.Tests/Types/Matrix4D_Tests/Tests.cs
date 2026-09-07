using AdaptiveNetwork.Geometry.Core.Types;

using static AdaptiveNetwork.Geometry.Core.Tests.Types.Matrix4D_Tests.TestCaseData;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.Matrix4D_Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Matrix4D"/> struct, verifying its functionality and behavior.
    /// </summary>
    [TestFixture]
    internal sealed class Tests
    {
        [Test]
        public void Constructor_ShouldInitializeElements()
        {
            Matrix4D matrix = new(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(matrix.M11, Is.EqualTo(1));
                Assert.That(matrix.M12, Is.EqualTo(2));
                Assert.That(matrix.M13, Is.EqualTo(3));
                Assert.That(matrix.M14, Is.EqualTo(4));
                Assert.That(matrix.M21, Is.EqualTo(5));
                Assert.That(matrix.M22, Is.EqualTo(6));
                Assert.That(matrix.M23, Is.EqualTo(7));
                Assert.That(matrix.M24, Is.EqualTo(8));
                Assert.That(matrix.M31, Is.EqualTo(9));
                Assert.That(matrix.M32, Is.EqualTo(10));
                Assert.That(matrix.M33, Is.EqualTo(11));
                Assert.That(matrix.M34, Is.EqualTo(12));
                Assert.That(matrix.M41, Is.EqualTo(13));
                Assert.That(matrix.M42, Is.EqualTo(14));
                Assert.That(matrix.M43, Is.EqualTo(15));
                Assert.That(matrix.M44, Is.EqualTo(16));
            }
        }

        [Test]
        public void Identity_ShouldHaveOnesOnDiagonal()
        {
            Matrix4D identity = Matrix4D.Identity;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(identity.M11, Is.EqualTo(1));
                Assert.That(identity.M22, Is.EqualTo(1));
                Assert.That(identity.M33, Is.EqualTo(1));
                Assert.That(identity.M44, Is.EqualTo(1));
                Assert.That(identity.M12, Is.EqualTo(0));
                Assert.That(identity.M21, Is.EqualTo(0));
                Assert.That(identity.M41, Is.EqualTo(0));
                Assert.That(identity.M42, Is.EqualTo(0));
                Assert.That(identity.M43, Is.EqualTo(0));
            }
        }

        [Test]
        public void CreateTranslation_ShouldPlaceVectorInLastRow()
        {
            Matrix4D matrix = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3));
            bool result = matrix.Equals(new Matrix4D(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                1, 2, 3, 1));
            Assert.That(result, Is.True);
        }

        [Test]
        public void CreateRotation_ShouldReturnIdentity_WhenQuaternionIsIdentity()
        {
            QuaternionD identityQuaternion = new(0, 0, 0, 1);
            Matrix4D matrix = Matrix4D.CreateRotation(identityQuaternion);
            bool result = matrix.EquatableWithinTolerance(Matrix4D.Identity, 1e-12);
            Assert.That(result, Is.True);
        }

        [Test]
        public void CreateScale_ShouldPlaceVectorOnDiagonal()
        {
            Matrix4D matrix = Matrix4D.CreateScale(new Vector3D(2, 3, 4));
            bool result = matrix.Equals(new Matrix4D(
                2, 0, 0, 0,
                0, 3, 0, 0,
                0, 0, 4, 0,
                0, 0, 0, 1));
            Assert.That(result, Is.True);
        }

        [Test]
        public void Transpose_ShouldSwapRowsAndColumns()
        {
            Matrix4D matrix = new(
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16);
            Matrix4D expected = new(
                1, 5, 9, 13,
                2, 6, 10, 14,
                3, 7, 11, 15,
                4, 8, 12, 16);
            Matrix4D result = matrix.Transpose();
            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void TryInvert_ShouldReturnTrueAndInverse_WhenMatrixIsInvertible()
        {
            Matrix4D matrix = Matrix4D.CreateScale(new Vector3D(2, 4, 5));
            bool succeeded = matrix.TryInvert(out Matrix4D inverse);
            Matrix4D expected = Matrix4D.CreateScale(new Vector3D(0.5, 0.25, 0.2));
            using (Assert.EnterMultipleScope())
            {
                Assert.That(succeeded, Is.True);
                Assert.That(inverse.EquatableWithinTolerance(expected, 1e-9), Is.True);
            }
        }

        [Test]
        public void TryInvert_ShouldReturnFalseAndIdentity_WhenMatrixIsSingular()
        {
            Matrix4D matrix = Matrix4D.CreateScale(new Vector3D(0, 1, 1));
            bool succeeded = matrix.TryInvert(out Matrix4D result);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(succeeded, Is.False);
                Assert.That(result.Equals(Matrix4D.Identity), Is.True);
            }
        }

        [Test]
        public void MultiplicationOperator_WithIdentity_ShouldReturnOriginalMatrix()
        {
            Matrix4D matrix = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3));
            Matrix4D result = matrix * Matrix4D.Identity;
            Assert.That(result.Equals(matrix), Is.True);
        }

        [Test]
        public void MultiplicationOperator_ShouldCombineTranslations()
        {
            Matrix4D a = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3));
            Matrix4D b = Matrix4D.CreateTranslation(new Vector3D(4, 5, 6));
            Matrix4D result = a * b;
            Matrix4D expected = Matrix4D.CreateTranslation(new Vector3D(5, 7, 9));
            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void Equals_Object_ShouldReturnTrue_WhenMatricesAreEqual()
        {
            Matrix4D a = Matrix4D.Identity;
            object b = Matrix4D.Identity;
            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void Equals_Object_ShouldReturnFalse_WhenObjectIsNotMatrix4D()
        {
            Matrix4D a = Matrix4D.Identity;
            Assert.That(a.Equals(new object()), Is.False);
        }

        [Test]
        public void Equals_Matrix4D_ShouldReturnFalse_WhenMatricesDiffer()
        {
            Matrix4D a = Matrix4D.Identity;
            Matrix4D b = Matrix4D.CreateTranslation(new Vector3D(1, 0, 0));
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenMatricesAreWithinTolerance()
        {
            Matrix4D a = Matrix4D.Identity;
            Matrix4D b = new(
                1.00001, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
            bool result = a.EquatableWithinTolerance(b, 0.0001);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenMatricesAreOutsideTolerance()
        {
            Matrix4D a = Matrix4D.Identity;
            Matrix4D b = new(
                1.0001, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
            bool result = a.EquatableWithinTolerance(b, 0.00001);
            Assert.That(result, Is.False);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnTrue_WhenMatricesAreExactlyTheSame_WithZeroTolerance()
        {
            Matrix4D a = Matrix4D.Identity;
            Matrix4D b = Matrix4D.Identity;
            bool result = a.EquatableWithinTolerance(b);
            Assert.That(result, Is.True);
        }

        [Test]
        public void EquatableWithinTolerance_ShouldReturnFalse_WhenMatricesAreDifferent_WithZeroTolerance()
        {
            Matrix4D a = Matrix4D.Identity;
            Matrix4D b = Matrix4D.CreateTranslation(new Vector3D(0.00001, 0, 0));
            bool result = a.EquatableWithinTolerance(b);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetHashCode_ShouldReturnSameValue_ForEqualMatrices()
        {
            Matrix4D a = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3));
            Matrix4D b = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3));
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void GetHashCode_ShouldReturnDifferentValue_ForDifferentMatrices()
        {
            Matrix4D a = Matrix4D.Identity;
            Matrix4D b = Matrix4D.CreateTranslation(new Vector3D(1, 2, 3));
            Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
        }

        [Test]
        [TestCaseSource(typeof(DeterminantTestCaseData), nameof(DeterminantTestCaseData.Data))]
        public void Determinant_ShouldReturnExpectedValue(DeterminantData data)
        {
            double result = data.Matrix.Determinant();
            Assert.That(result, Is.EqualTo(data.Expected));
        }
    }
}
