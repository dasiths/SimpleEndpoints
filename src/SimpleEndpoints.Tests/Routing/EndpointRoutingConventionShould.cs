using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Shouldly;
using SimpleEndpoints.Enrichers;
using SimpleEndpoints.Routing;
using SimpleEndpoints.VerbScoped;
using Xunit;

namespace SimpleEndpoints.Tests.Routing
{
    public class EndpointRoutingConventionShould
    {
        [Fact]
        public void MapConventions()
        {
            //Arrange
            var classAttributes = Attribute.GetCustomAttributes(typeof(TestEndpoint));
            var conventions = new EndpointRoutingConvention(new List<IEndpointMetadataEnricher>()
            {
                new RouteEndpointMetadataEnricher(Options.Create(new SimpleEndpointsConfiguration())),
                new HttpMethodEndpointMetadataEnricher()
            });

            var controller = new ControllerModel(typeof(TestEndpoint).GetTypeInfo(), classAttributes)
            {
                Selectors =
                {
                    new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel {Template = "route"},
                        EndpointMetadata = { }
                    }
                },
                ControllerName = nameof(TestEndpoint)
            };

            //Act
            conventions.Apply(new ApplicationModel {Controllers = {controller}});

            controller.Selectors[0].AttributeRouteModel.Template.ShouldBe("route");
            
            //Assert
            controller.Selectors[0].EndpointMetadata.Count.ShouldBe(1);
            controller.Selectors[0].EndpointMetadata.First().ShouldBeOfType<HttpMethodMetadata>();
            controller.Selectors[0].EndpointMetadata.OfType<HttpMethodMetadata>().First().HttpMethods
                .ShouldBe(new[] {"GET"});
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