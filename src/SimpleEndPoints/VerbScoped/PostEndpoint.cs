using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{

    [SimpleEndpoint("POST")]
    public abstract class AsyncPostEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        public abstract override Task<IActionResult> HandleAsync(TRequest model, CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("POST")]
    public abstract class AsyncPostEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("POST")]
    public abstract class PostEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        public abstract override IActionResult Handle(TRequest model);
    }

    [SimpleEndpoint("POST")]
    public abstract class PostEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}
