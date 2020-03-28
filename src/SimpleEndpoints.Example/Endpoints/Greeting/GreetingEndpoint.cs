using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Example.Endpoints.Greeting
{
    public class GreetingRequest
    {
        public string Name { get; set; }
    }
    
    public class GreetingResponse
    {
        public string Greeting { get; set; }
    }
    
    //As we are extending AsyncEndpoint we must specify the [HttpGet] attribute   
    public class GreetingAsyncEndpoint : AsyncEndpoint<GreetingRequest, GreetingResponse>
    {
        [HttpGet]
        public override async Task<ActionResult<GreetingResponse>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return Ok(new GreetingResponse {Greeting = await Task.FromResult($"Hello {requestModel.Name}")});
        }
    }
    
    //As we now extend AsyncGetEndpoint we know that the action is a Get so it can be omitted
    public class GreetingAsyncGetEndpoint : AsyncGetEndpoint<GreetingRequest, GreetingResponse>
    {
        public override async Task<ActionResult<GreetingResponse>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return Ok(new GreetingResponse {Greeting = await Task.FromResult($"Hello {requestModel.Name}")});
        }
    }
    
    //We can also overwrite the route segement - /api/GreetingAsyncGetWithRoute/get
    public class GreetingAsyncGetWithRouteEndpoint : AsyncGetEndpoint<GreetingRequest, GreetingResponse>
    {
        [Route("get")]
        public override async Task<ActionResult<GreetingResponse>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return Ok(new GreetingResponse {Greeting = await Task.FromResult($"Hello {requestModel.Name}")});
        }
    }

    //Or we can also overwrite the route root segment - /api/greeting/get
    [Route("api/greeting")]
    public class GreetingAsyncGetWithRouteEndpoint2 : AsyncGetEndpoint<GreetingRequest, GreetingResponse>
    {
        [Route("get")]
        public override async Task<ActionResult<GreetingResponse>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return Ok(new GreetingResponse {Greeting = await Task.FromResult($"Hello {requestModel.Name}")});
        }
    }

    //We can extend AsyncGetEndpoint and [HttpPost], we now expect a POST
    [Route("api/greeting/[assembly-name]")]
    public class GreetingAsyncPostWithContradictoryRouteAndHttpMethodEndpoint : AsyncGetEndpoint<GreetingRequest, GreetingResponse>
    {
        [HttpPost]
        public override async Task<ActionResult<GreetingResponse>> HandleAsync([FromQuery] GreetingRequest requestModel,
            CancellationToken cancellationToken = default)
        {
            return Ok(new GreetingResponse {Greeting = await Task.FromResult($"Hello {requestModel.Name}")});
        }
    }
}
