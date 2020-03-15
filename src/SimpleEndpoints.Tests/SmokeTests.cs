using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Shouldly;
using SimpleEndpoints.Example.Endpoints.WeatherForecast;
using SimpleEndpoints.Tests.Shared;
using Xunit;

namespace SimpleEndpoints.Tests
{
    public class SmokeTests
    {
        private readonly WebAppFactory _factory;

        public SmokeTests()
        {
            _factory = new WebAppFactory();
        }

        [Fact]
        public async Task When_Calling_Basic_Endpoint_It_Returns_Payload()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/weatherforecast");

            // Assert
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            var result = await response.Content.ReadAsStringAsync();
            var resultModel = JsonSerializer.Deserialize<List<WeatherForecast>>(result);
            resultModel.Count.ShouldBe(5);
        }
    }
}
