using System.Reflection;
using Bnt_Api_Gateway.Domain;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Bnt_Api_Gateway.Api.Extensions;

public static class SerilogExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder)
    {
        var applicationVersion = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        builder.UseSerilog((ctx, lc) => lc
            .WriteTo.Console()
            .WriteTo.File($"../logs/{Service.LogFileName}",
            restrictedToMinimumLevel: LogEventLevel.Verbose,
            outputTemplate: ConstProvider.SerilogOutPutTemplate)
            .Enrich.FromLogContext()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .Enrich.WithCorrelationIdHeader("X-Correlation-Id")
            .Enrich.WithClientAgent()
            .Enrich.WithClientIp()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithProperty("Version", applicationVersion ?? "")
            .Enrich.WithProperty("ApplicationName", Service.Name));

        return builder;
    }
}
