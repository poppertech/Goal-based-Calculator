using Microsoft.Practices.Unity;
using PoppertechCalculator.Processors;
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

            container.RegisterType<IForecastGraphCalculations, ForecastGraphCalculations>();
            container.RegisterType<IHistogramCalculations, HistogramCalculations>();
            container.RegisterType<IJointSimulator, JointSimulator>();
            container.RegisterType<IMonteCarloSimulator, MonteCarloSimulator>();
            container.RegisterType<ISimulationProcessor, SimulationProcessor>();
            container.RegisterType<IStatisticsCalculations, StatisticsCalculations>();
            container.RegisterType<IUniformRandomRepository, UniformRandomRepository>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}