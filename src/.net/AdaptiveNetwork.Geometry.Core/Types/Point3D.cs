using AdaptiveNetwork.Geometry.Core.Qualities;

using System.Diagnostics.CodeAnalysis;

namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a three-dimensional point with double precision coordinates.
    /// </summary>
    public struct Point3D : IEquatableWithinTolerance<Point3D>, IEquatableWithinTolerance<Vector3D>, ITranslatable<Point3D>
    {
        /// <summary>
        /// Gets the X coordinate of the point.
        /// </summary>
        public readonly double X;

        /// <summary>
        /// Gets the Y coordinate of the point.
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Gets the Z coordinate of the point.
        /// </summary>
        public readonly double Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3D"/> struct with the specified X, Y, and Z coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the point.</param>
        /// <param name="y">The Y coordinate of the point.</param>
        /// <param name="z">The Z coordinate of the point.</param>
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3D"/> struct from a <see cref="Vector3D"/> instance.
        /// </summary>
        /// <param name="vector">The Vector3D instance to initialize the Point3D from.</param>
        public Point3D(Vector3D vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        /// <summary>
        /// Returns a Point3D instance representing the origin (0, 0, 0).
        /// </summary>
        /// <returns>A Point3D instance representing the origin (0, 0, 0).</returns>
        public static readonly Point3D Origin = new(0, 0, 0);

        public override readonly string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current Point3D instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current Point3D instance.</param>
        /// <returns>true if the specified object is equal to the current Point3D instance; otherwise, false.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            throw new NotImplementedException("Point3D values should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        /// <summary>
        /// Returns a hash code for the current Point3D instance.
        /// </summary>
        /// <returns>A hash code for the current Point3D instance.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int GetHashCode()
        {
            throw new NotImplementedException("Point3D values should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        #region Almost Equatable Methods
        public bool EquatableWithinTolerance(Point3D other, double tolerance = 0)
        {
            return Math.Abs(X - other.X) <= tolerance &&
                   Math.Abs(Y - other.Y) <= tolerance &&
                   Math.Abs(Z - other.Z) <= tolerance;
        }

        public bool EquatableWithinTolerance(Vector3D other, double tolerance = 0)
        {
            return Math.Abs(X - other.X) <= tolerance &&
                   Math.Abs(Y - other.Y) <= tolerance &&
                   Math.Abs(Z - other.Z) <= tolerance;
        }

        #endregion

        #region Self Translating Methods

        public Point3D Translate(Vector3D vector3d)
        {
            Matrix4D matrix = Matrix4D.CreateTranslation(vector3d);
            double newX = X * matrix.M11 + Y * matrix.M21 + Z * matrix.M31 + matrix.M41;
            double newY = X * matrix.M12 + Y * matrix.M22 + Z * matrix.M32 + matrix.M42;
            double newZ = X * matrix.M13 + Y * matrix.M23 + Z * matrix.M33 + matrix.M43;
            return new Point3D(newX, newY, newZ);
        }

        public Point3D Translate(double x, double y, double z) => Translate(new Vector3D(x, y, z));

        public Point3D TranslateX(double x) => Translate(new Vector3D(x, 0, 0));

        public Point3D TranslateY(double y) => Translate(new Vector3D(0, y, 0));

        public Point3D TranslateZ(double z) => Translate(new Vector3D(0, 0, z));

        public Point3D TranslateXY(double x, double y) => Translate(new Vector3D(x, y, 0));

        /// <summary>
        /// Calculates the Euclidean distance between the current Point3D instance and another Point3D instance.
        /// </summary>
        /// <param name="other">The other Point3D instance to calculate the distance to.</param>
        /// <returns>The Euclidean distance between the current Point3D instance and the other Point3D instance.</returns>
        public double DistanceTo(Point3D other)
        {
            return Math.Sqrt(DistanceSquaredTo(other));
        }

        /// <summary>
        /// Calculates the squared Euclidean distance between the current Point3D instance and another Point3D instance. This method is useful for performance optimization when comparing distances, as it avoids the computational cost of taking the square root.
        /// </summary>
        /// <param name="other">The other Point3D instance to calculate the squared distance to.</param>
        /// <returns>The squared Euclidean distance between the current Point3D instance and the other Point3D instance.</returns>
        internal double DistanceSquaredTo(Point3D other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            double dz = Z - other.Z;
            return dx * dx + dy * dy + dz * dz;
        }

        #endregion
    }
}
