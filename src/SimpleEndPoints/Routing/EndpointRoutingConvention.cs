using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SimpleEndpoints.Enrichers;

namespace SimpleEndpoints.Routing
{
    public sealed class EndpointRoutingConvention : IApplicationModelConvention
    {
        private readonly IEnumerable<IEndpointMetadataEnricher> _metadataEnrichers;

        public EndpointRoutingConvention(IEnumerable<IEndpointMetadataEnricher> metadataEnrichers)
        {
            _metadataEnrichers = metadataEnrichers;
        }

        public void Apply(ApplicationModel application)
        {
            Action<ControllerModel> lastAction = c => {};

            foreach (var controller in application.Controllers)
            {
                var apply = _metadataEnrichers.Reverse()
                    .Aggregate(lastAction, (action, enricher) => c => enricher.Enrich(c, action));
                apply(controller);
            }
        }
    }
}
