using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{

    public abstract class AsyncPostEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        [HttpPost]
        public virtual Task<IActionResult> Post(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    public abstract class AsyncPostEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        [HttpPost]
        public virtual Task<ActionResult<TResponse>> Post(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    public abstract class PostEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        [HttpPost]
        public virtual IActionResult Post(TRequest model) =>
            Handle(model);
    }

    public abstract class PostEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        [HttpPost]
        public virtual ActionResult<TResponse> Post(TRequest model) =>
            Handle(model);
    }
}
