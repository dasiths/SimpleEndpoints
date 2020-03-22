namespace SimpleEndpoints
{
    public class SimpleEndpointsConfiguration
    {
        public string RoutePrefix { get; private set; }
        public string EndpointReplacementToken { get; private set; }

        public SimpleEndpointsConfiguration()
        {
            RoutePrefix = "api";
            EndpointReplacementToken = "Endpoint";
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