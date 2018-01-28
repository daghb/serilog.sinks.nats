# serilog.sinks.nats

<img src="https://ci.appveyor.com/api/projects/status/row7ob6fhgmvtqwb?svg=true" /> 
<a href="https://www.nuget.org/packages/Serilog.Sinks.Nats/">
  <img src="https://img.shields.io/nuget/v/Serilog.Sinks.Nats.svg" />
</a>

Serilog Sink for Nats

## Installation

Using [Nuget](https://www.nuget.org/packages/Serilog.Sinks.Nats/):

```
Install-Package Serilog.Sinks.Nats
```

## Usage

To use with `ILoggerFactory` via dependency injection, 
add the following to `ConfigureServices` in your `Startup` class. 
See the [logging documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging)
for specific help on using the `ILoggerFactory` and `ILogger<T>`.

```
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Nats;
using Serilog.Sinks.Nats.Sinks.Nats;

public class Startup 
{
   private readonly IConfiguration _config;
   // ... 
   public IServiceProvider ConfigureServices(IServiceCollection services)
   {
      var config = new NatsConfiguration
      {
          host = _config["NATS_HOST"]
          // ...
      };

      Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Nats(config, new JsonFormatter())
        .CreateLogger();

      var loggerFactory = new LoggerFactory();
      loggerFactory
        .AddSerilog()
        .AddConsole(LogLevel.Information);

      services.AddSingleton<ILoggerFactory>(loggerFactory);
   }
   // ...
}
```


## References

- [Serilog](https://serilog.net/)
- [Logging in ASP.Net Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging)
- [Dependency Injection in ASP.Net Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)