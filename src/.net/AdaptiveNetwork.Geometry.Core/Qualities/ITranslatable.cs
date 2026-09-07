using AdaptiveNetwork.Geometry.Core.Types;

namespace AdaptiveNetwork.Geometry.Core.Qualities
{
    /// <summary>
    /// Defines a method for translating an object in 3D space. This interface is intended for use with types that can be translated by a vector or by individual coordinate values.
    /// </summary>
    public interface ITranslatable<out TOut>
    {
        /// <summary>
        /// Translates the current object by the specified vector in 3D space.
        /// </summary>
        /// <param name="vector3d">The vector by which to translate the current object.</param>
        TOut Translate(Vector3D vector3d);

        /// <summary>
        /// Translates the current object by the specified x, y, and z values in 3D space.
        /// </summary>
        /// <param name="x">The value by which to translate the current object along the X axis.</param>
        /// <param name="y">The value by which to translate the current object along the Y axis.</param>
        /// <param name="z">The value by which to translate the current object along the Z axis.</param>
        TOut Translate(double x, double y, double z);

        /// <summary>
        /// Translates the current object by the specified x value in 3D space.
        /// </summary>
        /// <param name="x">The value by which to translate the current object along the X axis.</param>
        TOut TranslateX(double x);

        /// <summary>
        /// Translates the current object by the specified y value in 3D space.
        /// </summary>
        /// <param name="y">The value by which to translate the current object along the Y axis.</param>
        TOut TranslateY(double y);

        /// <summary>
        /// Translates the current object by the specified z value in 3D space.
        /// </summary>
        /// <param name="z">The value by which to translate the current object along the Z axis.</param>
        TOut TranslateZ(double z);

        /// <summary>
        /// Translates the current object by the specified x and y values in 3D space.
        /// </summary>
        /// <param name="x">The value by which to translate the current object along the X axis.</param>
        /// <param name="y">The value by which to translate the current object along the Y axis.</param>
        TOut TranslateXY(double x, double y);
    }
}
