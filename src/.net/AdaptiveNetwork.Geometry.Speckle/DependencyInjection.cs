using AdaptiveNetwork.Geometry.Speckle.Abstractions;
using AdaptiveNetwork.Geometry.Speckle.Services;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace AdaptiveNetwork.Geometry.Speckle
{
    /// <summary>
    /// Provides extension methods for registering AdaptiveNetwork.Geometry.Speckle services with the dependency injection container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the AdaptiveNetwork.Geometry.Speckle services with the specified units of measurement.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="units">The units of measurement to use across all speckle conversions.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RegisterAdaptiveNetworkSpeckle(this IServiceCollection services, string units = "meters")
        {
            Assembly thisAssembly = typeof(DependencyInjection).Assembly;
            List<Type> mappers = thisAssembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
                .ToList();
            foreach (Type mapper in mappers)
            {
                services.AddSingleton(typeof(IMapper<,>), mapper);
            }
            services.AddSingleton(new UnitsResolver(units));
            services.AddSingleton<IMapperResolver, ServiceProviderMapperResolver>();
            return services;
        }
    }
}
