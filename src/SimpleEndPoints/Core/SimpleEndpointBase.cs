using Microsoft.AspNetCore.Mvc;

namespace SimpleEndpoints.Core
{
    [ApiController]
    [Route(EndpointRoute)]
    public abstract class SimpleEndpointBase : ControllerBase
    {
        public const string EndpointRouteToken = "[endpoint]";
        public const string EndpointPrefixRouteToken = "[prefix]";
        public const string EndpointRoute = EndpointPrefixRouteToken + EndpointRouteToken;
    }
}