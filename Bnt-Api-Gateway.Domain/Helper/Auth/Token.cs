
using System.ComponentModel.DataAnnotations;
namespace Bnt_Api_Gateway.Domain;
public static class Token
{
    
    public static string Key { get; set; }
    public static string Audience { get; set; }
    public static string Issuer{get; set; }
    public static string Expiry{get; set; }
  
}

