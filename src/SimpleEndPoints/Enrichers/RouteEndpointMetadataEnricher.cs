using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Extensions;

namespace SimpleEndpoints.Enrichers
{
    public class RouteEndpointMetadataEnricher : IEndpointMetadataEnricher
    {
        private const string EndpointPlaceholder = "[endpoint]";
        private readonly SimpleEndpointsConfiguration _simpleEndpointsConfiguration;

        public RouteEndpointMetadataEnricher(IOptions<SimpleEndpointsConfiguration> simpleEndpointsConfiguration)
        {
            _simpleEndpointsConfiguration = simpleEndpointsConfiguration.Value;
        }

        public void Enrich(ControllerModel controller, Action<ControllerModel> next)
        {
            var routeBuilder = new StringBuilder();

            if (controller.Selectors.Any())
            {
                var routeTemplate = controller.Selectors[0].AttributeRouteModel.Template;

                if (routeTemplate.IndexOf(EndpointPlaceholder, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(_simpleEndpointsConfiguration.RoutePrefix))
                    {
                        routeBuilder
                            .Append($"{_simpleEndpointsConfiguration.RoutePrefix}/");
                    }

                    routeBuilder.Append(
                        routeTemplate.ReplaceCaseInsensitive(
                            $"{EndpointPlaceholder}",
                            controller.ControllerName.ReplaceCaseInsensitive(
                                _simpleEndpointsConfiguration.EndpointNamingConventionEnding,
                                string.Empty)));
                }
                else
                {
                    routeBuilder.Append(routeTemplate);
                }

                foreach (var keyValuePair in _simpleEndpointsConfiguration.RouteTokenDictionary)
                {
                    routeBuilder.Replace($"[{keyValuePair.Key}]", keyValuePair.Value);
                }

                controller.Selectors[0].AttributeRouteModel.Template = routeBuilder.ToString();
            }

            next(controller);
        }

        public int Order => _simpleEndpointsConfiguration.RouteEndpointMetadataEnricherOrder;
    }
}