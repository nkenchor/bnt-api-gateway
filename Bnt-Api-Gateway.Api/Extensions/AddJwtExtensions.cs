using Bnt_Api_Gateway.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bnt_Api_Gateway.Api.Extensions;

public static class JwtBearerExtensions
{
    public static void AddJwtBearerWithSymmetricKey(this IServiceCollection services, string key)
    {
        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = $"{Token.Issuer}",
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = Token.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                };
            });
    }
}
