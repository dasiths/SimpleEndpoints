using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    
    public abstract class AsyncPutEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        [HttpPut]
        public virtual Task<IActionResult> Put(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }
    
    public abstract class AsyncPutEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        [HttpPut]
        public virtual Task<ActionResult<TResponse>> Put(TRequest model, CancellationToken cancellationToken = default) =>
            HandleAsync(model, cancellationToken);
    }
    
    public abstract class PutEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        [HttpPut]
        public virtual IActionResult Put(TRequest model) =>
            Handle(model);
    }
    
    public abstract class PutEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        [HttpPut]
        public virtual ActionResult<TResponse> Put(TRequest model) =>
            Handle(model);
    }
}
