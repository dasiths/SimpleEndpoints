using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

namespace SimpleEndpoints.Conventions
{
    public sealed class EndpointRoutingConvention : IApplicationModelConvention
    {
        private readonly IConventionMutator[] _conventionMutators =
        {
            new RouteMutator(),
            new HttpMethodMetadataMutator()
        };

        private readonly SimpleEndpointsConfiguration _configuration;

        public EndpointRoutingConvention(IOptions<SimpleEndpointsConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var mutator in _conventionMutators)
                {
                    mutator.Mutate(controller, _configuration);
                }
            }
        }
    }
}
