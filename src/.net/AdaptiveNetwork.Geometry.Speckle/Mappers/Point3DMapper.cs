using AdaptiveNetwork.Geometry.Core.Types;
using AdaptiveNetwork.Geometry.Speckle.Abstractions;
using AdaptiveNetwork.Geometry.Speckle.Services;

using Speckle.Objects.Geometry;

namespace AdaptiveNetwork.Geometry.Speckle.Mappers
{
    /// <summary>
    /// A mapper that converts between Point3D and Speckle Point objects.
    /// </summary>
    internal sealed class Point3DMapper : IMapper<Point3D, Point>, IMapper<Point, Point3D>
    {
        readonly UnitsResolver _unitsResolver;

        public Point3DMapper(UnitsResolver unitsResolver)
        {
            _unitsResolver = unitsResolver ?? throw new ArgumentNullException(nameof(unitsResolver));
        }

        /// <summary>
        /// Maps a Point3D object to a Speckle Point object.
        /// </summary>
        /// <param name="input">The Point3D object to map.</param>
        /// <returns>A Speckle Point object.</returns>
        public Point Map(Point3D input)
        {
            string units = _unitsResolver.Resolve();
            Point specklePoint = new Point(input.X, input.Y, input.Z, units, null);
            return specklePoint;
        }

        /// <summary>
        /// Maps a Speckle Point object to a Point3D object.
        /// </summary>
        /// <param name="input">The Speckle Point object to map.</param>
        /// <returns>A Point3D object.</returns>
        public Point3D Map(Point input)
        {
            Point3D point3D = new Point3D(input.x, input.y, input.z);
            return point3D;
        }
    }
}
