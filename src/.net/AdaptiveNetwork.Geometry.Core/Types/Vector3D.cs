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

        /// <summary>
        /// Initializes a new instance of the Vector3D struct with default values (0, 0, 0).
        /// </summary>
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
        /// Initializes a new instance of the Vector3D struct from a Point3D instance, copying its X, Y, and Z coordinates.
        /// </summary>
        /// <param name="point">The Point3D instance to initialize the Vector3D from.</param>
        public Vector3D(Point3D point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
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

        /// <summary>
        /// <inheritdoc cref="IEquatableWithinTolerance{T}.EquatableWithinTolerance(T, double)"/>
        /// </summary>
        /// <param name="other">The other Vector3D instance to compare with.</param>
        /// <param name="tolerance">The tolerance within which the components are considered equal.</param>
        /// <returns>True if the components of the vectors are equal within the specified tolerance; otherwise, false.</returns>
        public bool EquatableWithinTolerance(Vector3D other, double tolerance = 0)
        {
            return Math.Abs(X - other.X) <= tolerance &&
                   Math.Abs(Y - other.Y) <= tolerance &&
                   Math.Abs(Z - other.Z) <= tolerance;
        }

        /// <summary>
        /// <inheritdoc cref="IEquatableWithinTolerance{T}.EquatableWithinTolerance(T, double)"/>
        /// </summary>
        /// <param name="other">The other Point3D instance to compare with.</param>
        /// <param name="tolerance">The tolerance within which the components are considered equal.</param>
        /// <returns>True if the components of the vectors are equal within the specified tolerance; otherwise, false.</returns>
        public bool EquatableWithinTolerance(Point3D other, double tolerance = 0)
        {
            return Math.Abs(X - other.X) <= tolerance &&
                   Math.Abs(Y - other.Y) <= tolerance &&
                   Math.Abs(Z - other.Z) <= tolerance;
        }

        /// <summary>
        /// Calculates the cross product of two Vector3D instances and returns a new Vector3D that is perpendicular to both input vectors.
        /// </summary>
        /// <param name="a">The first Vector3D instance.</param>
        /// <param name="b">The second Vector3D instance.</param>
        /// <returns>A new Vector3D instance representing the cross product of the two vectors.</returns>
        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            return new Vector3D(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        /// <summary>
        /// Calculates the dot product of two Vector3D instances and returns a scalar value.
        /// </summary>
        /// <param name="a">The first Vector3D instance.</param>
        /// <param name="b">The second Vector3D instance.</param>
        /// <returns>A double representing the dot product of the two vectors.</returns>
        public static double Dot(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Normalizes the given Vector3D instance, returning a new Vector3D with a length of 1. Throws an InvalidOperationException if the vector has zero length.
        /// </summary>
        /// <param name="vector">The Vector3D instance to normalize.</param>
        /// <returns>A new Vector3D instance with a length of 1.</returns>
        /// <exception cref="InvalidOperationException">Thrown when attempting to normalize a zero-length vector.</exception>
        public Vector3D Normalize()
        {
            double length = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (length <= 0)
                throw new InvalidOperationException("Cannot normalize a zero-length vector.");
            return new Vector3D(X / length, Y / length, Z / length);
        }

        /// <summary>
        /// Calculates the length (magnitude) of the Vector3D instance, which is the square root of the sum of the squares of its components.
        /// </summary>
        /// <returns>The length (magnitude) of the Vector3D instance.</returns>
        public double Magnitude() => Math.Sqrt(MagnitudeSquared());

        /// <summary>
        /// Calculates the squared length (magnitude) of the Vector3D instance, which is the sum of the squares of its components. This method avoids the computational cost of taking a square root and is useful for comparisons where the actual length is not needed.
        /// </summary>
        /// <returns>The squared length (magnitude) of the Vector3D instance.</returns>
        internal double MagnitudeSquared()
        {
            double x = X;
            double y = Y;
            double z = Z;
            return (x * x) + (y * y) + (z * z);
        }

        #region Operators

        /// <summary>
        /// Defines the addition operator for two Vector3D instances, returning a new Vector3D that is the component-wise sum of the two vectors.
        /// </summary>
        /// <param name="a">The first Vector3D instance.</param>
        /// <param name="b">The second Vector3D instance.</param>
        /// <returns>A new Vector3D instance representing the component-wise sum of the two vectors.</returns>
        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Defines the subtraction operator for two Vector3D instances, returning a new Vector3D that is the component-wise difference of the two vectors.
        /// </summary>
        /// <param name="a">The first Vector3D instance.</param>
        /// <param name="b">The second Vector3D instance.</param>
        /// <returns>A new Vector3D instance representing the component-wise difference of the two vectors.</returns>
        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Defines the multiplication operator for a Vector3D instance and a scalar, returning a new Vector3D that is the component-wise product of the vector and the scalar.
        /// </summary>
        /// <param name="a">The Vector3D instance.</param>
        /// <param name="scalar">The scalar value.</param>
        /// <returns>A new Vector3D instance representing the component-wise product of the vector and the scalar.</returns>
        public static Vector3D operator *(Vector3D a, double scalar)
        {
            return new Vector3D(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        /// <summary>
        /// Defines the division operator for a Vector3D instance and a scalar, returning a new Vector3D that is the component-wise quotient of the vector and the scalar. Throws a DivideByZeroException if the scalar is less than or equal to zero.
        /// </summary>
        /// <param name="a">The Vector3D instance.</param>
        /// <param name="scalar">The scalar value.</param>
        /// <returns>A new Vector3D instance representing the component-wise quotient of the vector and the scalar.</returns>
        /// <exception cref="DivideByZeroException">Thrown when the scalar is less than or equal to zero.</exception>
        public static Vector3D operator /(Vector3D a, double scalar)
        {
            if (scalar <= 0)
                throw new DivideByZeroException("Cannot divide by zero or a negative value.");
            return new Vector3D(a.X / scalar, a.Y / scalar, a.Z / scalar);
        }

        /// <summary>
        /// Defines the multiplication operator for a scalar and a Vector3D instance, returning a new Vector3D that is the component-wise product of the scalar and the vector.
        /// </summary>
        /// <param name="scalar">The scalar value.</param>
        /// <param name="a">The Vector3D instance.</param>
        /// <returns>A new Vector3D instance representing the component-wise product of the scalar and the vector.</returns>
        public static Vector3D operator *(double scalar, Vector3D a)
        {
            return new Vector3D(a.X * scalar, a.Y * scalar, a.Z * scalar);
        }

        /// <summary>
        /// Defines the equality operator for two Vector3D instances. This operator is not implemented and will throw a NotImplementedException. Use the EquatableWithinTolerance methods with a specified tolerance for comparison instead.
        /// </summary>
        /// <param name="a">The first Vector3D instance.</param>
        /// <param name="b">The second Vector3D instance.</param>
        /// <returns>Throws a NotImplementedException.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Vector3D operator ==(Vector3D a, Vector3D b)
        {
            throw new NotImplementedException("The Vector3D values should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        /// <summary>
        /// Defines the inequality operator for two Vector3D instances. This operator is not implemented and will throw a NotImplementedException. Use the EquatableWithinTolerance methods with a specified tolerance for comparison instead.
        /// </summary>
        /// <param name="a">The first Vector3D instance.</param>
        /// <param name="b">The second Vector3D instance.</param>
        /// <returns>Throws a NotImplementedException.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Vector3D operator !=(Vector3D a, Vector3D b)
        {
            throw new NotImplementedException("The Vector3D values should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        #endregion
    }
}
