using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SimpleEndpoints.Enrichers
{
    public interface IEndpointMetadataEnricher
    {
        /// <summary>
        /// Enrich the <see cref="ControllerModel"/> with metadata
        /// </summary>
        /// <param name="controller"><see cref="ControllerModel"/> to enrich</param>
        /// <param name="next">The action to invoke the next <see cref="IEndpointMetadataEnricher"/> in the pipeline</param>
        void Enrich(ControllerModel controller, Action<ControllerModel> next);

        /// <summary>
        /// A enricher with a lower numeric value of <see cref="Order"/>
        /// will have its <see cref="Enrich"/> method called after that of a provider
        /// with a higher numeric value of <see cref="Order"/>
        /// </summary>
        int Order { get; }
    }
}