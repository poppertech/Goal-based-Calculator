using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Processors;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class CumulativeReturnsCalculatorTests
    {
        [TestMethod]
        public void CalculateTimeSeriesReturnsOnSuccessReturnsCorrectCumulativeReturns()
        {
            //arrange
            var expectedCumulativeReturns = new decimal[,] { {.5m, 1.25m}, {.75m, 2.5m} };
            var initialPrice = 100;
            var simulations = new decimal[] { 50, 125, 150, 200 };
            var numCashFlows = 3;

            var calculator = new CumulativeReturnsCalculator();

            //act
            var actualCumulativeReturns = calculator.CalculateTimeSeriesReturns(initialPrice, simulations, numCashFlows);

            //assert
            Assert.AreEqual(expectedCumulativeReturns[0,0], actualCumulativeReturns[0,0]);
            Assert.AreEqual(expectedCumulativeReturns[1,0], actualCumulativeReturns[1,0]);
            Assert.AreEqual(expectedCumulativeReturns[0,1], actualCumulativeReturns[0,1]);
            Assert.AreEqual(expectedCumulativeReturns[1,1], actualCumulativeReturns[1,1]);
        }
    }
}
