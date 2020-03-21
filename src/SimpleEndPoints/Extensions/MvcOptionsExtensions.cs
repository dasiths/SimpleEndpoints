using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using SimpleEndpoints.Conventions;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static void WithSimpleEndpoints(this MvcOptions options, Action<SimpleEndpointsConfiguration> configure = null)
        {
            var configuration = new SimpleEndpointsConfiguration();
            configure?.Invoke(configuration);

            options.Conventions.Add(new EndpointRoutingConvention(configuration));
        }
    }

    public class SimpleEndpointsConfiguration
    {
        public string RoutePrefix { get; private set; }
        public string EndpointReplacementToken { get; private set; }

        public SimpleEndpointsConfiguration WithRoutePrefix(string prefix)
        {
            RoutePrefix = prefix;
            return this;
        }

        public SimpleEndpointsConfiguration WithEndpointNamingConvention(string endpointReplacementToken = "Endpoint")
        {
            EndpointReplacementToken = endpointReplacementToken;
            return this;
        }
    }

    public static class ServiceCollectionExtensions
    {
        //We need to decorate the api description for swagger
        public static IServiceCollection AddSimpleEndpointRouting(this IServiceCollection services)
        {
            services.AddSingleton<ApiDescriptionGroupCollectionProvider>();
            services.AddSingleton<IApiDescriptionGroupCollectionProvider>(x =>
                new ApiDescriptionGroupCollectionProviderDecorator(x.GetService<ApiDescriptionGroupCollectionProvider>()));

            return services;
        }
    }
    
    public class ApiDescriptionGroupCollectionProviderDecorator : IApiDescriptionGroupCollectionProvider
    {
        private readonly IApiDescriptionGroupCollectionProvider _inner;

        public ApiDescriptionGroupCollectionProviderDecorator(IApiDescriptionGroupCollectionProvider inner)
        {
            _inner = inner;
        }

        public ApiDescriptionGroupCollection ApiDescriptionGroups
        {
            get
            {
                //As we are not using HttpVerb attributes for out actions so swagger doesn't know how to render it
                foreach (var apiDescriptionGroup in _inner.ApiDescriptionGroups.Items)
                {
                    foreach (var apiDescription in apiDescriptionGroup.Items)
                    {
                        if (apiDescription.ActionDescriptor is ControllerActionDescriptor controller)
                        {
                            if (typeof(IDeleteEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "DELETE";
                            }
                            if (typeof(IGetEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "GET";
                            }
                            if (typeof(IPostEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "POST";
                            }
                            if (typeof(IPutEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "PUT";
                            }
                        }
                    }
                }

                return _inner.ApiDescriptionGroups;
            }
        }
    }
}