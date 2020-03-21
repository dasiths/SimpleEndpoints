using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Example.Endpoints.SimpleMessage
{
    public class SimpleMessageEndpoint : AsyncPostEndpoint<SimpleMessage, SimpleResponse>
    {
        // Here we force it to use a custom Route
        [Route("custom")]
        public override async Task<ActionResult<SimpleResponse>> HandleAsync(SimpleMessage requestModel, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(new SimpleResponse {Message = "Hello " + requestModel.Message});
        }
    }
}
