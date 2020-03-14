using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncGetEndpointController : AsyncEndpointController
    {
        [HttpGet]
        public virtual Task<IActionResult> Get(CancellationToken cancellationToken = default) =>
            HandleAsync(cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncGetEndpointControllerWithRequest<TRequest> : AsyncEndpointControllerWithRequest<TRequest>
    {
        [HttpGet]
        public virtual Task<IActionResult> Get(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncGetEndpointController<TResponse> : AsyncEndpointController<TResponse>
    {
        [HttpGet]
        public virtual Task<ActionResult<TResponse>> Get(CancellationToken cancellationToken = default) =>
            HandleAsync(cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncGetEndpointController<TRequest, TResponse> : AsyncEndpointController<TRequest, TResponse>
    {
        [HttpGet]
        public virtual Task<ActionResult<TResponse>> Get(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class GetEndpointController : EndpointController
    {
        [HttpGet]
        public virtual IActionResult Get() =>
            Handle();
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class GetEndpointController<TResponse> : EndpointController<TResponse>
    {
        [HttpGet]
        public virtual ActionResult<TResponse> Get() =>
            Handle();
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class GetEndpointControllerWithRequest<TRequest> : EndpointControllerWithRequest<TRequest>
    {
        [HttpGet]
        public virtual IActionResult Get(TRequest model) =>
            Handle(model);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class GetEndpointController<TRequest, TResponse> : EndpointController<TRequest, TResponse>
    {
        [HttpGet]
        public virtual ActionResult<TResponse> Get(TRequest model) =>
            Handle(model);
    }

}
