using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SimpleEndpoints.Conventions
{
    public class EndpointRoutingConvention: IApplicationModelConvention
    {
        private const string EndpointString = "endpoint";

        public virtual void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                controller.Selectors[0].AttributeRouteModel.Template = controller
                    .Selectors[0]
                    .AttributeRouteModel
                    .Template
                    .Replace($"[{EndpointString}]", 
                        controller.ControllerName.Substring(0,
                        controller.ControllerName.Length - EndpointString.Length));
            }
        }
    }
}
