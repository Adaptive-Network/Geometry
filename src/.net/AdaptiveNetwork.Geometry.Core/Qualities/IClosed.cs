namespace AdaptiveNetwork.Geometry.Core.Qualities
{
    /// <summary>
    /// Defines a method for closing an object. This interface is intended for use with types that can be closed, such as shapes or paths. The <see cref="IsClosed"/> property indicates whether the object is currently closed, and the <see cref="Close"/> method attempts to close the object and returns a boolean indicating success or failure.
    /// </summary>
    public interface IClosed
    {
        /// <summary>
        /// Gets a value indicating whether the object is currently closed. A closed object is one that has no open ends or gaps, and is typically a complete shape or path.
        /// </summary>
        public bool IsClosed { get; }

        /// <summary>
        /// Attempts to close the object. If the object is already closed, this method may have no effect. If the object can be closed, this method will modify the object to make it closed and return true. If the object cannot be closed, this method will return false.
        /// </summary>
        /// <returns>True if the object was successfully closed; otherwise, false.</returns>
        public bool Close();
    }
}
