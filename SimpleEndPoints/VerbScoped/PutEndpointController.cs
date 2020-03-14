using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncPutEndpointController<TRequest> : AsyncEndpointControllerWithRequest<TRequest>
    {
        [HttpPut]
        public virtual Task<IActionResult> Put(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncPutEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpPut]
        public virtual Task<ActionResult<TResponse>> Put(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class PutEndpointController<TRequest> : EndpointControllerWithRequest<TRequest>
    {
        [HttpPut]
        public virtual IActionResult Put(TRequest model) =>
            Handle(model);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class PutEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpPut]
        public virtual ActionResult<TResponse> Put(TRequest model) =>
            Handle(model);
    }

}
