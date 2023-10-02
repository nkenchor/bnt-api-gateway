
namespace Bnt_Api_Gateway.Api.Extensions;

public static class CorsExtensions
{
    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        return app;
    }
}