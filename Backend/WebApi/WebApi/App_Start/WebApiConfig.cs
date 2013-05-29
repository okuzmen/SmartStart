using System.Web.Http;
using Microsoft.Practices.Unity;
using WebApi.ApiControllers;
using WebApi.Configuration;
using WebApi.Handlers;
using WebApi.Infrastructure;
using WebApi.Interfaces;
using WebApi.Repositories;

[assembly: WebActivator.PreApplicationStartMethod(typeof(WebApi.App_Start.WebApiConfig), "ConfigureWithWebActivator")]
namespace WebApi.App_Start
{
    ///<remarks>
    /// This class is discovered and run during startup; see
    /// http://blogs.msdn.com/b/davidebb/archive/2010/10/11/light-up-your-nupacks-with-startup-code-and-webactivator.aspx
    ///</remarks>
    public static class WebApiConfig
    {

        internal static void ConfigureWithWebActivator()
        {
            Configure(GlobalConfiguration.Configuration);
        }

        public static void Configure(HttpConfiguration config)
        {
            ConfigureSupport(config);
            ConfigureRouting(config);
            ConfigureDependencyResolver(config);
            ConfigureHandlers(config);
        }

        #region Private Methods

        private static void ConfigureSupport(HttpConfiguration config)
        {
            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }

        private static void ConfigureDependencyResolver(HttpConfiguration config)
        {
            var unity = new UnityContainer();
            unity.RegisterType<HoleController>();

            //HierarchicalLifetimeManager it is a special lifetime manager which works like ContainerControlledLifetimeManager, 
            //except that in the presence of child containers, each child gets it's own instance of the object, instead of sharing one in the common parent.
            unity.RegisterType<IHoleRepository, HoleRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new IoCContainer(unity);
        }

        private static void ConfigureRouting(HttpConfiguration config)
        {
            #region default api routing

            //config.Routes.MapHttpRoute(
            //    name: ConfigurationProvider.GetRouteName(),
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            #endregion

            config.Routes.MapHttpRoute(
                name: ConfigurationProvider.GetRouteName(),
                routeTemplate: "breeze/{controller}/{action}");
        }

        private static void ConfigureHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new CorsHandler());
        }
        #endregion
    }
}