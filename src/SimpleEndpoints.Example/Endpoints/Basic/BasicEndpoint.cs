using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Core;

namespace SimpleEndpoints.Example.Endpoints.Basic
{
    public class BasicEndpoint: AsyncEndpoint<GreetingRequest, string>
    {
        [HttpGet]
        public override async Task<ActionResult<string>> HandleAsync([FromQuery]GreetingRequest requestModel, CancellationToken cancellationToken = default)
        {
            return $"Hello {requestModel.Name}";
        }
    }

    public class GreetingRequest
    {
        public string Name { get; set; }
    }
}
