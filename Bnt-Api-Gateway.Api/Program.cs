using Microsoft.AspNetCore.Mvc;
using Bnt_Api_Gateway.Domain;
using Serilog;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Bnt_Api_Gateway.Infrastructure;
using Bnt_Api_Gateway.Api.Extensions;
using serviceProvider = Bnt_Api_Gateway.Infrastructure.ServiceProvider;

var builder = WebApplication.CreateBuilder(args);

//Check for service configuration

if (serviceProvider.CheckConfiguration(@"../bnt-api-gateway.env") == false) return;

//Add environment variables to the global config
builder.Configuration.AddEnvironmentVariables();
//Map configuration to global class
serviceProvider.MapConfiguration(builder.Configuration);

// Suppress automatic model validation
builder.Services.Configure<ApiBehaviorOptions>(options =>{options.SuppressModelStateInvalidFilter = true;});

// Add JWT Bearer authentication with symmetric key validation
builder.Services.AddJwtBearerWithSymmetricKey(Token.Key);

// Add Serilog
builder.Host.AddSerilog();

// Add Ocelot
builder.Services.AddOcelotWithSwaggerSupport(builder.Configuration, "Configuration");

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("X-Correlation-Id"));
builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Defining the service address
builder.WebHost.UseUrls($"{Service.Address}:{Service.Port}");
var app = builder.Build();

// Add middleware
app.UseMiddleware<AppExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseCustomCors();
app.UseHeaderPropagation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwaggerAndOcelot();
app.MapControllers();
app.Run();
