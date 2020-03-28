using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleEndpoints.Extensions;

namespace SimpleEndpoints.Example
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenDictionary = new Dictionary<string, string>()
            {
                {"assembly-name", Assembly.GetExecutingAssembly().GetName(true).Name}
            };

            services.AddControllers();
            services.AddSimpleEndpointsRouting(options => options
                .WithRouteTokens(tokenDictionary)
                .WithRoutePrefix("api/v1")
            );

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple endpoints", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(options => options.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple endpoints V1"));
        }
    }
}
