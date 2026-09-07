namespace AdaptiveNetwork.Geometry.Core.Qualities
{
    /// <summary>
    /// Defines a method for determining whether two objects are equal within a distance tolerance. This interface is intended for use with types that require a custom equality comparison that takes into account a specified tolerance value.
    /// </summary>
    /// <typeparam name="TIn">The type of the objects to compare.</typeparam>
    public interface IEquatableWithinTolerance<in TIn>
    {
        /// <summary>
        /// Determines whether the current object is almost equal to another object of the same type, within a specified tolerance.
        /// </summary>
        /// <param name="other">The other object to compare with the current object.</param>
        /// <param name="tolerance">The tolerance within which the two objects are considered almost equal.</param>
        /// <returns>True if the current object is almost equal to the other object within the specified tolerance; otherwise, false.</returns>
        bool EquatableWithinTolerance(TIn other, double tolerance = 0);
    }
}
