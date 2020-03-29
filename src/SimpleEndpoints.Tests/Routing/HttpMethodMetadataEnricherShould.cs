using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Shouldly;
using SimpleEndpoints.Enrichers;
using SimpleEndpoints.VerbScoped;
using Xunit;

namespace SimpleEndpoints.Tests.Routing
{
    public class HttpMethodMetadataEnricherShould
    {
        [Fact]
        public void MapHttpDeleteFromAsyncGetEndpointBaseClass()
        {
            //Arrange
            var enricher = new HttpMethodEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration()),
                NullLogger<HttpMethodEndpointMetadataEnricher>.Instance);
            var controller = CreateController(typeof(TestDeleteEndpoint));

            //Act
            enricher.Enrich(controller, c => { });

            //Assert
            controller.Selectors[0].EndpointMetadata.Count.ShouldBe(1);
            controller.Selectors[0].EndpointMetadata.First().ShouldBeOfType<HttpMethodMetadata>();
            controller.Selectors[0].EndpointMetadata.OfType<HttpMethodMetadata>().First().HttpMethods
                .ShouldBe(new[] {"DELETE"});
        }

        [Fact]
        public void MapHttpGetFromAsyncGetEndpointBaseClass()
        {
            //Arrange
            var enricher = new HttpMethodEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration()),
                NullLogger<HttpMethodEndpointMetadataEnricher>.Instance);
            var controller = CreateController(typeof(TestGetEndpoint));

            //Act
            enricher.Enrich(controller, c => { });

            //Assert
            controller.Selectors[0].EndpointMetadata.Count.ShouldBe(1);
            controller.Selectors[0].EndpointMetadata.First().ShouldBeOfType<HttpMethodMetadata>();
            controller.Selectors[0].EndpointMetadata.OfType<HttpMethodMetadata>().First().HttpMethods
                .ShouldBe(new[] {"GET"});
        }

        [Fact]
        public void MapHttpPostFromAsyncGetEndpointBaseClass()
        {
            //Arrange
            var enricher = new HttpMethodEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration()),
                NullLogger<HttpMethodEndpointMetadataEnricher>.Instance);
            var controller = CreateController(typeof(TestPostEndpoint));

            //Act
            enricher.Enrich(controller, c => { });

            //Assert
            controller.Selectors[0].EndpointMetadata.Count.ShouldBe(1);
            controller.Selectors[0].EndpointMetadata.First().ShouldBeOfType<HttpMethodMetadata>();
            controller.Selectors[0].EndpointMetadata.OfType<HttpMethodMetadata>().First().HttpMethods
                .ShouldBe(new[] {"POST"});
        }

        [Fact]
        public void MapHttpPutFromAsyncGetEndpointBaseClass()
        {
            //Arrange
            var enricher = new HttpMethodEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration()),
                NullLogger<HttpMethodEndpointMetadataEnricher>.Instance);
            var controller = CreateController(typeof(TestPutEndpoint));

            //Act
            enricher.Enrich(controller, c => { });

            //Assert
            controller.Selectors[0].EndpointMetadata.Count.ShouldBe(1);
            controller.Selectors[0].EndpointMetadata.First().ShouldBeOfType<HttpMethodMetadata>();
            controller.Selectors[0].EndpointMetadata.OfType<HttpMethodMetadata>().First().HttpMethods
                .ShouldBe(new[] {"PUT"});
        }

        private static ControllerModel CreateController(Type typeEndpoint)
        {
            var classAttributes = Attribute.GetCustomAttributes(typeEndpoint);
            return new ControllerModel(typeEndpoint.GetTypeInfo(), classAttributes)
            {
                Selectors = {new SelectorModel {EndpointMetadata = { }}}
            };
        }

        public class TestDeleteEndpoint : AsyncDeleteEndpoint<int>
        {
            public override async Task<IActionResult>
                HandleAsync(int model, CancellationToken cancellationToken = default) =>
                await Task.FromResult(Ok(new {Success = true}));
        }

        public class TestGetEndpoint : AsyncGetEndpoint
        {
            public override async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default) =>
                await Task.FromResult(Ok(new {Success = true}));
        }

        public class TestPostEndpoint : AsyncPostEndpoint<int>
        {
            public override async Task<IActionResult>
                HandleAsync(int model, CancellationToken cancellationToken = default) =>
                await Task.FromResult(Ok(new {Success = true}));
        }

        public class TestPutEndpoint : AsyncPutEndpoint<int>
        {
            public override async Task<IActionResult>
                HandleAsync(int model, CancellationToken cancellationToken = default) =>
                await Task.FromResult(Ok(new {Success = true}));
        }
    }
}