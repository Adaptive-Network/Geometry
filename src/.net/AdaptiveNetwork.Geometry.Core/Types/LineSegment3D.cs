using AdaptiveNetwork.Geometry.Core.Qualities;

using System.Diagnostics.CodeAnalysis;

namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a line segment in 3D space defined by two points: a starting point and an ending point.
    /// </summary>
    public sealed class LineSegment3D : IEquatableWithinTolerance<LineSegment3D>, ITranslatable<LineSegment3D>, IRotatable<LineSegment3D>
    {
        /// <summary>
        /// Gets the starting point of the line segment in 3D space.
        /// </summary>
        public Point3D Start { get; private set; }

        /// <summary>
        /// Gets the ending point of the line segment in 3D space.
        /// </summary>
        public Point3D End { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment3D"/> struct with default values for the starting and ending points.
        /// </summary>
        public LineSegment3D() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment3D"/> struct with the specified starting and ending points in 3D space.
        /// </summary>
        /// <param name="start">The starting point of the line segment.</param>
        /// <param name="end">The ending point of the line segment.</param>
        public LineSegment3D(Point3D start, Point3D end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment3D"/> struct with the specified starting point and direction vector in 3D space.
        /// </summary>
        /// <param name="start">The starting point of the line segment.</param>
        /// <param name="direction">The direction vector from the starting point to the ending point.</param>
        public LineSegment3D(Point3D start, Vector3D direction)
        {
            Start = start;
            End = new Point3D(start.X + direction.X, start.Y + direction.Y, start.Z + direction.Z);
        }

        /// <summary>
        /// Gets the squared length of the line segment.
        /// </summary>
        internal double LengthSquared() => (End.X - Start.X) * (End.X - Start.X) + (End.Y - Start.Y) * (End.Y - Start.Y) + (End.Z - Start.Z) * (End.Z - Start.Z);

        /// <summary>
        /// Gets the length of the line segment, calculated as the square root of the squared length.
        /// </summary>
        public double Length() => Math.Sqrt(LengthSquared());

        /// <summary>
        /// <inheritdoc cref="IEquatableWithinTolerance{T}.EquatableWithinTolerance(T, double)"/>
        /// </summary>
        /// <param name="other">The other <see cref="LineSegment3D"/> to compare with.</param>
        /// <param name="tolerance">The tolerance within which the two lines are considered equal.</param>
        /// <returns>True if the lines are equal within the specified tolerance; otherwise, false.</returns>
        public bool EquatableWithinTolerance(LineSegment3D other, double tolerance = 0)
        {
            return Start.EquatableWithinTolerance(other.Start, tolerance) && End.EquatableWithinTolerance(other.End, tolerance);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            throw new NotImplementedException("Line equality should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException("Line equality should be compared using the EquatableWithinTolerance methods with a specified tolerance");
        }

        public LineSegment3D Translate(Vector3D vector3d)
        {
            return new LineSegment3D(Start.Translate(vector3d), End.Translate(vector3d));
        }

        public LineSegment3D Translate(double x, double y, double z)
        {
            return Translate(new Vector3D(x, y, z));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="x">The distance to translate along the X axis.</param>
        public LineSegment3D TranslateX(double x) => Translate(new Vector3D(x, 0, 0));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="y">The distance to translate along the Y axis.</param>
        public LineSegment3D TranslateY(double y) => Translate(new Vector3D(0, y, 0));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="z">The distance to translate along the Z axis.</param>
        public LineSegment3D TranslateZ(double z) => Translate(new Vector3D(0, 0, z));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="x">The distance to translate along the X axis.</param>
        /// <param name="y">The distance to translate along the Y axis.</param>
        public LineSegment3D TranslateXY(double x, double y) => Translate(new Vector3D(x, y, 0));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="vector">The vector to rotate around.</param>
        /// <param name="radians">The angle in radians to rotate.</param>
        public LineSegment3D RotateRadians(Vector3D vector, double radians) => RotateAroundAxisRadians(Point3D.Origin, vector, radians);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="vector">The vector to rotate around.</param>
        /// <param name="degrees">The angle in degrees to rotate.</param>
        public LineSegment3D RotateDegrees(Vector3D vector, double degrees) => RotateRadians(vector, degrees * (Math.PI / 180.0));

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="position">The position in 3D space to rotate around.</param>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="radians">The angle in radians to rotate.</param>
        public LineSegment3D RotateAroundAxisRadians(Point3D position, Vector3D axis, double radians)
        {
            Matrix4D matrix = Matrix4D.CreateRotation(QuaternionD.CreateFromAxisAngle(axis, radians));
            return new LineSegment3D(RotateAroundPosition(Start, position, matrix), RotateAroundPosition(End, position, matrix));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="position">The position in 3D space to rotate around.</param>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="degrees">The angle in degrees to rotate.</param>
        public LineSegment3D RotateAroundAxisDegrees(Point3D position, Vector3D axis, double degrees) => RotateAroundAxisRadians(position, axis, degrees * (Math.PI / 180.0));

        /// <summary>
        /// Rotates a point around a specified position in 3D space using a given rotation matrix.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="position">The position to rotate around.</param>
        /// <param name="rotation">The rotation matrix to apply.</param>
        /// <returns>The rotated point.</returns>
        private static Point3D RotateAroundPosition(Point3D point, Point3D position, Matrix4D rotation)
        {
            double x = point.X - position.X;
            double y = point.Y - position.Y;
            double z = point.Z - position.Z;

            double rotatedX = x * rotation.M11 + y * rotation.M21 + z * rotation.M31 + rotation.M41;
            double rotatedY = x * rotation.M12 + y * rotation.M22 + z * rotation.M32 + rotation.M42;
            double rotatedZ = x * rotation.M13 + y * rotation.M23 + z * rotation.M33 + rotation.M43;

            return new Point3D(rotatedX + position.X, rotatedY + position.Y, rotatedZ + position.Z);
        }
    }
}
