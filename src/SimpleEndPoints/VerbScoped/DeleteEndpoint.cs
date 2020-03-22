using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    [SimpleEndpoint("DELETE")]
    public abstract class AsyncDeleteEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        public abstract override Task<IActionResult> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("DELETE")]
    public abstract class AsyncDeleteEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }

    [SimpleEndpoint("DELETE")]
    public abstract class DeleteEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        public abstract override IActionResult Handle(TRequest model);
    }

    [SimpleEndpoint("DELETE")]
    public abstract class DeleteEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}