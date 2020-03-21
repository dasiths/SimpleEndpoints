using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    public interface IPutEndpoint
    {
    }

    public abstract class AsyncPutEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>, IPutEndpoint
    {
        public abstract override Task<IActionResult> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class AsyncPutEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>, IPutEndpoint
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class PutEndpoint<TRequest> : EndpointWithRequest<TRequest>, IPutEndpoint
    {
        public abstract override IActionResult Handle(TRequest model);
    }

    public abstract class PutEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>, IPutEndpoint
    {
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}