using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class GoalAttainmentCalculator : IGoalAttainmentCalculator
    {
        public static decimal[] CalculateAttainmentProbability(PortfolioContext portfolioContext)
        {
            var investmentsSimulations = InitializeInvestmentsSimulations(portfolioContext);
            var portfolioSimulations = InitializePortfolioSimulations(portfolioContext);
            portfolioSimulations = CalculatePortfolioSimulations(portfolioSimulations, investmentsSimulations, portfolioContext.CashFlowContext.CashFlows, 0);
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
                portfolioSimulations[cntPeriod, cntSimulation] = portfolioSimulations[cntPeriod, cntSimulation] - cashFlows[cntPeriod];
            }

            return portfolioSimulations;
        }
    }
}