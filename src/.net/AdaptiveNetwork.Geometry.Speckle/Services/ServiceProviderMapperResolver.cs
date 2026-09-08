using AdaptiveNetwork.Geometry.Speckle.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace AdaptiveNetwork.Geometry.Speckle.Services
{
    /// <summary>
    /// A mapper resolver that uses an IServiceProvider to resolve mappers for specified input and output types. This class implements the IMapperResolver interface and provides a method to resolve mappers using dependency injection.
    /// </summary>
    internal sealed class ServiceProviderMapperResolver : IMapperResolver
    {
        /// <summary>
        /// The IServiceProvider instance used to resolve mappers. This field is initialized through the constructor and is used in the Resolve method to obtain the appropriate mapper for the specified input and output types.
        /// </summary>
        readonly IServiceProvider _serviceProvider;
        public ServiceProviderMapperResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Resolves a mapper for the specified input and output types using the IServiceProvider. If the mapper cannot be resolved, an InvalidOperationException is thrown. This method is part of the IMapperResolver interface implementation.
        /// </summary>
        /// <typeparam name="TIn">The type of the input object to be mapped.</typeparam>
        /// <typeparam name="TOut">The type of the output object to be mapped to.</typeparam>
        /// <returns>An IMapper instance that can map objects of type TIn to objects of type TOut.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the mapper cannot be resolved.</exception>
        public IMapper<TIn, TOut> Resolve<TIn, TOut>()
        {
            IMapper<TIn, TOut>? mapper = _serviceProvider.GetRequiredService<IMapper<TIn, TOut>>();
            if (mapper is null) throw new InvalidOperationException($"Could not resolve mapper for the specified types {typeof(TIn)} and {typeof(TOut)}.");
            return mapper;
        }
    }
}
