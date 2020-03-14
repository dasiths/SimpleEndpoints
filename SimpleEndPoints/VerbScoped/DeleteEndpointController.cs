using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncDeleteEndpointController<TRequest> : AsyncEndpointControllerWithRequest<TRequest>
    {
        [HttpDelete]
        public virtual Task<IActionResult> Delete(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncDeleteEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpDelete]
        public virtual Task<ActionResult<TResponse>> Delete(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class DeleteEndpointController<TRequest> : EndpointControllerWithRequest<TRequest>
    {
        [HttpDelete]
        public virtual IActionResult Delete(TRequest model) =>
            Handle(model);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class DeleteEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpDelete]
        public virtual ActionResult<TResponse> Delete(TRequest model) =>
            Handle(model);
    }

}