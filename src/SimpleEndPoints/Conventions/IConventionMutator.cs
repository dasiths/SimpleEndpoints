using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SimpleEndpoints.Conventions
{
    internal interface IConventionMutator
    {
        void Mutate(ControllerModel controller, SimpleEndpointsConfiguration configuration);
    }
}