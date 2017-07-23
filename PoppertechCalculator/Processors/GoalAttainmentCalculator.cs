using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class GoalAttainmentCalculator : IGoalAttainmentCalculator
    {
        public static decimal[] CalculateAttainmentProbabilities(PortfolioContext portfolioContext)
        {
            var investmentsSimulations = InitializeInvestmentsSimulations(portfolioContext);
            var portfolioSimulations = InitializePortfolioSimulations(portfolioContext);
            portfolioSimulations = CalculatePortfolioSimulations(portfolioSimulations, investmentsSimulations, portfolioContext.CashFlows, 0);
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0);
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
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0);
            var numSimulations = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(1);
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
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0);
            var numSimulations = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(1);
            var cumulativeReturns = new decimal[numInvestments, numPeriods, numSimulations];

            for (int cntInvestments = 0; cntInvestments < numInvestments; cntInvestments++)
            {
                var investmentContext = portfolioContext.InvestmentContexts[cntInvestments];
                for (int cntPeriods = 0; cntPeriods < numPeriods; cntPeriods++)
                {
                    for (int cntSimulations = 0; cntSimulations < numSimulations; cntSimulations++)
                    {
                        cumulativeReturns[cntInvestments, cntPeriods, cntSimulations] = investmentContext.TimeSeriesReturns[cntPeriods, cntSimulations];
                    }
                }
            }

            return cumulativeReturns;
        }

        private static decimal[,] InitializePortfolioSimulations(PortfolioContext portfolioContext)
        {
            var numPeriods = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(0);
            var numSimulations = portfolioContext.InvestmentContexts[0].TimeSeriesReturns.GetUpperBound(1);
            var simulations = new decimal[numPeriods, numSimulations];
            return simulations;
        }

        private static decimal[,] CalculatePortfolioSimulations(decimal[,] portfolioSimulations, decimal[,,] investmentsSimulations, decimal[] cashFlows, int cntPeriod)
        {
            var numInvestments = investmentsSimulations.GetUpperBound(0);
            var numSimulations = investmentsSimulations.GetUpperBound(2);
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
                if (portfolioSimulations[cntPeriod - 1, cntSimulation] <= 0 || portfolioSimulationBase <= 0)
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
            var numInvestments = investmentsSimulations.GetUpperBound(0);
            var numSimulations = investmentsSimulations.GetUpperBound(2);
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
            var numPeriods = portfolioSimulations.GetUpperBound(0);
            var numSimulations = portfolioSimulations.GetUpperBound(1);

            var probabilities = new decimal[numPeriods];

            for (int cntPeriods = 0; cntPeriods < numPeriods; cntPeriods++)
            {
                for (int cntSimulations = 0; cntSimulations < numSimulations; cntSimulations++)
                {
                    probabilities[cntPeriods] = portfolioSimulations[cntPeriods, cntSimulations] > 0 ? probabilities[cntPeriods] + 1 : probabilities[cntPeriods];
                }
                probabilities[cntPeriods] = probabilities[cntPeriods] / numSimulations;
            }

            return probabilities;
        }
    }
}