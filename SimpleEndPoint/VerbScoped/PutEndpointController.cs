using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoint.Core;

namespace SimpleEndpoint.VerbScoped
{
    [Controller]
    [Route("[controller]")]
    public abstract class AsyncPutEndpointController<TRequest> : AsyncEmptyResponseEndpointController<TRequest>
    {
        [HttpPut]
        public virtual Task<IActionResult> Put(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class AsyncPutEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpPut]
        public virtual Task<ActionResult<TResponse>> Put(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class PutEndpointController<TRequest> : EmptyResponseEndpointController<TRequest>
    {
        [HttpPut]
        public virtual IActionResult Put(TRequest model) =>
            Handle(model);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class PutEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpPut]
        public virtual ActionResult<TResponse> Put(TRequest model) =>
            Handle(model);
    }

}
