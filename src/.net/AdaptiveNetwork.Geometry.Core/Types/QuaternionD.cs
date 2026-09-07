namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a quaternion with double precision values, commonly used for representing rotations in 3D space.
    /// </summary>
    public readonly struct QuaternionD
    {
        /// <summary>
        /// Gets the X component of the quaternion.
        /// </summary>
        public readonly double X = 0;

        /// <summary>
        /// Gets the Y component of the quaternion.
        /// </summary>
        public readonly double Y = 0;

        /// <summary>
        /// Gets the Z component of the quaternion.
        /// </summary>
        public readonly double Z = 0;

        /// <summary>
        /// Gets the W component of the quaternion.
        /// </summary>
        public readonly double W = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionD"/> struct with default values (0, 0, 0, 1), representing the identity quaternion.
        /// </summary>
        public QuaternionD() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionD"/> struct with the specified X, Y, Z, and W components.
        /// </summary>
        /// <param name="x">The X component of the quaternion.</param>
        /// <param name="y">The Y component of the quaternion.</param>
        /// <param name="z">The Z component of the quaternion.</param>
        /// <param name="w">The W component of the quaternion.</param>
        public QuaternionD(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets the identity quaternion, which represents no rotation. The identity quaternion has components (0, 0, 0, 1).
        /// </summary>
        public static readonly QuaternionD Identity = new(0, 0, 0, 1);

        /// <summary>
        /// Creates a quaternion representing a rotation around a specified axis by a given angle in radians.
        /// </summary>
        /// <param name="axis">The axis around which to rotate.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <returns>A quaternion representing the rotation.</returns>
        public static QuaternionD CreateFromAxisAngle(Vector3D axis, double angle)
        {
            Vector3D a = axis.Normalize();
            double halfAngle = angle / 2.0;
            double s = Math.Sin(halfAngle);
            return new QuaternionD(a.X * s, a.Y * s, a.Z * s, Math.Cos(halfAngle));
        }

        /// <summary>
        /// Creates a quaternion representing a rotation around a specified axis by a given angle in degrees.
        /// </summary>
        /// <param name="axis">The axis around which to rotate.</param>
        /// <param name="degrees">The angle of rotation in degrees.</param>
        /// <returns>A quaternion representing the rotation.</returns>
        public static QuaternionD CreateAxisFromAngleDegrees(Vector3D axis, double degrees)
        {
            double radians = degrees * (Math.PI / 180.0);
            return CreateFromAxisAngle(axis, radians);
        }

        /// <summary>
        /// Defines the multiplication operator for two quaternions, allowing for quaternion multiplication. The result is a new quaternion that represents the combined rotation of the two input quaternions.
        /// </summary>
        /// <param name="q1">The first quaternion.</param>
        /// <param name="q2">The second quaternion.</param>
        /// <returns>A new quaternion representing the product of the two input quaternions.</returns>
        public static QuaternionD operator *(QuaternionD q1, QuaternionD q2)
        {
            return new QuaternionD(
                q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y,
                q1.W * q2.Y - q1.X * q2.Z + q1.Y * q2.W + q1.Z * q2.X,
                q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W,
                q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z
            );
        }

        /// <summary>
        /// Calculates the squared length (magnitude) of the quaternion.
        /// </summary>
        /// <returns>The squared length of the quaternion.</returns>
        internal double LengthSquared() => X * X + Y * Y + Z * Z + W * W;

        /// <summary>
        /// Calculates the length (magnitude) of the quaternion.
        /// </summary>
        /// <returns>The length of the quaternion.</returns>
        public double Length() => Math.Sqrt(LengthSquared());

        /// <summary>
        /// Normalizes the quaternion to have a length of 1, returning a new normalized quaternion. Throws an InvalidOperationException if the quaternion has zero length.
        /// </summary>
        /// <returns>A new normalized quaternion.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the quaternion has zero length.</exception>
        public QuaternionD Normalize()
        {
            double length = Length();
            if (length <= 0)
                throw new InvalidOperationException("Cannot normalize a quaternion with zero length.");
            return new QuaternionD(X / length, Y / length, Z / length, W / length);
        }

        /// <summary>
        /// Calculates the conjugate of the quaternion, which is obtained by negating the vector part (X, Y, Z) while keeping the scalar part (W) unchanged.
        /// </summary>
        /// <returns>A new quaternion representing the conjugate of the current quaternion.</returns>
        public QuaternionD Conjugate() => new(-X, -Y, -Z, W);

        /// <summary>
        /// Calculates the inverse of the quaternion.
        /// </summary>
        /// <returns>A new quaternion representing the inverse of the current quaternion.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the quaternion has zero length.</exception>
        public QuaternionD Inverse()
        {
            double lengthSquared = LengthSquared();
            if (lengthSquared <= 0)
                throw new InvalidOperationException("Cannot invert a quaternion with zero length.");
            QuaternionD conjugate = Conjugate();
            return new QuaternionD(conjugate.X / lengthSquared, conjugate.Y / lengthSquared, conjugate.Z / lengthSquared, conjugate.W / lengthSquared);
        }

        /// <summary>
        /// Calculates the dot product of the current quaternion with another quaternion.
        /// </summary>
        /// <param name="other">The other quaternion to calculate the dot product with.</param>
        /// <returns>The dot product of the current quaternion and the other quaternion.</returns>
        public double Dot(QuaternionD other) => X * other.X + Y * other.Y + Z * other.Z + W * other.W;
    }
}
