using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Example.Endpoints.Basic
{
    public class GreetingRequest
    {
        public string Name { get; set; }
    }

    //As we are extending AsyncEndpoint we must specify the [HttpGet] attribute   
    public class GreetingAsyncEndpoint : AsyncEndpoint<GreetingRequest, string>
    {
        [HttpGet]
        public override async Task<ActionResult<string>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult($"Hello {requestModel.Name}");
        }
    }
    
    //As we now extend AsyncGetEndpoint we know that the action is a Get so it can be omitted
    public class GreetingAsyncGetEndpoint : AsyncGetEndpoint<GreetingRequest, string>
    {
        public override async Task<ActionResult<string>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult($"Hello {requestModel.Name}");
        }
    }
    
    //We can also overwrite the route segement - /api/GreetingAsyncGetWithRoute/get
    public class GreetingAsyncGetWithRouteEndpoint : AsyncGetEndpoint<GreetingRequest, string>
    {
        [Route("get")]
        public override async Task<ActionResult<string>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult($"Hello {requestModel.Name}");
        }
    }
    
    //Or we can also overwrite the route root segment - /api/greeting/get
    [Route("api/greeting")]
    public class GreetingAsyncGetWithRouteEndpoint2 : AsyncGetEndpoint<GreetingRequest, string>
    {
        [Route("get")]
        public override async Task<ActionResult<string>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult($"Hello {requestModel.Name}");
        }
    }
}
