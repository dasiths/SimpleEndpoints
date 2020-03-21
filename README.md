# SimpleEndpoints ![Build Master](https://github.com/dasiths/SimpleEndpoints/workflows/Build%20Master/badge.svg?branch=master) [![NuGet](https://img.shields.io/nuget/v/SimpleEndpoints.svg)](https://www.nuget.org/packages/SimpleEndpoints)
 
 ### A simple, convention-based, endpoint per action pattern implementation for AspNetCore 3.0+
 
<img src="./assets/logo.png" alt="Logo" width="200"/>

## Motivation

The aim of this pattern is to get away from the bloated god controllers that have a million action methods and so many dependencies. By following the SimpleEndpoints pattern we keep the endpoint scoped to a small feature and lightweight which makes it easier to understand and manage. The aim is not to blur the line between the controllers and the domain layer. You can choose to dispatch the request to the domain from the endpoint or handle it in the endpoint itself. Make an informed choice based to the context.

More about it in the [blog post here](https://dasith.me/2020/03/21/simple-endpoints/)

## Getting Started

1. Install and reference the Nuget `SimpleEndpoints`

In the NuGet Package Manager Console, type:

```
    Install-Package SimpleEndpoints
```

2. Define your request and response models
```C#
    public class SimpleMessage
    {
        public string Message { get; set; }
    }

    public class SimpleResponse
    {
        public string Message { get; set; }
    }
```
3. Create your endpoint and implement your business logic (You can choose to handle in place or dispatch to a domain layer)
```C#
    public class SimpleMessageEndpoint : AsyncGetEndpoint<SimpleMessage, SimpleResponse>
    {
        public override async Task<ActionResult<SimpleResponse>> HandleAsync(SimpleMessage requestModel, CancellationToken cancellationToken = default)
        {	
	    // Handle in place or dispatch to the domain i.e. return await _someDomainService.HandleAsync(requestModel)
	
            return new SimpleResponse()
            {
                Message = "Hello " + requestModel.Message
            };
        }
    }
```
4. In the `ConfigureServices()` method in your `Startup.cs` add the following
```C#
    public void ConfigureServices(IServiceCollection services)
    {
        // Other services go here
		
        services.AddControllers((options) =>
        {
            options.AddEndpointRoutingConvention(); // This is required to translate endpoint names
        });
    }
```

5. Navigate to the url `https://localhost:port_number/simplemessage?message=world` and see the result.

---

## Want more ?

Checkout the [Examples folder](https://github.com/dasiths/SimpleEndpoints/tree/master/src/SimpleEndpoints.Example) for more. Await more examples in the coming weeks.

The Endpoints are automatically inherited from a [`ControllerBase`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-3.1) and decorated with [`ApiController` attribute](https://www.strathweb.com/2018/02/exploring-the-apicontrollerattribute-and-its-features-for-asp-net-core-mvc-2-1/). You can decorate the endpoint class/action method with the usual (Route, HttpGet, FromQuery etc) attributes to customise and extend the functionality. Endpoints fully support [AspNetCore routing conventions](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1).

if you need need an endpoint with a custom route, a mix of parameters coming from the Route/Query/Body or need full control over of any aspect then you can do something like this. Each of these class/method attributes works independently of each other and you can pick and choose them as required.

```c#
    [Route("api/some-path/[endpoint]")] //results in "api/some-path/mycustom"
    public class MyCustomEndpoint : AsyncEndpoint<Request, Result>
    {
        [NonAction] // important: mark this as non action to ignore this in api
        public override async Task<ActionResult<Result>> HandleAsync(Request request, CancellationToken cancellationToken = default)
        {
            // logic here
        }

        // definew your custom signature
        [HttpGet("custom_path")]
        public async Task<ActionResult<Result>> Post([FromQuery]string id, [FromBody]BodyModel model, CancellationToken cancellationToken)
        {
            // map to the view model
            var requestModel = new Request() {
                id_property = id,
                model_property - model
            }

            // pass to the handler method
            return await this.HandleAsync(requestModel, cancellationToken);
        }
    }
```

---

Feel free to contribute and raise issues as you see fit :)

- Creator: Dasith Wijesiriwardena (https://dasith.me)
