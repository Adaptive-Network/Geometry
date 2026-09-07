using AdaptiveNetwork.Geometry.Core.Qualities;

using System.Diagnostics.CodeAnalysis;

namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a three-dimensional vector with double precision components.
    /// </summary>
    public struct Vector3D : IEquatableWithinTolerance<Vector3D>, IEquatableWithinTolerance<Point3D>
    {
        /// <summary>
        /// Gets the X component of the vector.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Gets the Y component of the vector.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Gets the Z component of the vector.
        /// </summary>
        public double Z { get; private set; }

        public Vector3D() { }

        /// <summary>
        /// Initializes a new instance of the Vector3D struct with the specified X, Y, and Z components.
        /// </summary>
        /// <param name="x">The X component of the vector.</param>
        /// <param name="y">The Y component of the vector.</param>
        /// <param name="z">The Z component of the vector.</param>
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns a new Vector3D instance representing the origin (0, 0, 0).
        /// </summary>
        /// <returns>A Vector3D instance with all components set to 0.</returns>
        public static Vector3D Origin() => new(0, 0, 0);

        /// <summary>
        /// Returns a string representation of the Vector3D instance in the format "X, Y, Z".
        /// </summary>
        /// <returns>A string representation of the Vector3D instance.</returns>
        public override readonly string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            throw new NotImplementedException("The Vector3D values should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException("The Vector3D values should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        public bool EquatableWithinTolerance(Vector3D other, double tolerance = 0)
        {
            return Math.Abs(X - other.X) <= tolerance &&
                   Math.Abs(Y - other.Y) <= tolerance &&
                   Math.Abs(Z - other.Z) <= tolerance;
        }

        public bool EquatableWithinTolerance(Point3D other, double tolerance = 0)
        {
            return Math.Abs(X - other.X) <= tolerance &&
                   Math.Abs(Y - other.Y) <= tolerance &&
                   Math.Abs(Z - other.Z) <= tolerance;
        }
    }
}
