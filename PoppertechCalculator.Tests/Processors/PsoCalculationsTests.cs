using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models.PSO;
using Moq;
using PoppertechCalculator.Processors;
using PoppertechCalculator.Models;
using PoppertechCalculator.Logic.Processors.PSO;


namespace PoppertechCalculator.Tests.Processors
{
    [TestClass]
    public class PsoCalculationsTests
    {
        [TestMethod]
        public void OptimizePortfolioOnSuccessReturnsCorrectResults()
        {
            //arrange
            var investmentContext1 = new InvestmentContext { Name = "Investment 1", Amount = 4000 };
            var investmentContext2 = new InvestmentContext { Name = "Investment 2", Amount = 0 };
            var investmentContexts = new[] { investmentContext1, investmentContext2 };
            var context = new PsoContext() { PositionLowerBound = 1000, PositionUpperBound = 3000, Interval = 1000, InvestmentContexts = investmentContexts };

            var optimalInvestment1Amount = 3000m;
            var optimalInvestment2Amount = 1000m;
            var optimalInvestment1 = new OptimalInvestmentResult {Name = "Investment 1", Amount = optimalInvestment1Amount };
            var optimalInvestment2 = new OptimalInvestmentResult { Name = "Investment 2", Amount = optimalInvestment2Amount };
            var optimalInvestments = new[] { optimalInvestment1, optimalInvestment2 };

            var maxProbability = .8m;
            var subOptimalChartData1 = new Dictionary<string, decimal>() { { "Year 1", .7m }, { "Year 2", .6m } };
            var subOptimalChartData2 = new Dictionary<string, decimal>() { { "Year 1", .5m }, { "Year 2", .4m } };
            var expectedChartData = new Dictionary<string, decimal>(){{"Year 1", .9m}, {"Year 2", maxProbability}};

            var expectedResult = new PsoResults { ChartData = expectedChartData, OptimalInvestments = optimalInvestments};

            var processor = new Mock<IGoalAttainmentProcessor>();
            processor.SetupSequence(p => p.CalculateGoalAttainmentChartData(It.IsAny<GoalAttainmentContext>()))
                .Returns(subOptimalChartData1)
                .Returns(subOptimalChartData2)
                .Returns(expectedChartData);

            var calculator = new PsoCalculationsProcessor(processor.Object);
            
            //act
            var result = calculator.OptimizePortfolio(context);

            //assert
            Assert.AreEqual( maxProbability, result.ChartData.Values.Last());
            Assert.AreEqual(optimalInvestment1Amount, result.OptimalInvestments.First().Amount);
            Assert.AreEqual(optimalInvestment2Amount, result.OptimalInvestments.Last().Amount);

        }
    }
}
