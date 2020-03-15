using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{

    public abstract class AsyncGetEndpoint : AsyncEndpoint
    {
        [HttpGet]
        public virtual Task<IActionResult> Get(CancellationToken cancellationToken = default) =>
            HandleAsync(cancellationToken);
    }

    public abstract class AsyncGetEndpointWithRequest<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        [HttpGet]
        public virtual Task<IActionResult> Get(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    public abstract class AsyncGetEndpoint<TResponse> : AsyncEndpoint<TResponse>
    {
        [HttpGet]
        public virtual Task<ActionResult<TResponse>> Get(CancellationToken cancellationToken = default) =>
            HandleAsync(cancellationToken);
    }

    public abstract class AsyncGetEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        [HttpGet]
        public virtual Task<ActionResult<TResponse>> Get(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    public abstract class GetEndpoint : Endpoint
    {
        [HttpGet]
        public virtual IActionResult Get() =>
            Handle();
    }

    public abstract class GetEndpoint<TResponse> : Endpoint<TResponse>
    {
        [HttpGet]
        public virtual ActionResult<TResponse> Get() =>
            Handle();
    }

    public abstract class GetEndpointWithRequest<TRequest> : EndpointWithRequest<TRequest>
    {
        [HttpGet]
        public virtual IActionResult Get(TRequest model) =>
            Handle(model);
    }

    public abstract class GetEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        [HttpGet]
        public virtual ActionResult<TResponse> Get(TRequest model) =>
            Handle(model);
    }
}
