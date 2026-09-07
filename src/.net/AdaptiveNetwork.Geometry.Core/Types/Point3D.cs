using AdaptiveNetwork.Geometry.Core.Qualities;

using System.Diagnostics.CodeAnalysis;

namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a three-dimensional point with double precision coordinates.
    /// </summary>
    public struct Point3D : IEquatableWithinTolerance<Point3D>, IEquatableWithinTolerance<Vector3D>
    {
        /// <summary>
        /// Gets the X coordinate of the point.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate of the point.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the Z coordinate of the point.
        /// </summary>
        public double Z { get; private set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns a Point3D instance representing the origin (0, 0, 0).
        /// </summary>
        /// <returns>A Point3D instance representing the origin (0, 0, 0).</returns>
        public static Point3D Origin() => new(0, 0, 0);

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
    }
}
