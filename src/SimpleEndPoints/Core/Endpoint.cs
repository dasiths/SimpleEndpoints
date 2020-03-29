using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleEndpoints.Core
{
    public abstract class AsyncEndpoint : SimpleEndpointBase
    {
        public abstract Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default);
    }

    public abstract class AsyncEndpointWithRequest<TRequest> : SimpleEndpointBase
    {
        public abstract Task<IActionResult> HandleAsync(TRequest requestModel, CancellationToken cancellationToken = default);
    }

    public abstract class AsyncEndpoint<TResponse> : SimpleEndpointBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(CancellationToken cancellationToken = default);
    }

    public abstract class AsyncEndpoint<TRequest, TResponse> : SimpleEndpointBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest requestModel, CancellationToken cancellationToken = default);
    }

    public abstract class Endpoint : SimpleEndpointBase
    {
        public abstract IActionResult Handle();
    }

    public abstract class EndpointWithRequest<TRequest> : SimpleEndpointBase
    {
        public abstract IActionResult Handle(TRequest requestModel);
    }

    public abstract class Endpoint<TResponse> : SimpleEndpointBase
    {
        public abstract ActionResult<TResponse> Handle();
    }

    public abstract class Endpoint<TRequest, TResponse> : SimpleEndpointBase
    {
        public abstract ActionResult<TResponse> Handle(TRequest requestModel);
    }
}
