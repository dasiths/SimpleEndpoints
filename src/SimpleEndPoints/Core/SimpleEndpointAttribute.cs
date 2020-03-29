using System;

namespace SimpleEndpoints.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SimpleEndpointAttribute: Attribute
    {
        public readonly string HttpVerb;

        public SimpleEndpointAttribute(HttpVerb httpVerb)
        {
            HttpVerb = httpVerb.ToString().ToUpper();
        }
    }
}
