using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SimpleEndpoints.Enrichers
{
    public interface IEndpointMetadataEnricher
    {
        void Enrich(ControllerModel controller, Action<ControllerModel> next);
    }
}