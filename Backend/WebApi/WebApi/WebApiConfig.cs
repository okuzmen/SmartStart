using System.Data.Entity;
using System.Web.Http;
using Microsoft.Practices.Unity;
using WebApi.ApiControllers;
using WebApi.Configuration;
using WebApi.Infrastructure;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: ConfigurationProvider.GetRouteName(),
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            AdjustDependencyResolver(config);
            Database.SetInitializer(new SmartStartContextInitializer());
        }

        private static void AdjustDependencyResolver(HttpConfiguration config)
        {
            var unity = new UnityContainer();
            unity.RegisterType<HoleController>();

            //HierarchicalLifetimeManager it is a special lifetime manager which works like ContainerControlledLifetimeManager, 
            //except that in the presence of child containers, each child gets it's own instance of the object, instead of sharing one in the common parent.
            unity.RegisterType<IHoleRepository, HoleRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new IoCContainer(unity);
        }
    }
}