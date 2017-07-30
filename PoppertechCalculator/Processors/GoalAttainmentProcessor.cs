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
        private ICumulativeReturnsCalculator _cumulativeReturnsCalculator;
        private IGoalAttainmentCalculator _goalCalculator;

        public GoalAttainmentProcessor(IJointSimulator jointSimulator, ICumulativeReturnsCalculator cumulativeReturnsCalculator, IGoalAttainmentCalculator goalCalculator)
        {
            _jointSimulator = jointSimulator;
            _cumulativeReturnsCalculator = cumulativeReturnsCalculator;
            _goalCalculator = goalCalculator;
        }

        public Dictionary<string, decimal> CalculateGoalAttainment(GoalAttainmentContext context)
        {
            var investmentContexts = context.InvestmentContexts;
            var unconditionalSimulations = CalculateUnconditionalSimulations(investmentContexts);
            var portfolioInvestmentContexts = CalculateConditionalSimulations(investmentContexts, unconditionalSimulations.AreaNumbers, context.CashFlows.Length);
            var portfolioContext = new PortfolioContext
            {
                InvestmentContexts = portfolioInvestmentContexts,
                CashFlows = context.CashFlows
            };
            var probabilities = _goalCalculator.CalculateAttainmentProbabilities(portfolioContext);
            var probabilityChartData = probabilities.Select((p, i) => new { Date = "Year " + (i + 1), Probability = p }).ToDictionary(g => g.Date, g => g.Probability);
            return probabilityChartData;
        }

        private MonteCarloResults CalculateUnconditionalSimulations(ForecastVariable[] forecasts) 
        {
            var unConditionalForecast = forecasts.Where(f => string.IsNullOrWhiteSpace(f.Parent)).Single();
            var unConditionalSimulations = _jointSimulator.CalculateUnconditionalSimulations(unConditionalForecast.Name, unConditionalForecast.Regions[0].Forecast);
            return unConditionalSimulations;
        }

        private PortfolioInvestmentContext[] CalculateConditionalSimulations(InvestmentContext[] investmentContexts, int[] unConditionalAreaNumbers, int numCashFlows)
        {
            var conditionalContexts = investmentContexts.Where(f => !string.IsNullOrWhiteSpace(f.Parent)).ToArray();
            var portfolioInvestmentContexts = new PortfolioInvestmentContext[conditionalContexts.Length];

            for (int cnt = 0; cnt < conditionalContexts.Length; cnt++)
            {
                var investmentContext = conditionalContexts[cnt];
                var regions = investmentContext.Regions.ToArray();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(unConditionalAreaNumbers, investmentContext.Name, regions);
                var timeSeries = _cumulativeReturnsCalculator.CalculateTimeSeriesReturns(investmentContext.InitialPrice, jointSimulations.Simulations, numCashFlows);
                var portfolioInvestmentContext = new PortfolioInvestmentContext(investmentContext);
                portfolioInvestmentContext.TimeSeriesReturns = timeSeries;
                portfolioInvestmentContexts[cnt] = portfolioInvestmentContext;
            }
            return portfolioInvestmentContexts;
        }

    }
}