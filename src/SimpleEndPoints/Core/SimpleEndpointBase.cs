using Microsoft.AspNetCore.Mvc;

namespace SimpleEndpoints.Core
{
    [ApiController]
    [Route("[endpoint]")]
    public abstract class SimpleEndpointBase : ControllerBase
    {
    }
}