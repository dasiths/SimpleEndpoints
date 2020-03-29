using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Shouldly;
using SimpleEndpoints.Enrichers;
using SimpleEndpoints.VerbScoped;
using Xunit;

namespace SimpleEndpoints.Tests.Routing
{
    public class RouteMetadataEnricherShould
    {
        [Fact]
        public void MapRoute_ReplacingPlaceholderWithEndpointName()
        {
            //Arrange
            var enricher = new RouteEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration().WithRoutePrefix("api")),
                NullLogger<RouteEndpointMetadataEnricher>.Instance);
            var classAttributes = Attribute.GetCustomAttributes(typeof(TestEndpoint));
            var controller = CreateControllerModel(classAttributes.OfType<RouteAttribute>().First().Template);

            //Act
            enricher.Enrich(controller, c => { });

            //Assert
            controller.Selectors[0].AttributeRouteModel.Template.ShouldBe("api/Test");
        }

        [Fact]
        public void MapRoute_HonouringRouteAttribute()
        {
            //Arrange
            var enricher = new RouteEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration()),
                NullLogger<RouteEndpointMetadataEnricher>.Instance);
            var controller = CreateControllerModel("my-route");

            //Act
            enricher.Enrich(controller, c => {});

            //Assert
            controller.Selectors[0].AttributeRouteModel.Template.ShouldBe("my-route");
        }

        private static ControllerModel CreateControllerModel(string routeTemplate)
        {
            var classAttributes = Attribute.GetCustomAttributes(typeof(TestEndpoint));

            var controller = new ControllerModel(typeof(TestEndpoint).GetTypeInfo(), classAttributes)
            {
                Selectors =
                {
                    new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel {Template = routeTemplate},
                        EndpointMetadata = { }
                    }
                },
                ControllerName = nameof(TestEndpoint)
            };
            return controller;
        }

        public class TestEndpoint : AsyncGetEndpoint
        {
            public override async Task<IActionResult> HandleAsync(CancellationToken cancellationToken = default)
            {
                return await Task.FromResult(Ok(new {Success = true}));
            }
        }
    }
}