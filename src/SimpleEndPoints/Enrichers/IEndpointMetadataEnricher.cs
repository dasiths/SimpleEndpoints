using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SimpleEndpoints.Enrichers
{
    public interface IEndpointMetadataEnricher
    {
        void Enrich(ControllerModel controller, Action<ControllerModel> next);

        /// <summary>
        /// A enricher with a lower numeric value of
        /// Order will have its Enrich() method called after that of a provider
        /// with a higher numeric value of Order
        /// </summary>
        int Order { get; }
    }
}