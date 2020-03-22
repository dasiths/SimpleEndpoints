using Microsoft.AspNetCore.Mvc;

namespace SimpleEndpoints.Swashbuckle
{
    public static class MvcOptionsExtensions
    {
        public static void AddSwashbuckleCompatibilityForSimpleEndpoints(this MvcOptions options)
        {
            options.Conventions.Add(new SwashbuckleCompatibilityConvention());
        }
    }
}
