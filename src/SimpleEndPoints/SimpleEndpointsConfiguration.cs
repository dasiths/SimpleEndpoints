using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleEndpoints.Core;

namespace SimpleEndpoints
{
    public class SimpleEndpointsConfiguration
    {
        public const int DefaultRouteEndpointMetadataEnricherOrder = 100;
        public const int DefaultHttpMethodEndpointMetadataEnricherOrder = 100;
        private const int DefaultEndpointApiDescriptionProviderOrder = 100;

        public string RoutePrefix { get; private set; }
        public string EndpointNamingConventionEnding { get; private set; }
        public int EndpointApiDescriptionProviderOrder { get; private set; }
        public int RouteEndpointMetadataEnricherOrder { get; private set; }
        public int HttpMethodEndpointMetadataEnricherOrder { get; private set; }
        public ReadOnlyDictionary<string, string> RouteTokenDictionary { get; private set; }

        public SimpleEndpointsConfiguration()
        {
            RoutePrefix = string.Empty;
            EndpointNamingConventionEnding = nameof(Endpoint);
            RouteEndpointMetadataEnricherOrder = DefaultRouteEndpointMetadataEnricherOrder;
            HttpMethodEndpointMetadataEnricherOrder = DefaultHttpMethodEndpointMetadataEnricherOrder;
            EndpointApiDescriptionProviderOrder = DefaultEndpointApiDescriptionProviderOrder;
            RouteTokenDictionary = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
        }

        public SimpleEndpointsConfiguration WithRoutePrefix(string prefix)
        {
            RoutePrefix = prefix;
            return this;
        }

        public SimpleEndpointsConfiguration WithEndpointNamingConventionEndingWith(string endpointNamingConventionEnding)
        {
            EndpointNamingConventionEnding = endpointNamingConventionEnding;
            return this;
        }

        public SimpleEndpointsConfiguration WithRouteTokens(Dictionary<string, string> tokens)
        {
            RouteTokenDictionary = new ReadOnlyDictionary<string, string>(tokens);
            return this;
        }

        public SimpleEndpointsConfiguration WithEndpointApiDescriptionProviderOrder(int order)
        {
            EndpointApiDescriptionProviderOrder = order;
            return this;
        }

        public SimpleEndpointsConfiguration WithRouteEndpointMetadataEnricherOrder(int order)
        {
            RouteEndpointMetadataEnricherOrder = order;
            return this;
        }

        public SimpleEndpointsConfiguration WithHttpMethodEndpointMetadataEnricherOrder(int order)
        {
            HttpMethodEndpointMetadataEnricherOrder = order;
            return this;
        }
    }
}