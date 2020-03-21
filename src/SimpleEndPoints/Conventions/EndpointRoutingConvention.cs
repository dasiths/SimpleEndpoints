using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using SimpleEndpoints.Extensions;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Conventions
{
    public class EndpointRoutingConvention : IApplicationModelConvention
    {
        private const string EndpointPlaceholder = "[endpoint]";

        private readonly SimpleEndpointsConfiguration _configuration;

        public EndpointRoutingConvention(SimpleEndpointsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                MapRouteConvention(controller);
            }
        }

        private  void MapRouteConvention(ControllerModel controller)
        {
            var routeTemplate = controller.Selectors[0].AttributeRouteModel.Template;

            //If we haven't set a route with [Route("")] on our endpoint
            if (routeTemplate.Equals(EndpointPlaceholder, StringComparison.OrdinalIgnoreCase))
            {
                var routeBuilder = new StringBuilder();

                //Do we want to set a global route prefix e.g. api/*?
                if (!string.IsNullOrWhiteSpace(_configuration.RoutePrefix))
                {
                    routeBuilder.Append($"{_configuration.RoutePrefix}/");
                }

                //Do a replacement on the Endpoint name e.g. MyGreatEndpoint to just MyGreat
                routeBuilder.Append(routeTemplate.Replace($"{EndpointPlaceholder}",
                    controller.ControllerName.Replace(_configuration.EndpointReplacementToken, string.Empty)));

                controller.Selectors[0].AttributeRouteModel.Template = routeBuilder.ToString();
            }

            AddHttpMethodMetadata(controller);
        }

        //This allows us to get away from having to add Http verb attributes to our action methods, it can be driven by base class
        private static void AddHttpMethodMetadata(ControllerModel controller)
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
