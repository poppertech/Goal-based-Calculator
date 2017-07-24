using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class GoalAttainmentCalculator : IGoalAttainmentCalculator
    {
        public decimal[] CalculateAttainmentProbabilities(PortfolioContext portfolioContext)
        {
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0) + 2;

            var investmentsSimulations = InitializeInvestmentsSimulations(portfolioContext);
            
            var portfolioSimulations = InitializePortfolioSimulations(portfolioContext);

            portfolioSimulations = CalculatePortfolioSimulations(portfolioSimulations, investmentsSimulations, portfolioContext.CashFlows, 0);
            
            var cumReturns = InitializeCumulativeReturns(portfolioContext);

            var weights = portfolioContext.InvestmentContexts.Select(c => c.Weight).ToArray();

            for (int cntPeriod = 1; cntPeriod < numPeriods; cntPeriod++)
            {
                investmentsSimulations = CalculateInvestmentSimulations(investmentsSimulations, cumReturns, portfolioSimulations, weights, cntPeriod);
                portfolioSimulations = CalculatePortfolioSimulations(portfolioSimulations, investmentsSimulations, portfolioContext.CashFlows, cntPeriod);
            }

            var attainmentProbabilities = CalculateProbabilities(portfolioSimulations);
            return attainmentProbabilities;
        }

        private static decimal[,,] InitializeInvestmentsSimulations(PortfolioContext portfolioContext)
        {
            const int cntPeriods = 0;
            var numInvestments = portfolioContext.InvestmentContexts.Length;
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0) + 2;
            var numSimulations = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(1) + 1;
            var investmentsSimulations = new decimal[numInvestments, numPeriods, numSimulations];
            
            for (int cntInvestments = 0; cntInvestments < numInvestments; cntInvestments++)
            {
                var investmentContext = portfolioContext.InvestmentContexts[cntInvestments];
                for (int cntSimulations = 0; cntSimulations < numSimulations; cntSimulations++)
                {
                    investmentsSimulations[cntInvestments, cntPeriods, cntSimulations] = investmentContext.Amount;
                }  
            }
            return investmentsSimulations;
        }

        private static decimal[, ,] InitializeCumulativeReturns(PortfolioContext portfolioContext)
        {
            var numInvestments = portfolioContext.InvestmentContexts.Length;
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0) + 2;
            var numSimulations = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(1) + 1;
            var cumulativeReturns = new decimal[numInvestments, numPeriods, numSimulations];

            for (int cntInvestments = 0; cntInvestments < numInvestments; cntInvestments++)
            {
                var investmentContext = portfolioContext.InvestmentContexts[cntInvestments];
                for (int cntPeriods = 1; cntPeriods < numPeriods; cntPeriods++)
                {
                    for (int cntSimulations = 0; cntSimulations < numSimulations; cntSimulations++)
                    {
                        cumulativeReturns[cntInvestments, cntPeriods, cntSimulations] = investmentContext.TimeSeriesReturns[cntPeriods - 1, cntSimulations];
                    }
                }
            }

            return cumulativeReturns;
        }

        private static decimal[,] InitializePortfolioSimulations(PortfolioContext portfolioContext)
        {
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0) + 2;
            var numSimulations = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(1) + 1;
            var simulations = new decimal[numPeriods, numSimulations];
            return simulations;
        }

        private static decimal[,] CalculatePortfolioSimulations(decimal[,] portfolioSimulations, decimal[,,] investmentsSimulations, decimal[] cashFlows, int cntPeriod)
        {
            var numInvestments = investmentsSimulations.GetUpperBound(0) + 1;
            var numSimulations = investmentsSimulations.GetUpperBound(2) + 1;

            var cashFlow = cashFlows[cntPeriod];

            for (var cntInvestment = 0; cntInvestment < numInvestments; cntInvestment++)
            {
                for (int cntSimulation = 0; cntSimulation < numSimulations; cntSimulation++)
                {
                    portfolioSimulations[cntPeriod, cntSimulation] = portfolioSimulations[cntPeriod, cntSimulation] + investmentsSimulations[cntInvestment, cntPeriod, cntSimulation];
                }
            }

            for (int cntSimulation = 0; cntSimulation < numSimulations; cntSimulation++)
            {
                var portfolioSimulationBase = portfolioSimulations[cntPeriod, cntSimulation] - cashFlows[cntPeriod];
                if (portfolioSimulationBase <= 0 || (cntPeriod >= 1 && portfolioSimulations[cntPeriod - 1, cntSimulation] <= 0))
                {
                    portfolioSimulations[cntPeriod, cntSimulation] = 0;
                }
                else
                {
                    portfolioSimulations[cntPeriod, cntSimulation] = portfolioSimulationBase;
                }
                    
            }

            return portfolioSimulations;
        }

        private static decimal[, ,] CalculateInvestmentSimulations(decimal[,,] investmentsSimulations, decimal[,,] cumReturns, decimal[,] portfolioSimulations, decimal[] weights, int cntPeriod)
        {
            var numInvestments = investmentsSimulations.GetUpperBound(0) + 1;
            var numSimulations = investmentsSimulations.GetUpperBound(2) + 1;
            for (int cntInvestment = 0; cntInvestment < numInvestments; cntInvestment++)
            {
                for (int cntSimulation = 0; cntSimulation < numSimulations; cntSimulation++)
                {
                    var cumReturn = cumReturns[cntInvestment, cntPeriod, cntSimulation];
                    var weight = weights[cntInvestment];
                    var portfolioSimulation = portfolioSimulations[cntPeriod - 1, cntSimulation];
                    investmentsSimulations[cntInvestment, cntPeriod, cntSimulation] = cumReturn * weight * portfolioSimulation;
                }   
            }
            return investmentsSimulations;
        }

        private static decimal[] CalculateProbabilities(decimal[,] portfolioSimulations)
        {
            var numPeriods = portfolioSimulations.GetUpperBound(0) + 1;
            var numSimulations = portfolioSimulations.GetUpperBound(1) + 1;

            var probabilities = new decimal[numPeriods - 1];

            for (int cntPeriods = 1; cntPeriods < numPeriods; cntPeriods++)
            {
                for (int cntSimulations = 0; cntSimulations < numSimulations; cntSimulations++)
                {
                    probabilities[cntPeriods- 1] = portfolioSimulations[cntPeriods, cntSimulations] > 0 ? probabilities[cntPeriods - 1] + 1 : probabilities[cntPeriods - 1];
                }
                probabilities[cntPeriods - 1] = probabilities[cntPeriods - 1] / numSimulations;
            }

            return probabilities;
        }
    }
}