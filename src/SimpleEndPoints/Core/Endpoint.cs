using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleEndpoints.Core
{
    [ApiController]
    [Route("[endpoint]")]
    public abstract class AsyncEndpoint : ControllerBase
    {
        public abstract Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class AsyncEndpointWithRequest<TRequest> : ControllerBase
    {
        public abstract Task<IActionResult> HandleAsync(TRequest requestModel, CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class AsyncEndpoint<TResponse> : ControllerBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class AsyncEndpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest requestModel, CancellationToken cancellationToken = default);
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class Endpoint : ControllerBase
    {
        public abstract IActionResult Handle();
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class EndpointWithRequest<TRequest> : ControllerBase
    {
        public abstract IActionResult Handle(TRequest requestModel);
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class Endpoint<TResponse> : ControllerBase
    {
        public abstract ActionResult<TResponse> Handle();
    }

    [ApiController]
    [Route("[endpoint]")]
    public abstract class Endpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract ActionResult<TResponse> Handle(TRequest requestModel);
    }
}
