using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.Routing
{
    public class EndpointApiDescriptionProvider : IApiDescriptionProvider
    {
        private readonly SimpleEndpointsConfiguration _simpleEndpointsConfiguration;

        public EndpointApiDescriptionProvider(IOptions<SimpleEndpointsConfiguration> simpleEndpointsConfiguration)
        {
            _simpleEndpointsConfiguration = simpleEndpointsConfiguration.Value;
        }


        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var apiDescription in context.Results)
            {
                if (apiDescription.ActionDescriptor is ControllerActionDescriptor controller)
                {
                    if (apiDescription.HttpMethod is null)
                    {
                        if (controller.ControllerTypeInfo.GetCustomAttributes(typeof(SimpleEndpointAttribute),
                            true).FirstOrDefault() is SimpleEndpointAttribute attribute)
                        {
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