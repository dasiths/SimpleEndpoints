using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoint.Core;

namespace SimpleEndpoint.VerbScoped
{
    [Controller]
    [Route("[controller]")]
    public abstract class AsyncDeleteEndpointController<TRequest> : AsyncEmptyResponseEndpointController<TRequest>
    {
        [HttpDelete]
        public virtual Task<IActionResult> Delete(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class AsyncDeleteEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpDelete]
        public virtual Task<ActionResult<TResponse>> Delete(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class DeleteEndpointController<TRequest> : EmptyResponseEndpointController<TRequest>
    {
        [HttpDelete]
        public virtual IActionResult Delete(TRequest model) =>
            Handle(model);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class DeleteEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpDelete]
        public virtual ActionResult<TResponse> Delete(TRequest model) =>
            Handle(model);
    }

}