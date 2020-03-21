using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{

    public abstract class AsyncGetEndpoint : AsyncEndpoint
    {
        [HttpGet]
        public abstract override Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default);
    }

    public abstract class AsyncGetEndpointWithRequest<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        [HttpGet]
        public abstract override Task<IActionResult> HandleAsync([FromQuery] TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class AsyncGetEndpoint<TResponse> : AsyncEndpoint<TResponse>
    {
        [HttpGet]
        public abstract override Task<ActionResult<TResponse>> HandleAsync(
            CancellationToken cancellationToken = default);
    }

    public abstract class AsyncGetEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        [HttpGet]
        public abstract override Task<ActionResult<TResponse>> HandleAsync([FromQuery] TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class GetEndpoint : Endpoint
    {
        [HttpGet]
        public abstract override IActionResult Handle();
    }

    public abstract class GetEndpoint<TResponse> : Endpoint<TResponse>
    {
        [HttpGet]
        public abstract override ActionResult<TResponse> Handle();
    }

    public abstract class GetEndpointWithRequest<TRequest> : EndpointWithRequest<TRequest>
    {
        [HttpGet]
        public abstract override IActionResult Handle([FromQuery] TRequest model);
    }

    public abstract class GetEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        [HttpGet]
        public abstract override ActionResult<TResponse> Handle([FromQuery] TRequest model);
    }
}

