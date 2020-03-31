using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Core;
using SimpleEndpoints.Extensions;

namespace SimpleEndpoints.Enrichers
{
    public class RouteEndpointMetadataEnricher : IEndpointMetadataEnricher
    {
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
                var containsEndpointRouteToken =
                    routeTemplate.IndexOf(SimpleEndpointBase.EndpointRouteToken, StringComparison.OrdinalIgnoreCase) >= 0;
                var containsPrefixRouteToken = 
                    routeTemplate.IndexOf(SimpleEndpointBase.EndpointPrefixRouteToken, StringComparison.OrdinalIgnoreCase) >= 0;

                _logger.LogTrace($"RouteTemplate is {routeTemplate}");
                _logger.LogTrace($"RouteTemplate contains {SimpleEndpointBase.EndpointRouteToken} = {containsEndpointRouteToken}");
                _logger.LogTrace($"RouteTemplate contains {SimpleEndpointBase.EndpointPrefixRouteToken} = {containsPrefixRouteToken}");

                routeBuilder.Append(routeTemplate);

                if (containsEndpointRouteToken || containsPrefixRouteToken)
                {
                    if (containsPrefixRouteToken)
                    {
                        _logger.LogTrace($"Route prefix of \"{_simpleEndpointsConfiguration.RoutePrefix}/\" applied");
                        routeBuilder.Replace(
                            $"{SimpleEndpointBase.EndpointPrefixRouteToken}",
                            $"{_simpleEndpointsConfiguration.RoutePrefix}/");
                    }

                    if (containsEndpointRouteToken)
                    {
                        var controllerNameWithoutEndpoint = controller.ControllerName.ReplaceCaseInsensitive(
                            _simpleEndpointsConfiguration.EndpointNamingConventionEnding,
                            string.Empty);

                        _logger.LogTrace($"Calculated endpoint name: {controllerNameWithoutEndpoint}");

                        routeBuilder.Replace(
                                $"{SimpleEndpointBase.EndpointRouteToken}", controllerNameWithoutEndpoint);
                    }
                }

                foreach (var keyValuePair in _simpleEndpointsConfiguration.RouteTokenDictionary)
                {
                    _logger.LogTrace($"Replacing custom route token [{keyValuePair.Key}] with {keyValuePair.Value}");
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