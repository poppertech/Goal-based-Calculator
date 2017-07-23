using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class GoalAttainmentProcessor : IGoalAttainmentProcessor
    {
        private IJointSimulator _jointSimulator;

        public GoalAttainmentProcessor(IJointSimulator jointSimulator)
        {
            _jointSimulator = jointSimulator;
        }

        public Dictionary<string, decimal> CalculateGoalAttainment(GoalAttainmentContext context)
        {
            var forecasts = context.InvestmentContexts;
            var unconditionalSimulations = CalculateUnconditionalSimulations(forecasts);
            var conditionalSimulations = CalculateConditionalSimulations(forecasts, unconditionalSimulations.AreaNumbers);
            return new Dictionary<string, decimal>();
        }

        private MonteCarloResults CalculateUnconditionalSimulations(ForecastVariable[] forecasts) 
        {
            var unConditionalForecast = forecasts.Where(f => string.IsNullOrWhiteSpace(f.Parent)).Single();
            var unConditionalSimulations = _jointSimulator.CalculateUnconditionalSimulations(unConditionalForecast.Name, unConditionalForecast.Regions[0].Forecast);
            return unConditionalSimulations;
        }

        private MonteCarloResults[] CalculateConditionalSimulations(ForecastVariable[] forecasts, int[] unConditionalAreaNumbers)
        {
            var conditionalForecasts = forecasts.Where(f => !string.IsNullOrWhiteSpace(f.Parent)).ToArray();
            var conditionalSimulations = new MonteCarloResults[conditionalForecasts.Length];

            for (int cnt = 0; cnt < conditionalForecasts.Length; cnt++)
            {
                var conditionalForecast = conditionalForecasts[cnt];
                var regions = conditionalForecast.Regions.ToArray();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(unConditionalAreaNumbers, conditionalForecast.Name, regions);
                conditionalSimulations[cnt] = jointSimulations;
            }

            return conditionalSimulations;
        }

    }
}