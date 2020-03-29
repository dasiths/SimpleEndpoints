using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using SimpleEndpoints.Enrichers;

namespace SimpleEndpoints.Routing
{
    public sealed class EndpointRoutingConvention : IApplicationModelConvention
    {
        private readonly IEnumerable<IEndpointMetadataEnricher> _metadataEnrichers;
        private readonly ILogger<EndpointRoutingConvention> _logger;

        public EndpointRoutingConvention(IEnumerable<IEndpointMetadataEnricher> metadataEnrichers, ILogger<EndpointRoutingConvention> logger)
        {
            _metadataEnrichers = metadataEnrichers;
            _logger = logger;
        }

        public void Apply(ApplicationModel application)
        {
            void LastAction(ControllerModel c)
            {
                _logger.LogTrace("Enricher Pipeline end reached");
            }

            var apply = _metadataEnrichers.OrderBy(e => e.Order)
                .Aggregate((Action<ControllerModel>) LastAction, (action, enricher) => c =>
                {
                    _logger.LogTrace($"Enriching started using: {enricher.GetType().Name} with Order: {enricher.Order}");
                    enricher.Enrich(c, action);
                    _logger.LogTrace($"Enriching completed with: {enricher.GetType().Name}");
                });

            foreach (var controller in application.Controllers)
            {
                _logger.LogTrace($"Applying enricher pipeline for {controller.ControllerName}");
                apply(controller);
                _logger.LogTrace($"Completed enricher pipeline for {controller.ControllerName}");
            }
        }
    }
}
