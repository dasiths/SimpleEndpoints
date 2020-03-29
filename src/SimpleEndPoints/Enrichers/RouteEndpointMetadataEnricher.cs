using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Extensions;

namespace SimpleEndpoints.Enrichers
{
    public class RouteEndpointMetadataEnricher : IEndpointMetadataEnricher
    {
        private const string EndpointPlaceholder = "[endpoint]";
        private readonly ILogger<RouteEndpointMetadataEnricher> _logger;
        private readonly SimpleEndpointsConfiguration _simpleEndpointsConfiguration;

        public RouteEndpointMetadataEnricher(IOptions<SimpleEndpointsConfiguration> simpleEndpointsConfiguration, ILogger<RouteEndpointMetadataEnricher> logger)
        {
            _logger = logger;
            _simpleEndpointsConfiguration = simpleEndpointsConfiguration.Value;
        }

        public void Enrich(ControllerModel controller, Action<ControllerModel> next)
        {
            _logger.LogTrace($"Start processing {controller.ControllerName}");
            var routeBuilder = new StringBuilder();

            if (controller.Selectors.Any())
            {
                var routeTemplate = controller.Selectors[0].AttributeRouteModel.Template;
                var containsEndpointPlaceholder =
                    routeTemplate.IndexOf(EndpointPlaceholder, StringComparison.OrdinalIgnoreCase) >= 0;

                _logger.LogTrace($"RouteTemplate is {routeTemplate} and contains {EndpointPlaceholder} = {containsEndpointPlaceholder}");

                if (containsEndpointPlaceholder)
                {
                    if (!string.IsNullOrWhiteSpace(_simpleEndpointsConfiguration.RoutePrefix))
                    {
                        _logger.LogTrace($"Route prefix of \"{_simpleEndpointsConfiguration.RoutePrefix}/\" applied");
                        routeBuilder
                            .Append($"{_simpleEndpointsConfiguration.RoutePrefix}/");
                    }

                    var controllerNameWithoutEndpointPlaceholder = controller.ControllerName.ReplaceCaseInsensitive(
                        _simpleEndpointsConfiguration.EndpointNamingConventionEnding,
                        string.Empty);

                    _logger.LogTrace($"Calculated endpoint name: {controllerNameWithoutEndpointPlaceholder}");
                    routeBuilder.Append(
                        routeTemplate.ReplaceCaseInsensitive(
                            $"{EndpointPlaceholder}", controllerNameWithoutEndpointPlaceholder));
                }
                else
                {
                    routeBuilder.Append(routeTemplate);
                }

                foreach (var keyValuePair in _simpleEndpointsConfiguration.RouteTokenDictionary)
                {
                    routeBuilder.Replace($"[{keyValuePair.Key}]", keyValuePair.Value);
                }

                var newRoute = routeBuilder.ToString();
                _logger.LogTrace($"Calculated route template: {newRoute}");

                controller.Selectors[0].AttributeRouteModel.Template = newRoute;
            }

            _logger.LogTrace($"End processing {controller.ControllerName}. Invoking Next enricher.");

            next(controller);
        }

        public int Order => _simpleEndpointsConfiguration.RouteEndpointMetadataEnricherOrder;
    }
}