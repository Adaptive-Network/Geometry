using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Qualities
{
    /// <summary>
    /// Defines a method for rotating an object in 3D space. This interface is intended for use with types that can be rotated by a vector and an angle, or around an axis and an angle. These methods will mutate the geometry to rotate it.
    /// </summary>
    public interface IRotatable<out TOut>
    {
        /// <summary>
        /// Rotates the current object around the specified vector by the given angle in radians.
        /// </summary>
        /// <param name="vector">The vector to rotate around.</param>
        /// <param name="radians">The angle in radians to rotate by.</param>
        TOut RotateRadians(Vector3D vector, double radians);

        /// <summary>
        /// Rotates the current object around the specified vector by the given angle in degrees.
        /// </summary>
        /// <param name="vector">The vector to rotate around.</param>
        /// <param name="degrees">The angle in degrees to rotate by.</param>
        TOut RotateDegrees(Vector3D vector, double degrees);

        /// <summary>
        /// Rotates the current object around the specified axis by the given angle in radians, with respect to a specified position in 3D space.
        /// </summary>
        /// <param name="position">The position in 3D space to rotate around.</param>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="radians">The angle in radians to rotate by.</param>
        TOut RotateAroundAxisRadians(Point3D position, Vector3D axis, double radians);

        /// <summary>
        /// Rotates the current object around the specified axis by the given angle in degrees, with respect to a specified position in 3D space.
        /// </summary>
        /// <param name="position">The position in 3D space to rotate around.</param>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="degrees">The angle in degrees to rotate by.</param>
        TOut RotateAroundAxisDegrees(Point3D position, Vector3D axis, double degrees);
    }
}
