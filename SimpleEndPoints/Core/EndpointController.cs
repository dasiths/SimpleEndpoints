using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleEndpoints.Core
{
    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncEndpointController : ControllerBase
    {
        [NonAction]
        protected abstract Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncEndpointControllerWithRequest<TRequest> : ControllerBase
    {
        [NonAction]
        protected abstract Task<IActionResult> HandleAsync(TRequest requestModel, CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncEndpointController<TResponse> : ControllerBase
    {
        [NonAction]
        protected abstract Task<ActionResult<TResponse>> HandleAsync(CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class AsyncEndpointController<TRequest, TResponse> : ControllerBase
    {
        [NonAction]
        protected abstract Task<ActionResult<TResponse>> HandleAsync(TRequest requestModel, CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class EndpointController : ControllerBase
    {
        [NonAction]
        protected abstract IActionResult Handle();
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class EndpointControllerWithRequest<TRequest> : ControllerBase
    {
        [NonAction]
        protected abstract IActionResult Handle(TRequest requestModel);
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class EndpointController<TResponse> : ControllerBase
    {
        [NonAction]
        protected abstract ActionResult<TResponse> Handle();
    }

    [ApiController]
    [Route("[controller]")]
    public abstract class EndpointController<TRequest, TResponse> : ControllerBase
    {
        [NonAction]
        protected abstract ActionResult<TResponse> Handle(TRequest requestModel);
    }
}
