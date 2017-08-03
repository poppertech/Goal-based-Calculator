using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using Moq;
using PoppertechCalculator.Processors;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class GoalAttainmentProcessorTests
    {
        [TestMethod]
        public void CalculateGoalAttainmentOnSuccessReturnsProbabilityChartData()
        {
            //arrange
            var date1 = "Year 1";
            var date2 = "Year 2";
            var probability1 = .5m;
            var probability2 = .75m;

            var expectedChartData = new Dictionary<string, decimal> { { date1, probability1 }, { date2, probability2 } };

            var unconditionalContext = new InvestmentContext 
            {
                Name = "GDP", 
                Regions = new[] { new ForecastRegion{Forecast = new Forecast{}} } 
            };

            var conditionalContext = new InvestmentContext 
            {
                Name = "Stocks",
                InitialPrice = 100,
                Parent = "GDP",
                Regions = new[] { new ForecastRegion { Forecast = new Forecast { } } } ,
                Amount = 1000
            };

            var context = new GoalAttainmentContext() 
            {
                InvestmentContexts = new[]{unconditionalContext, conditionalContext},
                CashFlows = new decimal[] { 100}
            };

            var unconditionalMonteCarloResults = new MonteCarloResults{Simulations = new decimal[]{100}, AreaNumbers = new int[]{1} };
            var conditionalMonteCarloResults = new MonteCarloResults { Simulations = new decimal[] { 100 }, AreaNumbers = new int[] { 1 } };
            var jointSimulator = new Mock<IJointSimulator>();
            jointSimulator.Setup(s => s.CalculateUnconditionalSimulations(It.IsAny<string>(), It.IsAny<Forecast>())).Returns(unconditionalMonteCarloResults);
            jointSimulator.Setup(s => s.CalculateJointSimulations(It.IsAny<IList<int>>(), It.IsAny<string>(), It.IsAny<IList<ForecastRegion>>())).Returns(conditionalMonteCarloResults);

            var timeSeries = new decimal[,] { { .75m, 1.25m } };
            var cumulativeReturnsCalculator = new Mock<ICumulativeReturnsCalculator>();
            cumulativeReturnsCalculator.Setup(c => c.CalculateTimeSeriesReturns(It.IsAny<decimal>(), It.IsAny<decimal[]>(), It.IsAny<int>())).Returns(timeSeries);

            var probabilities = new decimal[] { probability1, probability2 };
            var goalCalculator = new Mock<IGoalAttainmentCalculator>();
            goalCalculator.Setup(c => c.CalculateAttainmentProbabilities(It.IsAny<PortfolioContext>())).Returns(probabilities);

            var processor = new GoalAttainmentProcessor(jointSimulator.Object, cumulativeReturnsCalculator.Object, goalCalculator.Object);

            //act
            var probabilityChartData = processor.CalculateGoalAttainment(context);

            //assert
            Assert.AreEqual(probabilityChartData[date1], probability1);
            Assert.AreEqual(probabilityChartData[date2], probability2);

        }
    }
}
