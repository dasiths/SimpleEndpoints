using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    [SimpleEndpoint("GET")]
    public abstract class AsyncGetEndpoint : AsyncEndpoint
    {
        public abstract override Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("GET")]
    public abstract class AsyncGetEndpointWithRequest<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        public abstract override Task<IActionResult> HandleAsync([FromQuery] TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("GET")]
    public abstract class AsyncGetEndpoint<TResponse> : AsyncEndpoint<TResponse>
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("GET")]
    public abstract class AsyncGetEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync([FromQuery] TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("GET")]
    public abstract class GetEndpoint : Endpoint
    {
        public abstract override IActionResult Handle();
    }

    [SimpleEndpoint("GET")]
    public abstract class GetEndpoint<TResponse> : Endpoint<TResponse>
    {
        public abstract override ActionResult<TResponse> Handle();
    }

    [SimpleEndpoint("GET")]
    public abstract class GetEndpointWithRequest<TRequest> : EndpointWithRequest<TRequest>
    {
        public abstract override IActionResult Handle([FromQuery] TRequest model);
    }

    [SimpleEndpoint("GET")]
    public abstract class GetEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        public abstract override ActionResult<TResponse> Handle([FromQuery] TRequest model);
    }
}

