
using System.Net;
using System.Text.Json;
using Bnt_Api_Gateway.Domain;

namespace Bnt_Api_Gateway.Infrastructure;
public class AppExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public AppExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

       if (context.Response.StatusCode >= 400)
        {
            var error = new AppException(new[]{"An error occurred"},"GATEWAY_ERROR",500);
           
            switch (context.Response.StatusCode)
            {
                case (int)HttpStatusCode.BadRequest:
                    error = new AppException(new[]{"Bad Request"},"GATEWAY_ERROR",400);
                    break;
                case (int)HttpStatusCode.Unauthorized:
                    error = new AppException(new[]{"Unauthorized"},"GATEWAY_ERROR",401);
                    break;
                case (int)HttpStatusCode.Forbidden:
                    error = new AppException(new[]{"Forbidden"},"GATEWAY_ERROR",403);
                    break;
                case (int)HttpStatusCode.NotFound:
                    error = new AppException(new[]{"NotFound"},"GATEWAY_ERROR",404);
                    break;
                case (int)HttpStatusCode.MethodNotAllowed:
                    error = new AppException(new[]{"MethodNotAllowed"},"GATEWAY_ERROR",405);
                    break;
                case (int)HttpStatusCode.RequestTimeout:
                    error = new AppException(new[]{"RequestTimeout"},"GATEWAY_ERROR",408);
                    break;
                case (int)HttpStatusCode.Conflict:
                    error = new AppException(new[]{"Conflict"},"GATEWAY_ERROR",409);
                    break;
                case (int)HttpStatusCode.Gone:
                    error = new AppException(new[]{"Gone"},"GATEWAY_ERROR",410);
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    error = new AppException(new[]{"InternalServerError"},"GATEWAY_ERROR",500);
                    break;
                case (int)HttpStatusCode.ServiceUnavailable:
                    error = new AppException(new[]{"ServiceUnavailable"},"GATEWAY_ERROR",503);
                    break;
                case (int)HttpStatusCode.NotImplemented:
                    error = new AppException(new[]{"NotImplemented"},"GATEWAY_ERROR",501);
                    break;
                case (int)HttpStatusCode.HttpVersionNotSupported:
                    error = new AppException(new[]{"HttpVersionNotSupported"},"GATEWAY_ERROR",505);
                    break;
                default:
                    break;
            }

            var json = JsonSerializer.Serialize(new AppExceptionResponse(error));

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    
    }
}
