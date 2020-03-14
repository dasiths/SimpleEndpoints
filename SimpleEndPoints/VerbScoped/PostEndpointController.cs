using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncPostEndpointController<TRequest> : AsyncEndpointControllerWithRequest<TRequest>
    {
        [HttpPost]
        public virtual Task<IActionResult> Post(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncPostEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpPost]
        public virtual Task<ActionResult<TResponse>> Post(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class PostEndpointController<TRequest> : EndpointControllerWithRequest<TRequest>
    {
        [HttpPost]
        public virtual IActionResult Post(TRequest model) =>
            Handle(model);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class PostEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpPost]
        public virtual ActionResult<TResponse> Post(TRequest model) =>
            Handle(model);
    }

}
