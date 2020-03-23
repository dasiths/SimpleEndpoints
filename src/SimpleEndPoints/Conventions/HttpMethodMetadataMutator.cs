using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using SimpleEndpoints.Core;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Conventions
{
    internal class HttpMethodMetadataMutator : IConventionMutator
    {
        public void Mutate(ControllerModel controller, SimpleEndpointsConfiguration configuration)
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
        }
    }
}