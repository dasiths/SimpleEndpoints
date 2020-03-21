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
            services.AddControllers(options =>
            {
                //Register simple endpoints & configure global route and naming convention
                options.WithSimpleEndpoints(endpointsConfiguration =>
                    endpointsConfiguration
                        .WithRoutePrefix("api")
                        .WithEndpointNamingConvention("Endpoint"));
            });

            //This is just so we can use Swagger and could be named accordingly
            services.AddSimpleEndpointRouting();

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Simple endpoints", Version = "v1"}));
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
