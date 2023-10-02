using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Bnt_Api_Gateway.Api.Extensions;

public static class OcelotExtensions
{
    public static IServiceCollection AddOcelotWithSwaggerSupport(this IServiceCollection services, IConfiguration configuration, string ocelotConfigFolder)
    {
        configuration = new ConfigurationBuilder().AddOcelotWithSwaggerSupport(options =>
{
            options.Folder = "Configuration";
        }).Build();
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        services.AddOcelot(configuration);
        services.AddSwaggerForOcelot(configuration);
   

        return services;
    }
}

