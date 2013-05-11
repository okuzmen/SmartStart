using System.Configuration;

namespace WebApi.Configuration
{
    internal static class ConfigurationProvider
    {
         internal static string GetRouteName()
         {
             return ConfigurationManager.AppSettings[AppSettingsKeys.RouteName];
         }
    }
}