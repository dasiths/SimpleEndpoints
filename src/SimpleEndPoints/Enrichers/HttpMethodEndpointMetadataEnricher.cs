using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.Enrichers
{
    public class HttpMethodEndpointMetadataEnricher : IEndpointMetadataEnricher
    {
        public void Enrich(ControllerModel controller, Action<ControllerModel> next)
        {
            if (controller.ControllerType
                .GetCustomAttributes(typeof(SimpleEndpointAttribute), true).FirstOrDefault() is SimpleEndpointAttribute attribute)
            {
                if (controller.Selectors.Any())
                {
                    if (!controller.Selectors[0].EndpointMetadata.Any(m => m is HttpMethodMetadata))
                    {
                        controller.Selectors[0].EndpointMetadata.Add(new HttpMethodMetadata(new[] { (attribute).HttpVerb }));
                    }
                }
            }

            next(controller);
        }
    }
}