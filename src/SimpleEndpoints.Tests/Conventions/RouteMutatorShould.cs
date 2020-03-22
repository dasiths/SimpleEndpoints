using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Shouldly;
using SimpleEndpoints.Conventions;
using SimpleEndpoints.VerbScoped;
using Xunit;

namespace SimpleEndpoints.Tests.Conventions
{
    public class RouteMutatorShould
    {
        [Fact]
        public void MapRoute_ReplacingPlaceholderWithEndpointName()
        {
            //Arrange
            var mutator = new RouteMutator();
            var classAttributes = Attribute.GetCustomAttributes(typeof(TestEndpoint));
            var controller = CreateControllerModel(classAttributes.OfType<RouteAttribute>().First().Template);

            //Act
            mutator.Mutate(controller, new SimpleEndpointsConfiguration());

            //Assert
            controller.Selectors[0].AttributeRouteModel.Template.ShouldBe("api/Test");
        }

        [Fact]
        public void MapRoute_HonouringRouteAttribute()
        {
            //Arrange
            var mutator = new RouteMutator();
            var controller = CreateControllerModel("my-route");

            //Act
            mutator.Mutate(controller, new SimpleEndpointsConfiguration());

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