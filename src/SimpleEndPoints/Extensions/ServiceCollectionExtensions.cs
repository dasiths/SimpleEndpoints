using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
            services.AddEndpointMetadataEnricher<RouteEndpointMetadataEnricher>();
            services.AddEndpointMetadataEnricher<HttpMethodEndpointMetadataEnricher>();

            services.AddSingleton<EndpointRoutingConvention>();
            services.AddSingleton<IApiDescriptionProvider, EndpointApiDescriptionProvider>();
            services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureMvcOptionsForSimpleEndpoints>();

            if (configure != null)
            {
                services.Configure<SimpleEndpointsConfiguration>(configure);
            }

            return services;
        }

        public static IServiceCollection AddEndpointMetadataEnricher<T>(this IServiceCollection services) where T : class, IEndpointMetadataEnricher
        {
            services.AddSingleton<IEndpointMetadataEnricher, T>();
            return services;
        }
    }
}