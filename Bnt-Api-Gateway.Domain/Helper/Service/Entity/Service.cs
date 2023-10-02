
using System.ComponentModel.DataAnnotations;
namespace Bnt_Api_Gateway.Domain;
public static class Service
{
    
    public static string Address { get; set; }
    public static string Port { get; set; }
    public static string Name{get; set; }
    public static string Mode{get; set; }
    public static string LogFileName{get; set; }
    public static string LogDirectory{get; set;}
    public static string EventBusUrl{get; set; }
    public static string LaunchUrl{get; set; }
  
}

