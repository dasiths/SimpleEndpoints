using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    public interface IPostEndpoint
    {
    }
    
    public abstract class AsyncPostEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>, IPostEndpoint
    {
        public abstract override Task<IActionResult> HandleAsync(TRequest model, CancellationToken cancellationToken = default);
    }

    public abstract class AsyncPostEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>, IPostEndpoint
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    public abstract class PostEndpoint<TRequest> : EndpointWithRequest<TRequest>, IPostEndpoint
    {
        public abstract override IActionResult Handle(TRequest model);
    }

    public abstract class PostEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>, IPostEndpoint
    {
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}
