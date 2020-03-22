using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace SimpleEndpoints.Swashbuckle
{
    public class SwashbuckleCompatibilityConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var action in application.Controllers.SelectMany(c => c.Actions))
            {
                if (action.Selectors.Any() && 
                    action.Selectors[0].ActionConstraints.Count == 0 && 
                    action.ApiExplorer.IsVisible.GetValueOrDefault(false))
                {
                    var attribute = action.Attributes.FirstOrDefault(a => a is HttpMethodAttribute);

                    if (attribute is HttpMethodAttribute item)
                    {
                        var o = new HttpMethodActionConstraint(item.HttpMethods);
                        action.Selectors[0].ActionConstraints.Add(o);
                    }
                }
            }
        }
    }
}
