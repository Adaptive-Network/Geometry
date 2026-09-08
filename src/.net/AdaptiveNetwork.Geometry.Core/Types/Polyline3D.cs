using AdaptiveNetwork.Geometry.Core.Comparers;
using AdaptiveNetwork.Geometry.Core.Qualities;

namespace AdaptiveNetwork.Geometry.Core.Types
{
    /// <summary>
    /// Represents a 3D polyline, which is a series of connected line segments defined by a collection of 3D points. The points are stored in a HashSet to ensure uniqueness, and an optional equality comparer can be provided to customize point comparison.
    /// </summary>
    public sealed class Polyline3D : IEquatableWithinTolerance<Polyline3D>, ITranslatable<Polyline3D>, IRotatable<Polyline3D>, IClosed, IGeometry
    {
        /// <summary>
        /// The optional equality comparer used to compare points in the polyline. If provided, it allows for custom comparison logic, such as comparing points within a certain tolerance.
        /// </summary>
        private readonly IEqualityComparer<Point3D>? _pointEqualityComparer = new Point3DEqualityWithinToleranceComparer(0);

        /// <summary>
        /// Gets the collection of 3D points that define the polyline. The points are stored in a HashSet to ensure that each point is unique within the polyline.
        /// </summary>
        public HashSet<Point3D> Points { get; private set; }

        /// <summary>
        /// Gets the number of points in the polyline. This property returns the count of unique points stored in the HashSet.
        /// </summary>
        public int Count => Points.Count;

        /// <summary>
        /// Gets a value indicating whether the polyline is closed. A polyline is considered closed if it has at least three points and the first and last points are equal, considering the provided equality comparer.
        /// </summary>
        public bool IsClosed => DetermineIsClosed();

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline3D"/> class with an empty collection of points. The points are stored in a HashSet to ensure uniqueness, and an optional equality comparer can be provided to customize point comparison.
        /// </summary>
        public Polyline3D()
        {
            Points = new HashSet<Point3D>(_pointEqualityComparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline3D"/> class with an empty collection of points and a specified equality comparer. The points are stored in a HashSet to ensure uniqueness, and the provided equality comparer is used to customize point comparison.
        /// </summary>
        /// <param name="pointEqualityComparer">The equality comparer to use for comparing points.</param>
        public Polyline3D(IEqualityComparer<Point3D> pointEqualityComparer)
        {
            _pointEqualityComparer = pointEqualityComparer;
            Points = new(_pointEqualityComparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline3D"/> class with a specified collection of points. The points are stored in a HashSet to ensure uniqueness, and an optional equality comparer can be provided to customize point comparison.
        /// </summary>
        /// <param name="points">The collection of points to initialize the polyline with.</param>
        public Polyline3D(IEnumerable<Point3D> points)
        {
            Points = new HashSet<Point3D>(points, _pointEqualityComparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline3D"/> class with a specified equality comparer and a collection of points. The points are stored in a HashSet to ensure uniqueness, and the provided equality comparer is used to customize point comparison.
        /// </summary>
        /// <param name="pointEqualityComparer">The equality comparer to use for comparing points.</param>
        /// <param name="points">The collection of points to initialize the polyline with.</param>
        public Polyline3D(IEqualityComparer<Point3D> pointEqualityComparer, IEnumerable<Point3D> points)
        {
            _pointEqualityComparer = pointEqualityComparer;
            Points = new HashSet<Point3D>(points, _pointEqualityComparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline3D"/> class with a specified collection of points. The points are stored in a HashSet to ensure uniqueness, and an optional equality comparer can be provided to customize point comparison.
        /// </summary>
        /// <param name="points">The collection of points to initialize the polyline with.</param>
        public Polyline3D(params Point3D[] points)
        {
            Points = new HashSet<Point3D>(points, _pointEqualityComparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline3D"/> class with a specified equality comparer and a collection of points. The points are stored in a HashSet to ensure uniqueness, and the provided equality comparer is used to customize point comparison.
        /// </summary>
        /// <param name="pointEqualityComparer">The equality comparer to use for comparing points.</param>
        /// <param name="points">The collection of points to initialize the polyline with.</param>
        public Polyline3D(IEqualityComparer<Point3D> pointEqualityComparer, params Point3D[] points)
        {
            _pointEqualityComparer = pointEqualityComparer;
            Points = new HashSet<Point3D>(points, _pointEqualityComparer);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="other">The <see cref="Polyline3D"/> to compare with the current instance.</param>
        /// <param name="tolerance">The tolerance within which to compare the points.</param>
        /// <returns>True if the polylines are equal within the specified tolerance; otherwise, false.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool EquatableWithinTolerance(Polyline3D other, double tolerance = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="vector3d">The vector by which to translate the polyline.</param>
        /// <returns>A new <see cref="Polyline3D"/> translated by the specified vector.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Polyline3D Translate(Vector3D vector3d)
        {
            throw new NotImplementedException();
        }

        public Polyline3D Translate(double x, double y, double z)
        {
            throw new NotImplementedException();
        }

        public Polyline3D TranslateX(double x)
        {
            throw new NotImplementedException();
        }

        public Polyline3D TranslateY(double y)
        {
            throw new NotImplementedException();
        }

        public Polyline3D TranslateZ(double z)
        {
            throw new NotImplementedException();
        }

        public Polyline3D TranslateXY(double x, double y)
        {
            throw new NotImplementedException();
        }

        public Polyline3D RotateRadians(Vector3D vector, double radians)
        {
            throw new NotImplementedException();
        }

        public Polyline3D RotateDegrees(Vector3D vector, double degrees)
        {
            throw new NotImplementedException();
        }

        public Polyline3D RotateAroundAxisRadians(Point3D position, Vector3D axis, double radians)
        {
            throw new NotImplementedException();
        }

        public Polyline3D RotateAroundAxisDegrees(Point3D position, Vector3D axis, double degrees)
        {
            throw new NotImplementedException();
        }

        public bool Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the polyline is closed by checking if the first and last points are equal, considering the provided equality comparer. A polyline is considered closed if it has at least three points and the first and last points are equal.
        /// </summary>
        /// <returns>True if the polyline is closed; otherwise, false.</returns>
        private bool DetermineIsClosed()
        {
            if (Count < 3) return false; // A polyline with fewer than 3 points cannot be closed.
            Point3D first = Points.First();
            Point3D last = Points.Last();

            IEqualityComparer<Point3D> comparer = _pointEqualityComparer ?? new Point3DEqualityWithinToleranceComparer();
            bool firstEqualsLast = comparer.Equals(first, last);
            return firstEqualsLast;
        }
    }
}
