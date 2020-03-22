using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using SimpleEndpoints.Example.Endpoints.Basic;
using SimpleEndpoints.Tests.Shared;
using Xunit;

namespace SimpleEndpoints.Tests.Endpoints.Basic
{
    public class GreetingEndpointTestsShould
    {
        private readonly WebAppFactory _factory;

        public GreetingEndpointTestsShould()
        {
            _factory = new WebAppFactory();
        }

        [Fact]
        public async Task GreetingAsyncEndpoint()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/GreetingAsync?name=peterparker");
            var responseContent = JsonConvert.DeserializeObject<GreetingResponse>(await response.Content.ReadAsStringAsync());
            
            // Assert
            responseContent.Greeting.ShouldBe("Hello peterparker");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GreetingAsyncGetEndpoint()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/GreetingAsyncGet?name=peterparker");
            var responseContent = JsonConvert.DeserializeObject<GreetingResponse>(await response.Content.ReadAsStringAsync());
            
            // Assert
            responseContent.Greeting.ShouldBe("Hello peterparker");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GreetingAsyncGetWithRouteEndpoint()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/GreetingAsyncGetWithRoute/get?name=peterparker");
            var responseContent = JsonConvert.DeserializeObject<GreetingResponse>(await response.Content.ReadAsStringAsync());
            
            // Assert
            responseContent.Greeting.ShouldBe("Hello peterparker");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GreetingAsyncGetWithContradictoryRouteEndpoint()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/greeting/get?name=peterparker");
            var responseContent = JsonConvert.DeserializeObject<GreetingResponse>(await response.Content.ReadAsStringAsync());
            
            // Assert
            responseContent.Greeting.ShouldBe("Hello peterparker");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
    }
}
