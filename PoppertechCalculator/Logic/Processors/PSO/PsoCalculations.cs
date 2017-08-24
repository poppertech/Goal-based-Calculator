using System.Linq;
using PoppertechCalculator.Logic.Interfaces.Pso;
using PoppertechCalculator.Models.PSO;
using PoppertechCalculator.Processors;
using System.Collections.Generic;
namespace PoppertechCalculator.Logic.Processors.PSO
{
    public class PsoCalculations : IPsoCalculations
    {
        private readonly IGoalAttainmentProcessor _goalAttainmentProcessor;

        public PsoCalculations(IGoalAttainmentProcessor goalAttainmentProcessor)
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
            var budget = context.InvestmentContexts.Sum(c => c.Amount);
            var investmentContext1 = context.InvestmentContexts.First();
            var investmentContext2 = context.InvestmentContexts.Last();

            for (decimal investment1 = context.PositionLowerBound; investment1 <= context.PositionUpperBound; investment1 = investment1 + context.Interval)
            {
                var investment2 = budget - investment1;
                if (investment2 < context.PositionLowerBound)
                    break;
                if (investment2 > context.PositionUpperBound)
                    continue;

                investmentContext1.Amount = investment1;
                investmentContext2.Amount = investment2;

                var chartData = _goalAttainmentProcessor.CalculateGoalAttainmentChartData(context);

                var feasibleInvestmentResults = context.InvestmentContexts.Select(c =>
                {
                    return new OptimalInvestmentResult
                    {
                        Name = c.Name,
                        Amount = c.Amount,
                        Weight = c.Amount / budget
                    };
                }
                );

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