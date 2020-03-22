using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Conventions
{
    internal class HttpMethodMetadataMutator : IConventionMutator
    {
        public void Mutate(ControllerModel controller, SimpleEndpointsConfiguration configuration)
        {
            var controllerInterfaces = controller.ControllerType.ImplementedInterfaces.ToArray();

            if (controllerInterfaces.Contains(typeof(IDeleteEndpoint)))
            {
                controller.Selectors[0].EndpointMetadata.Add(new HttpMethodMetadata(new[] {"DELETE"}));
            }
            else if (controllerInterfaces.Contains(typeof(IGetEndpoint)))
            {
                controller.Selectors[0].EndpointMetadata.Add(new HttpMethodMetadata(new[] {"GET"}));
            }
            else if (controllerInterfaces.Contains(typeof(IPostEndpoint)))
            {
                controller.Selectors[0].EndpointMetadata.Add(new HttpMethodMetadata(new[] {"POST"}));
            }
            else if (controllerInterfaces.Contains(typeof(IPutEndpoint)))
            {
                controller.Selectors[0].EndpointMetadata.Add(new HttpMethodMetadata(new[] {"PUT"}));
            }
        }
    }
}