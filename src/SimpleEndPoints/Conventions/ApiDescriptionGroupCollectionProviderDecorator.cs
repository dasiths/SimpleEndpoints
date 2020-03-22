using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Conventions
{
    internal class ApiDescriptionGroupCollectionProviderDecorator : IApiDescriptionGroupCollectionProvider
    {
        private readonly IApiDescriptionGroupCollectionProvider _inner;

        public ApiDescriptionGroupCollectionProviderDecorator(IApiDescriptionGroupCollectionProvider inner)
        {
            _inner = inner;
        }

        public ApiDescriptionGroupCollection ApiDescriptionGroups
        {
            get
            {
                foreach (var apiDescriptionGroup in _inner.ApiDescriptionGroups.Items)
                {
                    foreach (var apiDescription in apiDescriptionGroup.Items)
                    {
                        if (apiDescription.ActionDescriptor is ControllerActionDescriptor controller)
                        {
                            if (typeof(IDeleteEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "DELETE";
                            }
                            if (typeof(IGetEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "GET";
                            }
                            if (typeof(IPostEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "POST";
                            }
                            if (typeof(IPutEndpoint).IsAssignableFrom(controller.ControllerTypeInfo))
                            {
                                apiDescription.HttpMethod = "PUT";
                            }
                        }
                    }
                }

                return _inner.ApiDescriptionGroups;
            }
        }
    }
}