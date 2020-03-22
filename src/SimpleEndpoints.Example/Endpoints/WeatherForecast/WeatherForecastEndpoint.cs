using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleEndpoints.VerbScoped;

namespace SimpleEndpoints.Example.Endpoints.WeatherForecast
{
    public class WeatherForecastEndpoint : AsyncGetEndpoint<List<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override async Task<ActionResult<List<WeatherForecast>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var rng = new Random();
            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList());
        }
    }
}
