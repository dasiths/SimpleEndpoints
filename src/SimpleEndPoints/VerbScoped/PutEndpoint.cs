using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.VerbScoped
{
    
    public abstract class AsyncPutEndpoint<TRequest> : AsyncEndpointWithRequest<TRequest>
    {
        [HttpPut]
        public abstract override Task<IActionResult> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }
    
    public abstract class AsyncPutEndpoint<TRequest, TResponse> : AsyncEndpoint<TRequest, TResponse>
    {
        [HttpPut]
        public abstract override Task<ActionResult<TResponse>> HandleAsync(TRequest model,
            CancellationToken cancellationToken = default);
    }
    
    public abstract class PutEndpoint<TRequest> : EndpointWithRequest<TRequest>
    {
        [HttpPut]
        public abstract override IActionResult Handle(TRequest model);
    }
    
    public abstract class PutEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    {
        [HttpPut]
        public abstract override ActionResult<TResponse> Handle(TRequest model);
    }
}
