namespace SimpleEndpoints
{
    public class SimpleEndpointsConfiguration
    {
        public const int DefaultRouteEndpointMetadataEnricherOrder = 100;
        public const int DefaultHttpMethodEndpointMetadataEnricherOrder = 100;
        private const int DefaultEndpointApiDescriptionProviderOrder = 100;

        public string RoutePrefix { get; private set; }
        public string EndpointReplacementToken { get; private set; }
        public int EndpointApiDescriptionProviderOrder { get; private set; }
        public int RouteEndpointMetadataEnricherOrder { get; private set; }
        public int HttpMethodEndpointMetadataEnricherOrder { get; private set; }

        public SimpleEndpointsConfiguration()
        {
            RoutePrefix = "";
            EndpointReplacementToken = "Endpoint";
            RouteEndpointMetadataEnricherOrder = DefaultRouteEndpointMetadataEnricherOrder;
            HttpMethodEndpointMetadataEnricherOrder = DefaultHttpMethodEndpointMetadataEnricherOrder;
            EndpointApiDescriptionProviderOrder = DefaultEndpointApiDescriptionProviderOrder;
        }

        public SimpleEndpointsConfiguration WithRoutePrefix(string prefix)
        {
            RoutePrefix = prefix;
            return this;
        }

        public SimpleEndpointsConfiguration WithEndpointNamingConvention(string endpointReplacementToken)
        {
            EndpointReplacementToken = endpointReplacementToken;
            return this;
        }
    }
}