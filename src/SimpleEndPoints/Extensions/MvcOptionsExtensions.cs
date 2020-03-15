using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.Conventions;

namespace SimpleEndpoints.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static void AddEndpointRoutingConvention(this MvcOptions options)
        {
            options.Conventions.Add(new EndpointRoutingConvention());
        }
    }
}
