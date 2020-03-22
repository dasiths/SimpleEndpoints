using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleEndpoints.Core
{
    public sealed class SimpleEndpointAttribute: Attribute
    {
        public readonly string HttpVerb;

        public SimpleEndpointAttribute(string httpVerb)
        {
            HttpVerb = httpVerb;
        }
    }
}
