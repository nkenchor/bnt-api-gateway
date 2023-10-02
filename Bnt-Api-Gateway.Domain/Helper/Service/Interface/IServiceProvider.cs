namespace Bnt_Api_Gateway.Domain;

public interface IServiceProvider
{
    void MapConfig();
    void ReadConfig(string envFilePath);
    
}
    
