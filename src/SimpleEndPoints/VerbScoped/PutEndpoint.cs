using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{

    [SimpleEndpoint("PUT")]
    public abstract class AsyncPutEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        public abstract override Task<IActionResult> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("PUT")]
    public abstract class AsyncPutEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("PUT")]
    public abstract class PutEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        public abstract override IActionResult Handle(TRequest model);
    }

    [SimpleEndpoint("PUT")]
    public abstract class PutEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}