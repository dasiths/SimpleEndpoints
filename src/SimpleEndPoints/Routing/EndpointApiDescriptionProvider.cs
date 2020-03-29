using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.Routing
{
    public class EndpointApiDescriptionProvider : IApiDescriptionProvider
    {
        private readonly ILogger<EndpointApiDescriptionProvider> _logger;
        private readonly SimpleEndpointsConfiguration _simpleEndpointsConfiguration;

        public EndpointApiDescriptionProvider(IOptions<SimpleEndpointsConfiguration> simpleEndpointsConfiguration, ILogger<EndpointApiDescriptionProvider> logger)
        {
            _logger = logger;
            _simpleEndpointsConfiguration = simpleEndpointsConfiguration.Value;
        }


        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var apiDescription in context.Results)
            {
                if (apiDescription.ActionDescriptor is ControllerActionDescriptor controller)
                {
                    _logger.LogTrace($"{controller.ControllerName}.{controller.ActionName}: {nameof(apiDescription.HttpMethod)} is" +
                                     $" {apiDescription.HttpMethod ?? "NULL"}");

                    if (apiDescription.HttpMethod is null)
                    {
                        if (controller.ControllerTypeInfo.GetCustomAttributes(typeof(SimpleEndpointAttribute),
                            true).FirstOrDefault() is SimpleEndpointAttribute attribute)
                        {
                            _logger.LogTrace($"Setting {controller.ControllerName} with {nameof(apiDescription.HttpMethod)} of: {attribute.HttpVerb}");
                            apiDescription.HttpMethod = attribute.HttpVerb;
                        }
                    }
                }
            }
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        public int Order => _simpleEndpointsConfiguration.EndpointApiDescriptionProviderOrder;
    }
}