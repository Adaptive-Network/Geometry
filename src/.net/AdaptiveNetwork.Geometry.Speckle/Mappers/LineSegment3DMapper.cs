using AdaptiveNetwork.Geometry.Core.Types;
using AdaptiveNetwork.Geometry.Speckle.Abstractions;
using AdaptiveNetwork.Geometry.Speckle.Services;

using Speckle.Objects.Geometry;

namespace AdaptiveNetwork.Geometry.Speckle.Mappers
{
    /// <summary>
    /// Maps between LineSegment3D and Speckle Line objects.
    /// </summary>
    internal sealed class LineSegment3DMapper : IMapper<LineSegment3D, Line>, IMapper<Line, LineSegment3D>
    {
        readonly IMapper<Point3D, Point> _specklePointMapper;
        readonly IMapper<Point, Point3D> _coreMapper;
        readonly UnitsResolver _unitsResolver;
        public LineSegment3DMapper(IMapperResolver mapperResolver, UnitsResolver unitsResolver)
        {
            _specklePointMapper = mapperResolver.Resolve<Point3D, Point>();
            _coreMapper = mapperResolver.Resolve<Point, Point3D>();
            _unitsResolver = unitsResolver ?? throw new ArgumentNullException(nameof(unitsResolver));
        }

        public Line Map(LineSegment3D input)
        {
            Point start = _specklePointMapper.Map(input.Start);
            Point end = _specklePointMapper.Map(input.End);
            string units = _unitsResolver.Resolve();

            return new Line { end = end, start = start, units = units };
        }

        public LineSegment3D Map(Line input)
        {
            Point3D start = _coreMapper.Map(input.start);
            Point3D end = _coreMapper.Map(input.end);
            return new LineSegment3D(start, end);
        }
    }
}
