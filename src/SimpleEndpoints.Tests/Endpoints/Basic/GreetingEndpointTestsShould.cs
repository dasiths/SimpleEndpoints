using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using SimpleEndpoints.Example.Endpoints.Greeting;
using SimpleEndpoints.Tests.Shared;
using Xunit;

namespace SimpleEndpoints.Tests.Endpoints.Basic
{
    public class GreetingEndpointTestsShould
    {
        private readonly WebAppFactory _factory;
        private readonly string routePrefix = "api/v1/";

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
            var response = await client.GetAsync($"{routePrefix}GreetingAsync?name=peterparker");
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
            var response = await client.GetAsync($"{routePrefix}GreetingAsyncGet?name=peterparker");
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
            var response = await client.GetAsync($"{routePrefix}GreetingAsyncGetWithRoute/get?name=peterparker");
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

        [Fact]
        public async Task GreetingAsyncPostWithContradictoryRouteAndHttpMethodEndpoint()
        {
            // Arrange
            using var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync($"/api/greeting/{nameof(SimpleEndpoints)}.{nameof(Example)}?name=peterparker", new StringContent(""));
            var responseContent = JsonConvert.DeserializeObject<GreetingResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            responseContent.Greeting.ShouldBe("Hello peterparker");
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
    }
}
