namespace AdaptiveNetwork.Geometry.Speckle.Abstractions
{
    /// <summary>
    /// Defines a method for resolving an IMapper instance for a given input and output type. This interface is intended for use with types that can be mapped from one type to another.
    /// </summary>
    public interface IMapperResolver
    {
        /// <summary>
        /// Resolves an IMapper instance for the specified input and output types. The resolved IMapper instance can be used to map objects of type TIn to objects of type TOut.
        /// </summary>
        /// <typeparam name="TIn">The type of the input object to be mapped.</typeparam>
        /// <typeparam name="TOut">The type of the output object to be mapped to.</typeparam>
        /// <returns>An IMapper instance that can map objects of type TIn to objects of type TOut.</returns>
        public IMapper<TIn, TOut> Resolve<TIn, TOut>();
    }
}
