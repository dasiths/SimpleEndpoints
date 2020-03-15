using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{

    public abstract class AsyncDeleteEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        [HttpDelete]
        public virtual Task<IActionResult> Delete(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    public abstract class AsyncDeleteEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        [HttpDelete]
        public virtual Task<ActionResult<TResponse>> Delete(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    public abstract class DeleteEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        [HttpDelete]
        public virtual IActionResult Delete(TRequest model) =>
            Handle(model);
    }

    public abstract class DeleteEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        [HttpDelete]
        public virtual ActionResult<TResponse> Delete(TRequest model) =>
            Handle(model);
    }
}