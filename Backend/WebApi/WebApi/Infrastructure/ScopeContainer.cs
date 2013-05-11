using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace WebApi.Infrastructure
{
    internal class ScopeContainer : IDependencyScope
    {
        protected IUnityContainer container;

        public ScopeContainer(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }   

        public object GetService(Type serviceType)
        {
            return container.IsRegistered(serviceType) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.IsRegistered(serviceType) ? container.ResolveAll(serviceType) : new List<object>();
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}