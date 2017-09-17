using PoppertechCalculator.Models;
using PoppertechCalculator.Repositories;
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
        private IPortfolioResultsRepository _repository;

        public GoalAttainmentProcessor(IJointSimulator jointSimulator, 
            ICumulativeReturnsCalculator cumulativeReturnsCalculator, 
            IGoalAttainmentCalculator goalCalculator,
            IPortfolioResultsRepository repository)
        {
            _jointSimulator = jointSimulator;
            _cumulativeReturnsCalculator = cumulativeReturnsCalculator;
            _goalCalculator = goalCalculator;
            _repository = repository;
        }

        public IDictionary<string, decimal> GetGoalAttainmentChartData()
        {
            return _repository.GetPortfolioResults();
        }

        public IDictionary<string, decimal> CalculateGoalAttainmentChartData(GoalAttainmentContext context)
        {
            var probabilities = CalculateGoalAttainment(context);
            var probabilityChartData = probabilities.Select((p, i) => new { Date = "Year " + (i + 1), Probability = p }).ToDictionary(g => g.Date, g => g.Probability);
            return probabilityChartData;
        }

        public IEnumerable<decimal> CalculateGoalAttainment(GoalAttainmentContext context)
        {
            var investmentContexts = context.InvestmentContexts;
            var unconditionalSimulations = CalculateUnconditionalSimulations(investmentContexts);
            var portfolioInvestmentContexts = CalculateConditionalSimulations(investmentContexts, unconditionalSimulations.AreaNumbers, context.CashFlows.Count());
            var portfolioContext = new PortfolioContext
            {
                InvestmentContexts = portfolioInvestmentContexts,
                CashFlows = context.CashFlows,
            };
            var probabilities = _goalCalculator.CalculateAttainmentProbabilities(portfolioContext);
            return probabilities;
        }

        private MonteCarloResults CalculateUnconditionalSimulations(IEnumerable<ForecastVariable> forecasts) 
        {
            var unConditionalForecast = forecasts.Where(f => string.IsNullOrWhiteSpace(f.Parent)).Single();
            var unConditionalSimulations = _jointSimulator.CalculateUnconditionalSimulations(unConditionalForecast.Name, unConditionalForecast.Regions[0].Forecast);
            return unConditionalSimulations;
        }

        private IList<PortfolioInvestmentContext> CalculateConditionalSimulations(IEnumerable<InvestmentContext> investmentContexts, IList<int> unConditionalAreaNumbers, int numCashFlows)
        {
            var conditionalContexts = investmentContexts.Where(f => !string.IsNullOrWhiteSpace(f.Parent)).ToArray();
            var portfolioInvestmentContexts = new PortfolioInvestmentContext[conditionalContexts.Length];
            var portfolioValue = conditionalContexts.Select(c => c.Amount).Sum();

            for (int cnt = 0; cnt < conditionalContexts.Length; cnt++)
            {
                var investmentContext = conditionalContexts[cnt];
                var regions = investmentContext.Regions.ToList();
                var jointSimulations = _jointSimulator.CalculateJointSimulations(unConditionalAreaNumbers, investmentContext.Name, regions);
                var timeSeries = _cumulativeReturnsCalculator.CalculateTimeSeriesReturns(investmentContext.InitialPrice, jointSimulations.Simulations, numCashFlows);
                var portfolioInvestmentContext = new PortfolioInvestmentContext(investmentContext);
                portfolioInvestmentContext.TimeSeriesReturns = timeSeries;
                portfolioInvestmentContext.Weight = portfolioInvestmentContext.Amount / portfolioValue;
                portfolioInvestmentContexts[cnt] = portfolioInvestmentContext;
            }
            return portfolioInvestmentContexts;
        }

    }
}