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

            container.RegisterType<IGoalAttainmentProcessor, GoalAttainmentProcessor>();
            container.RegisterType<IGoalAttainmentCalculator, GoalAttainmentCalculator>();
            container.RegisterType<ICumulativeReturnsCalculator, CumulativeReturnsCalculator>();

            container.RegisterType<IForecastGraphCalculations, ForecastGraphCalculations>();
            container.RegisterType<IHistogramCalculations, HistogramCalculations>();
            container.RegisterType<IJointSimulator, JointSimulator>();
            container.RegisterType<IMonteCarloSimulator, MonteCarloSimulator>();
            container.RegisterType<ISimulationProcessor, SimulationProcessor>();
            container.RegisterType<IStatisticsCalculations, StatisticsCalculations>();
            container.RegisterType<IUniformRandomRepository, UniformRandomRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}