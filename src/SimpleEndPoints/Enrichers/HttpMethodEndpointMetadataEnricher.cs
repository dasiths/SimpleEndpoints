using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.Enrichers
{
    public class HttpMethodEndpointMetadataEnricher : IEndpointMetadataEnricher
    {
        private readonly ILogger<HttpMethodEndpointMetadataEnricher> _logger;
        private readonly SimpleEndpointsConfiguration _simpleEndpointsConfiguration;

        public HttpMethodEndpointMetadataEnricher(IOptions<SimpleEndpointsConfiguration> simpleEndpointsConfiguration, ILogger<HttpMethodEndpointMetadataEnricher> logger)
        {
            _logger = logger;
            _simpleEndpointsConfiguration = simpleEndpointsConfiguration.Value;
        }

        public void Enrich(ControllerModel controller, Action<ControllerModel> next)
        {
            _logger.LogTrace($"Start processing {controller.ControllerName}");
            _logger.LogTrace($"Inspecting for {nameof(SimpleEndpointAttribute)}");
            if (controller.ControllerType
                .GetCustomAttributes(typeof(SimpleEndpointAttribute), true).FirstOrDefault() is SimpleEndpointAttribute attribute)
            {
                _logger.LogTrace($"{nameof(SimpleEndpointAttribute)} found");
                if (controller.Selectors.Any())
                {
                    var hasExistingHttpMetadata =
                        !controller.Selectors[0].EndpointMetadata.Any(m => m is HttpMethodMetadata);
                    _logger.LogTrace($"{controller.ControllerName} has {nameof(HttpMethodMetadata)}: {hasExistingHttpMetadata}");

                    if (hasExistingHttpMetadata)
                    {
                        controller.Selectors[0].EndpointMetadata.Add(new HttpMethodMetadata(new[] { (attribute).HttpVerb }));
                        _logger.LogTrace($"{controller.ControllerName} has been added {nameof(HttpMethodMetadata)} with {attribute.HttpVerb}");
                    }
                }
            }

            _logger.LogTrace($"End processing {controller.ControllerName}. Invoking Next enricher.");

            next(controller);
        }

        public int Order => _simpleEndpointsConfiguration.HttpMethodEndpointMetadataEnricherOrder;
    }
}