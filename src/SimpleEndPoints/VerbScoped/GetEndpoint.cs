using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    public interface IGetEndpoint
    {
    }

    public abstract class AsyncGetEndpoint : AsyncEndpoint, IGetEndpoint
    {
        public abstract override Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default);
    }

    public abstract class AsyncGetEndpointWithRequest<TRequest> : AsyncEndpointWithRequest<TRequest>, IGetEndpoint
    {
        public abstract override Task<IActionResult> HandleAsync([FromQuery] TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class AsyncGetEndpoint<TResponse> : AsyncEndpoint<TResponse>, IGetEndpoint
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(
            CancellationToken cancellationToken = default);
    }

    public abstract class AsyncGetEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>, IGetEndpoint
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync([FromQuery] TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class GetEndpoint : Endpoint, IGetEndpoint
    {
        public abstract override IActionResult Handle();
    }

    public abstract class GetEndpoint<TResponse> : Endpoint<TResponse>, IGetEndpoint
    {
        public abstract override ActionResult<TResponse> Handle();
    }

    public abstract class GetEndpointWithRequest<TRequest> : EndpointWithRequest<TRequest>, IGetEndpoint
    {
        public abstract override IActionResult Handle([FromQuery] TRequest model);
    }

    public abstract class GetEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>, IGetEndpoint
    {
        public abstract override ActionResult<TResponse> Handle([FromQuery] TRequest model);
    }
}

