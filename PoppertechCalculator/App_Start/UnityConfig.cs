using Microsoft.Practices.Unity;
using PoppertechCalculator.Repositories;
using System.Web.Http;
using Unity.WebApi;

namespace PoppertechCalculator
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IUniformRandomRepository, UniformRandomRepository>();
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}