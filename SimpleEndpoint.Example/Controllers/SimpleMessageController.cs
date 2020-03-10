using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoint.Example.Models;
using SimpleEndpoint.VerbScoped;

namespace SimpleEndpoint.Example.Controllers
{
    // Use 'ApiController' attribute when you need advanced convention based model binding
    // See more here: https://kimsereyblog.blogspot.com/2018/08/apicontroller-attribute-in-asp-net-core.html
    [ApiController]
    public class SimpleMessageController: AsyncPostEndpointController<SimpleMessage, SimpleResponse>
    {
        [ProducesResponseType(200)] // Add extra method attributes by overriding the method
        public override Task<ActionResult<SimpleResponse>> Post(SimpleMessage model, CancellationToken cancellationToken = default)
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
