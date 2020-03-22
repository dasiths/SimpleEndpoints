using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Shouldly;
using SimpleEndpoints.Conventions;
using SimpleEndpoints.VerbScoped;
using Xunit;

namespace SimpleEndpoints.Tests.Conventions
{
    public class HttpMethodMetadataMutatorShould
    {
        [Fact]
        public void MapHttpDeleteFromAsyncGetEndpointBaseClass()
        {
            //Arrange
            var mutator = new HttpMethodMetadataMutator();
            var controller = CreateController(typeof(TestDeleteEndpoint));

            //Act
            mutator.Mutate(controller, new SimpleEndpointsConfiguration());

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
            var mutator = new HttpMethodMetadataMutator();
            var controller = CreateController(typeof(TestGetEndpoint));

            //Act
            mutator.Mutate(controller, new SimpleEndpointsConfiguration());

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
            var mutator = new HttpMethodMetadataMutator();
            var controller = CreateController(typeof(TestPostEndpoint));

            //Act
            mutator.Mutate(controller, new SimpleEndpointsConfiguration());

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
            var mutator = new HttpMethodMetadataMutator();
            var controller = CreateController(typeof(TestPutEndpoint));

            //Act
            mutator.Mutate(controller, new SimpleEndpointsConfiguration());

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