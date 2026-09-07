using AdaptiveNetwork.Geometry.Core.Types;

using static AdaptiveNetwork.Geometry.Core.Tests.Types.QuaternionD_Tests.TestCaseData;

namespace AdaptiveNetwork.Geometry.Core.Tests.Types.QuaternionD_Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="QuaternionD"/> struct, verifying its functionality and behavior.
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
            double w = 4.0;
            QuaternionD quaternion = new(x, y, z, w);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(quaternion.X, Is.EqualTo(x));
                Assert.That(quaternion.Y, Is.EqualTo(y));
                Assert.That(quaternion.Z, Is.EqualTo(z));
                Assert.That(quaternion.W, Is.EqualTo(w));
            }
        }

        [Test]
        public void DefaultConstructor_ShouldInitializeToIdentity()
        {
            QuaternionD quaternion = new();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(quaternion.X, Is.EqualTo(0));
                Assert.That(quaternion.Y, Is.EqualTo(0));
                Assert.That(quaternion.Z, Is.EqualTo(0));
                Assert.That(quaternion.W, Is.EqualTo(1));
            }
        }

        [Test]
        public void Identity_ShouldRepresentNoRotation()
        {
            QuaternionD identity = QuaternionD.Identity;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(identity.X, Is.EqualTo(0));
                Assert.That(identity.Y, Is.EqualTo(0));
                Assert.That(identity.Z, Is.EqualTo(0));
                Assert.That(identity.W, Is.EqualTo(1));
            }
        }

        [Test]
        public void CreateFromAxisAngle_ShouldReturnExpectedQuaternion()
        {
            Vector3D axis = new(0, 0, 1);
            double angle = Math.PI / 2;
            QuaternionD result = QuaternionD.CreateFromAxisAngle(axis, angle);
            double expectedHalf = Math.Sqrt(2) / 2;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(0).Within(1e-9));
                Assert.That(result.Y, Is.EqualTo(0).Within(1e-9));
                Assert.That(result.Z, Is.EqualTo(expectedHalf).Within(1e-9));
                Assert.That(result.W, Is.EqualTo(expectedHalf).Within(1e-9));
            }
        }

        [Test]
        public void CreateFromAxisAngle_ShouldReturnIdentity_WhenAngleIsZero()
        {
            Vector3D axis = new(1, 0, 0);
            QuaternionD result = QuaternionD.CreateFromAxisAngle(axis, 0);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(0).Within(1e-9));
                Assert.That(result.Y, Is.EqualTo(0).Within(1e-9));
                Assert.That(result.Z, Is.EqualTo(0).Within(1e-9));
                Assert.That(result.W, Is.EqualTo(1).Within(1e-9));
            }
        }

        [Test]
        public void CreateAxisFromAngleDegrees_ShouldMatchRadianEquivalent()
        {
            Vector3D axis = new(0, 0, 1);
            QuaternionD expected = QuaternionD.CreateFromAxisAngle(axis, Math.PI / 2);
            QuaternionD result = QuaternionD.CreateAxisFromAngleDegrees(axis, 90);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(expected.X).Within(1e-9));
                Assert.That(result.Y, Is.EqualTo(expected.Y).Within(1e-9));
                Assert.That(result.Z, Is.EqualTo(expected.Z).Within(1e-9));
                Assert.That(result.W, Is.EqualTo(expected.W).Within(1e-9));
            }
        }

        [Test]
        public void MultiplicationOperator_ShouldCombineRotations()
        {
            QuaternionD i = new(1, 0, 0, 0);
            QuaternionD j = new(0, 1, 0, 0);
            QuaternionD result = i * j;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(0));
                Assert.That(result.Y, Is.EqualTo(0));
                Assert.That(result.Z, Is.EqualTo(1));
                Assert.That(result.W, Is.EqualTo(0));
            }
        }

        [Test]
        public void MultiplicationOperator_WithIdentity_ShouldReturnOriginalQuaternion()
        {
            QuaternionD quaternion = new(1, 2, 3, 4);
            QuaternionD result = quaternion * QuaternionD.Identity;
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(quaternion.X));
                Assert.That(result.Y, Is.EqualTo(quaternion.Y));
                Assert.That(result.Z, Is.EqualTo(quaternion.Z));
                Assert.That(result.W, Is.EqualTo(quaternion.W));
            }
        }

        [Test]
        public void LengthSquared_ShouldReturnSumOfSquares()
        {
            QuaternionD quaternion = new(1, 2, 3, 4);
            double result = quaternion.LengthSquared();
            Assert.That(result, Is.EqualTo(30));
        }

        [Test]
        public void Length_ShouldReturnMagnitude()
        {
            QuaternionD quaternion = new(0, 3, 4, 0);
            double result = quaternion.Length();
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Length_ShouldReturnZero_WhenQuaternionIsZero()
        {
            QuaternionD quaternion = new(0, 0, 0, 0);
            double result = quaternion.Length();
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Normalize_ShouldReturnUnitQuaternion()
        {
            QuaternionD quaternion = new(0, 3, 4, 0);
            QuaternionD result = quaternion.Normalize();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(0));
                Assert.That(result.Y, Is.EqualTo(0.6));
                Assert.That(result.Z, Is.EqualTo(0.8));
                Assert.That(result.W, Is.EqualTo(0));
            }
        }

        [Test]
        public void Normalize_ShouldThrowInvalidOperationException_WhenQuaternionIsZeroLength()
        {
            QuaternionD quaternion = new(0, 0, 0, 0);
            Assert.Throws<InvalidOperationException>(() => quaternion.Normalize());
        }

        [Test]
        public void Conjugate_ShouldNegateVectorPart()
        {
            QuaternionD quaternion = new(1, 2, 3, 4);
            QuaternionD result = quaternion.Conjugate();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(-1));
                Assert.That(result.Y, Is.EqualTo(-2));
                Assert.That(result.Z, Is.EqualTo(-3));
                Assert.That(result.W, Is.EqualTo(4));
            }
        }

        [Test]
        public void Inverse_ShouldReturnExpectedQuaternion()
        {
            QuaternionD quaternion = new(2, 0, 0, 0);
            QuaternionD result = quaternion.Inverse();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(-0.5));
                Assert.That(result.Y, Is.EqualTo(0));
                Assert.That(result.Z, Is.EqualTo(0));
                Assert.That(result.W, Is.EqualTo(0));
            }
        }

        [Test]
        public void Inverse_OfIdentity_ShouldReturnIdentity()
        {
            QuaternionD result = QuaternionD.Identity.Inverse();
            using (Assert.EnterMultipleScope())
            {
                Assert.That(result.X, Is.EqualTo(0));
                Assert.That(result.Y, Is.EqualTo(0));
                Assert.That(result.Z, Is.EqualTo(0));
                Assert.That(result.W, Is.EqualTo(1));
            }
        }

        [Test]
        public void Inverse_ShouldThrowInvalidOperationException_WhenQuaternionIsZeroLength()
        {
            QuaternionD quaternion = new(0, 0, 0, 0);
            Assert.Throws<InvalidOperationException>(() => quaternion.Inverse());
        }

        [Test]
        [TestCaseSource(typeof(DotTestCaseData), nameof(DotTestCaseData.Data))]
        public void Dot_ShouldReturnScalarProduct(DotData data)
        {
            double result = data.A.Dot(data.B);
            Assert.That(result, Is.EqualTo(data.Expected));
        }
    }
}
