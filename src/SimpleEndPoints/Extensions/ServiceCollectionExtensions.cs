using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Enrichers;
using SimpleEndpoints.Routing;

namespace SimpleEndpoints.Extensions
{
    internal sealed class ConfigureMvcOptionsForSimpleEndpoints : IConfigureOptions<MvcOptions>
    {
        private readonly EndpointRoutingConvention _endpointRoutingConvention;

        public ConfigureMvcOptionsForSimpleEndpoints(EndpointRoutingConvention endpointRoutingConvention)
        {
            _endpointRoutingConvention = endpointRoutingConvention;
        }
        public void Configure(MvcOptions options)
        {
            options.Conventions.Add(_endpointRoutingConvention);
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleEndpointsRouting(this IServiceCollection services,
            Action<SimpleEndpointsConfiguration> configure = null)
        {
            // order of registration is order of invocation for enrichers
            services.AddSingleton<IEndpointMetadataEnricher, RouteEndpointMetadataEnricher>();
            services.AddSingleton<IEndpointMetadataEnricher, HttpMethodEndpointMetadataEnricher>();

            services.AddSingleton<EndpointRoutingConvention>();
            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureMvcOptionsForSimpleEndpoints>();

            services.AddSingleton<ApiDescriptionGroupCollectionProvider>();
            services.AddSingleton<IApiDescriptionGroupCollectionProvider>(x =>
                new ApiDescriptionGroupCollectionProviderDecorator(
                    x.GetService<ApiDescriptionGroupCollectionProvider>()));

            if (configure != null)
            {
                services.Configure<SimpleEndpointsConfiguration>(configure);
            }

            return services;
        }
    }
}