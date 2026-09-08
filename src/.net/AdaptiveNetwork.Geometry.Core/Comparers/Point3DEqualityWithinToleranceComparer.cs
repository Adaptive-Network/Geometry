using AdaptiveNetwork.Geometry.Core.Types;

using System.Diagnostics.CodeAnalysis;

namespace AdaptiveNetwork.Geometry.Core.Comparers
{
    /// <summary>
    /// An equality comparer for <see cref="Point3D"/> that compares points within a specified tolerance.
    /// </summary>
    public readonly struct Point3DEqualityWithinToleranceComparer : IEqualityComparer<Point3D>
    {
        private readonly double _tolerance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3DEqualityWithinToleranceComparer"/> struct with the specified tolerance for point comparison.
        /// </summary>
        /// <param name="tolerance">The tolerance within which to compare points.</param>
        public Point3DEqualityWithinToleranceComparer(double tolerance = 0)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(tolerance, 0);
            _tolerance = tolerance;
        }

        /// <summary>
        /// Determines whether two <see cref="Point3D"/> instances are equal within the specified tolerance.
        /// </summary>
        /// <param name="x">The first <see cref="Point3D"/> to compare.</param>
        /// <param name="y">The second <see cref="Point3D"/> to compare.</param>
        /// <returns>True if the points are equal within the specified tolerance; otherwise, false.</returns>
        public bool Equals(Point3D x, Point3D y)
        {
            return x.EquatableWithinTolerance(y, _tolerance);
        }

        /// <summary>
        /// Returns a hash code for the specified <see cref="Point3D"/> instance, quantized to the comparer's tolerance so that points considered equal by <see cref="Equals(Point3D, Point3D)"/> are likely to share a hash code.
        /// </summary>
        /// <param name="obj">The <see cref="Point3D"/> instance for which to get a hash code.</param>
        /// <returns>A hash code for the specified <see cref="Point3D"/> instance.</returns>
        public int GetHashCode([DisallowNull] Point3D obj)
        {
            if (_tolerance == 0.0d)
            {
                return HashCode.Combine(obj.X, obj.Y, obj.Z);
            }

            return HashCode.Combine(
                Math.Round(obj.X / _tolerance),
                Math.Round(obj.Y / _tolerance),
                Math.Round(obj.Z / _tolerance));
        }
    }
}
