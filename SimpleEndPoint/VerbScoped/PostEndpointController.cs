using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoint.Core;

namespace SimpleEndpoint.VerbScoped
{
    [Controller]
    [Route("[controller]")]
    public abstract class AsyncPostEndpointController<TRequest> : AsyncEmptyResponseEndpointController<TRequest>
    {
        [HttpPost]
        public virtual Task<IActionResult> Post(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class AsyncPostEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpPost]
        public virtual Task<ActionResult<TResponse>> Post(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class PostEndpointController<TRequest> : EmptyResponseEndpointController<TRequest>
    {
        [HttpPost]
        public virtual IActionResult Post(TRequest model) =>
            Handle(model);
    }

    [Controller]
    [Route("[controller]")]
    public abstract class PostEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpPost]
        public virtual ActionResult<TResponse> Post(TRequest model) =>
            Handle(model);
    }

}
