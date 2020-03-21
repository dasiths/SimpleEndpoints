using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    public interface IDeleteEndpoint
    {
    }

    public abstract class AsyncDeleteEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>, IDeleteEndpoint
    {
        public abstract override Task<IActionResult> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class AsyncDeleteEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>, IDeleteEndpoint
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class DeleteEndpoint<TRequest> : EndpointWithRequest<TRequest>, IDeleteEndpoint
    {
        public abstract override IActionResult Handle(TRequest model);
    }

    public abstract class DeleteEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>, IDeleteEndpoint
    {
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}