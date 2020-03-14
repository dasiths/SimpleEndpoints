using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Example.Models;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Example.Controllers
{
    public class SimpleMessageController : AsyncPostEndpointController<SimpleMessage, SimpleResponse>
    {
        // Override if you want to change the default behaviour such as model binding
        // Here we force it to use the [FromQuery] model binding instead of the default [FromBody] and define a custom Route
        [HttpPost("custom")]
        public override Task<ActionResult<SimpleResponse>> Post([FromQuery]SimpleMessage model, CancellationToken cancellationToken = default)
        {
            return base.Post(model, cancellationToken);
        }

        protected override async Task<ActionResult<SimpleResponse>> HandleAsync(SimpleMessage requestModel, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new SimpleResponse()
            {
                Message = "Hello " + requestModel.Message
            });
        }
    }
}
