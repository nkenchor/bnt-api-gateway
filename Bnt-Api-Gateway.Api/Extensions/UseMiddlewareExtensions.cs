using Ocelot.Middleware;

namespace Bnt_Api_Gateway.Api.Extensions;
public static class MiddlewareExtensions
{
    public static void UseSwaggerAndOcelot(this IApplicationBuilder app)
    {
        app.UseSwaggerForOcelotUI(options =>
        {
            options.PathToSwaggerGenerator = "/swagger/docs";
        }).UseOcelot().Wait();
    }
}
