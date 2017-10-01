using System.Linq;
using PoppertechCalculator.Logic.Interfaces.Pso;
using PoppertechCalculator.Models.PSO;
using PoppertechCalculator.Processors;
using System.Collections.Generic;
namespace PoppertechCalculator.Logic.Processors.PSO
{
    public class PsoCalculationsProcessor : IPsoCalculationsProcessor
    {
        private readonly IGoalAttainmentProcessor _goalAttainmentProcessor;

        public PsoCalculationsProcessor(IGoalAttainmentProcessor goalAttainmentProcessor)
        {
            _goalAttainmentProcessor = goalAttainmentProcessor;
        }

        public PsoResults OptimizePortfolio(PsoContext context)
        {
            var feasibleResults = CalculateFeasibleResults(context);
            var optimalResult = ChooseOptimalResult(feasibleResults);
            return optimalResult;
        }

        private IEnumerable<PsoResults> CalculateFeasibleResults(PsoContext context)
        {
            var feasiblePortfolioResults = new List<PsoResults>();
            var investments = context.InvestmentContexts.Where(c => c.Parent != null);
            var budget = investments.Sum(c => c.Amount);
            var investmentContext1 = investments.First();
            var investmentContext2 = investments.Last();

            for (decimal investment1 = context.OptimizationParams.LowerBound; investment1 <= context.OptimizationParams.UpperBound; investment1 = investment1 + context.OptimizationParams.Interval)
            {
                var investment2 = budget - investment1;
                if (investment2 < context.OptimizationParams.LowerBound)
                    break;
                if (investment2 > context.OptimizationParams.UpperBound)
                    continue;

                investmentContext1.Amount = investment1;
                investmentContext2.Amount = investment2;

                var chartData = _goalAttainmentProcessor.CalculateGoalAttainmentChartData(context);

                var feasibleInvestmentResults = new List<OptimalInvestmentResult>();
                foreach (var investmentContext in investments)
                {
                    var optimalInvestmentResult = new OptimalInvestmentResult
                    {
                        Name = investmentContext.Name,
                        Amount = investmentContext.Amount,
                        Weight = investmentContext.Amount / budget
                    };
                    feasibleInvestmentResults.Add(optimalInvestmentResult);
                }

                var feasiblePortfolioResult = new PsoResults
                {
                    ChartData = chartData,
                    OptimalInvestments = feasibleInvestmentResults
                };

                feasiblePortfolioResults.Add(feasiblePortfolioResult);
            }
            return feasiblePortfolioResults;
        }

        private PsoResults ChooseOptimalResult(IEnumerable<PsoResults> feasibleResults)
        {
            decimal maxProbability = 0;
            PsoResults optimalResult = null;
            foreach (var feasibleResult in feasibleResults)
            {
                var probability = feasibleResult.ChartData.Values.Last();
                if (probability > maxProbability)
                {
                    maxProbability = probability;
                    optimalResult = feasibleResult;
                }
            }
            return optimalResult;
        }
    }
}