using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Processors;
using System.Linq;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class StatisticsCalculationsTests
    {
        [TestMethod]
        public void GetStatisticsOnSuccessReturnsStatistics()
        {
            //arrange
            var expectedMean = .5m;
            var expectedStdev = 0.534522484m;
            decimal expectedSkew = 0;
            var expectedKurt = -2.8m;

            var inputReturns = new decimal[]{0,1,0,1,0,1,0,1}; 

            var calculator = new StatisticsCalculations();

            //act
            var stats = calculator.GetStatistics(inputReturns);

            //assert
            Assert.IsTrue(Math.Abs(expectedMean - stats.Statistics.Where(s => s.Text == "Mean").First().Value) < .0001m );
            Assert.IsTrue(Math.Abs(expectedStdev - stats.Statistics.Where(s => s.Text == "Stdev").First().Value) < .0001m);
            Assert.IsTrue(Math.Abs(expectedSkew - stats.Statistics.Where(s => s.Text == "Skew").First().Value) < .0001m);
            Assert.IsTrue(Math.Abs(expectedKurt - stats.Statistics.Where(s => s.Text == "Kurt").First().Value) < .0001m);
        }
    }
}
