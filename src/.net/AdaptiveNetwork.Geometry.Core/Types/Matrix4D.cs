using AdaptiveNetwork.Geometry.Core.Qualities;

using System.Diagnostics.CodeAnalysis;

namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a 4x4 matrix with double precision values, commonly used for transformations in 3D space.
    /// </summary>
    public readonly struct Matrix4D : IEquatable<Matrix4D>, IEquatableWithinTolerance<Matrix4D>
    {
        /// <summary>
        /// Gets the elements of the matrix in row-major order.
        /// </summary>
        public readonly double M11, M12, M13, M14;

        /// <summary>
        /// Gets the elements of the matrix in row-major order.
        /// </summary>
        public readonly double M21, M22, M23, M24;

        /// <summary>
        /// Gets the elements of the matrix in row-major order.
        /// </summary>
        public readonly double M31, M32, M33, M34;

        /// <summary>
        /// Gets the elements of the matrix in row-major order.
        /// </summary>
        public readonly double M41, M42, M43, M44;

        public Matrix4D(
            double m11, double m12, double m13, double m14,
            double m21, double m22, double m23, double m24,
            double m31, double m32, double m33, double m34,
            double m41, double m42, double m43, double m44)
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
            M41 = m41; M42 = m42; M43 = m43; M44 = m44;
        }

        public static readonly Matrix4D Identity = new(1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        /// <summary>
        /// Creates a translation matrix that translates points by the specified vector.
        /// </summary>
        /// <param name="vector">The vector by which to translate points.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix4D CreateTranslation(Vector3D vector) => new(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            vector.X, vector.Y, vector.Z, 1);

        /// <summary>
        /// Creates a rotation matrix from the specified quaternion.
        /// </summary>
        /// <param name="quarternion">The quaternion representing the rotation.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix4D CreateRotation(QuaternionD quarternion)
        {
            double x2 = quarternion.X + quarternion.X, y2 = quarternion.Y + quarternion.Y, z2 = quarternion.Z + quarternion.Z;
            double xx = quarternion.X * x2, xy = quarternion.X * y2, xz = quarternion.X * z2;
            double yy = quarternion.Y * y2, yz = quarternion.Y * z2, zz = quarternion.Z * z2;
            double wx = quarternion.W * x2, wy = quarternion.W * y2, wz = quarternion.W * z2;

            return new Matrix4D(
                1 - (yy + zz), xy + wz, xz - wy, 0,
                xy - wz, 1 - (xx + zz), yz + wx, 0,
                xz + wy, yz - wx, 1 - (xx + yy), 0,
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a scaling matrix that scales points by the specified vector.
        /// </summary>
        /// <param name="vector">The vector by which to scale points.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix4D CreateScale(Vector3D vector) => new Matrix4D(
            vector.X, 0, 0, 0,
            0, vector.Y, 0, 0,
            0, 0, vector.Z, 0,
            0, 0, 0, 1);

        /// <summary>
        /// Returns the transpose of the current matrix, which is obtained by swapping its rows and columns.
        /// </summary>
        /// <returns>The transposed matrix.</returns>
        public Matrix4D Transpose() => new(
                M11, M21, M31, M41,
                M12, M22, M32, M42,
                M13, M23, M33, M43,
                M14, M24, M34, M44);

        /// <summary>
        /// Attempts to invert the current matrix. If the matrix is invertible, the method returns true and outputs the inverted matrix; otherwise, it returns false and outputs the identity matrix.
        /// </summary>
        /// <param name="result">The inverted matrix if the current matrix is invertible; otherwise, the identity matrix.</param>
        /// <returns>True if the matrix is invertible; otherwise, false.</returns>
        public bool TryInvert(out Matrix4D result)
        {
            double m11 = Determinant(M22, M23, M24, M32, M33, M34, M42, M43, M44);
            double m12 = Determinant(M21, M23, M24, M31, M33, M34, M41, M43, M44);
            double m13 = Determinant(M21, M22, M24, M31, M32, M34, M41, M42, M44);
            double m14 = Determinant(M21, M22, M23, M31, M32, M33, M41, M42, M43);

            double det = M11 * m11 - M12 * m12 + M13 * m13 - M14 * m14;
            if (Math.Abs(det) < 1e-12)
            {
                result = Identity;
                return false;
            }

            double m21 = Determinant(M12, M13, M14, M32, M33, M34, M42, M43, M44);
            double m22 = Determinant(M11, M13, M14, M31, M33, M34, M41, M43, M44);
            double m23 = Determinant(M11, M12, M14, M31, M32, M34, M41, M42, M44);
            double m24 = Determinant(M11, M12, M13, M31, M32, M33, M41, M42, M43);

            double m31 = Determinant(M12, M13, M14, M22, M23, M24, M42, M43, M44);
            double m32 = Determinant(M11, M13, M14, M21, M23, M24, M41, M43, M44);
            double m33 = Determinant(M11, M12, M14, M21, M22, M24, M41, M42, M44);
            double m34 = Determinant(M11, M12, M13, M21, M22, M23, M41, M42, M43);

            double m41 = Determinant(M12, M13, M14, M22, M23, M24, M32, M33, M34);
            double m42 = Determinant(M11, M13, M14, M21, M23, M24, M31, M33, M34);
            double m43 = Determinant(M11, M12, M14, M21, M22, M24, M31, M32, M34);
            double m44 = Determinant(M11, M12, M13, M21, M22, M23, M31, M32, M33);

            double invDet = 1.0 / det;

            result = new Matrix4D(
                 m11 * invDet, -m21 * invDet, m31 * invDet, -m41 * invDet,
                -m12 * invDet, m22 * invDet, -m32 * invDet, m42 * invDet,
                 m13 * invDet, -m23 * invDet, m33 * invDet, -m43 * invDet,
                -m14 * invDet, m24 * invDet, -m34 * invDet, m44 * invDet);
            return true;
        }

        /// <summary>
        /// Multiplies two Matrix4D instances together, returning the resulting matrix.
        /// </summary>
        /// <param name="a">The first matrix to multiply.</param>
        /// <param name="b">The second matrix to multiply.</param>
        /// <returns>The resulting matrix from multiplying the two matrices.</returns>
        public static Matrix4D operator *(Matrix4D a, Matrix4D b)
        {
            return new Matrix4D(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
                a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
                a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
                a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

                a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
                a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
                a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
                a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current Matrix4D instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current Matrix4D instance.</param>
        /// <returns>true if the specified object is equal to the current Matrix4D instance; otherwise, false.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Matrix4D other && Equals(other);

        public bool Equals(Matrix4D other)
        {
            return M11 == other.M11 && M12 == other.M12 && M13 == other.M13 && M14 == other.M14 &&
                   M21 == other.M21 && M22 == other.M22 && M23 == other.M23 && M24 == other.M24 &&
                   M31 == other.M31 && M32 == other.M32 && M33 == other.M33 && M34 == other.M34 &&
                   M41 == other.M41 && M42 == other.M42 && M43 == other.M43 && M44 == other.M44;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="other">The matrix to compare with the current Matrix4D instance.</param>
        /// <param name="tolerance">The tolerance within which to consider the matrices equal.</param>
        /// <returns>true if the matrices are equal within the specified tolerance; otherwise, false.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool EquatableWithinTolerance(Matrix4D other, double tolerance = 0)
        {
            return Math.Abs(M11 - other.M11) <= tolerance && Math.Abs(M12 - other.M12) <= tolerance &&
                    Math.Abs(M13 - other.M13) <= tolerance && Math.Abs(M14 - other.M14) <= tolerance &&
                    Math.Abs(M21 - other.M21) <= tolerance && Math.Abs(M22 - other.M22) <= tolerance &&
                    Math.Abs(M23 - other.M23) <= tolerance && Math.Abs(M24 - other.M24) <= tolerance &&
                    Math.Abs(M31 - other.M31) <= tolerance && Math.Abs(M32 - other.M32) <= tolerance &&
                    Math.Abs(M33 - other.M33) <= tolerance && Math.Abs(M34 - other.M34) <= tolerance &&
                    Math.Abs(M41 - other.M41) <= tolerance && Math.Abs(M42 - other.M42) <= tolerance &&
                    Math.Abs(M43 - other.M43) <= tolerance && Math.Abs(M44 - other.M44) <= tolerance;
        }

        /// <summary>
        /// Returns a hash code for the current Matrix4D instance.
        /// </summary>
        /// <returns>A hash code for the current Matrix4D instance.</returns>
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(M11);
            hash.Add(M12);
            hash.Add(M13);
            hash.Add(M14);
            hash.Add(M21);
            hash.Add(M22);
            hash.Add(M23);
            hash.Add(M24);
            hash.Add(M31);
            hash.Add(M32);
            hash.Add(M33);
            hash.Add(M34);
            hash.Add(M41);
            hash.Add(M42);
            hash.Add(M43);
            hash.Add(M44);
            return hash.ToHashCode();
        }

        private static double Determinant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);

        public double Determinant()
        {
            double m11 = Determinant(M22, M23, M24, M32, M33, M34, M42, M43, M44);
            double m12 = Determinant(M21, M23, M24, M31, M33, M34, M41, M43, M44);
            double m13 = Determinant(M21, M22, M24, M31, M32, M34, M41, M42, M44);
            double m14 = Determinant(M21, M22, M23, M31, M32, M33, M41, M42, M43);
            return M11 * m11 - M12 * m12 + M13 * m13 - M14 * m14;
        }

    }
}
