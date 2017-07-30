using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using PoppertechCalculator.Processors;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class GoalAttainmentCalculatorTests
    {
        [TestMethod]
        public void CalculateAttainmentProbabilitiesOnSuccessReturnsProbabilities()
        {
            //arrange
            var expectedProbabilities = new decimal[3] { .75m, .5m, .25m};
            var returns1 = new decimal[3, 4] { { .95m, 1, 1, 1 }, { 1, .95m, 1, 1 }, { 1, 1, .95m, 1 } };
            var returns2 = new decimal[3, 4]{ { 1, 1.05m, 1.25m, 1.5m }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
            var investmentContext1 = new PortfolioInvestmentContext() { Amount = 80, Weight = .4m, TimeSeriesReturns = returns1 };
            var investmentContext2 = new PortfolioInvestmentContext() {Amount = 120, Weight = .6m, TimeSeriesReturns = returns2 };
            var investmentContexts = new[] { investmentContext1, investmentContext2 };
            var cashFlows = new decimal[] {0, 200, 10, 20 };
            var portfolioContext = new PortfolioContext() {
                InvestmentContexts = investmentContexts,
                CashFlows = cashFlows
            };

            var calculator = new GoalAttainmentCalculator();

            //act
            var actualProbabilities = calculator.CalculateAttainmentProbabilities(portfolioContext);

            //assert
            Assert.AreEqual(expectedProbabilities.Length, actualProbabilities.Length);
            Assert.AreEqual(expectedProbabilities[0], actualProbabilities[0]);
            Assert.AreEqual(expectedProbabilities[1], actualProbabilities[1]);
            Assert.AreEqual(expectedProbabilities[2], actualProbabilities[2]);
        }
    }
}
