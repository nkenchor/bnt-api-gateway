using Bnt_Api_Gateway.Domain;
using Serilog;
namespace Bnt_Api_Gateway.Infrastructure;

public class ServiceProvider
{ 
    
    public static void MapConfiguration(IConfiguration _configuration)
    {
        Log.Information("Mapping service configuration from env file to global configuration object...");
        Service.Address=_configuration.GetSection("Service")["Address"];
        Service.Port=_configuration.GetSection("Service")["Port"];
        Service.Name=_configuration.GetSection("Service")["Name"];;
        Service.Mode=_configuration.GetSection("Service")["Mode"];
        Service.LogFileName=_configuration.GetSection("Service")["LogFileName"];
        Service.LogDirectory=_configuration.GetSection("Service")["LogDirectory"];
        Service.EventBusUrl=_configuration.GetSection("Service")["EventBusUrl"];
        Service.LaunchUrl=_configuration.GetSection("Service")["LaunchUrl"];  
        Token.Key=_configuration.GetSection("Token")["Key"];
        Token.Audience=_configuration.GetSection("Token")["Audience"];
        Token.Issuer=_configuration.GetSection("Token")["Issuer"];
        Token.Expiry=_configuration.GetSection("Token")["Expiry"];  
    }
    
    public static bool CheckConfiguration(string envFilePath)
    {
        Log.Information("Reading service configuration from env file...");
      
        if (!File.Exists(envFilePath)) 
        {
            Log.Error("Env file does not exist");
            return false;
        }
        foreach (var line in File.ReadAllLines(envFilePath))
        {
            var parts = line.Split(new[] {'='},2,StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;
            Log.Information("Setting global environment variable" + parts[0]);
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
        return true;
    }

    
}