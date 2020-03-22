using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SimpleEndpoints.Conventions
{
    internal class RouteMutator : IConventionMutator
    {
        private const string EndpointPlaceholder = "[endpoint]";

        public void Mutate(ControllerModel controller, SimpleEndpointsConfiguration configuration)
        {
            var routeBuilder = new StringBuilder();
            var routeTemplate = controller.Selectors[0].AttributeRouteModel.Template;

            if (routeTemplate.IndexOf(EndpointPlaceholder, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                if (!string.IsNullOrWhiteSpace(configuration.RoutePrefix))
                {
                    routeBuilder
                        .Append($"/{configuration.RoutePrefix}/");
                }

                routeBuilder.Append(routeTemplate.Replace($"{EndpointPlaceholder}",
                    controller.ControllerName.Replace(configuration.EndpointReplacementToken, string.Empty)));
            }
            else
            {
                routeBuilder.Append(routeTemplate);
            }

            controller.Selectors[0].AttributeRouteModel.Template = routeBuilder.ToString();
        }
    }
}