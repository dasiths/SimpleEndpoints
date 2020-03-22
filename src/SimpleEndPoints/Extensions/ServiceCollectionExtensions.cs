using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using SimpleEndpoints.Conventions;

namespace SimpleEndpoints.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleEndpointRouting(this IServiceCollection services,
            Action<SimpleEndpointsConfiguration> configure = null)
        {
            var configuration = new SimpleEndpointsConfiguration();
            configure?.Invoke(configuration);

            services.AddMvcCore(options => { options.Conventions.Add(new EndpointRoutingConvention(configuration)); });

            services.AddSingleton<ApiDescriptionGroupCollectionProvider>();
            services.AddSingleton<IApiDescriptionGroupCollectionProvider>(x =>
                new ApiDescriptionGroupCollectionProviderDecorator(
                    x.GetService<ApiDescriptionGroupCollectionProvider>()));

            return services;
        }
    }
}