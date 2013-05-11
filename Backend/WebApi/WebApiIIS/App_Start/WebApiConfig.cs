using System.Web.Http;

namespace WebApiIIS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            WebApi.WebApiConfig.Register(config);
        }
    }
}
