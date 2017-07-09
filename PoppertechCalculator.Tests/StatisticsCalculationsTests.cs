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
            Assert.IsTrue(Math.Abs(expectedMean - stats.Mean) < .0001m );
            Assert.IsTrue(Math.Abs(expectedStdev - stats.Stdev) < .0001m);
            Assert.IsTrue(Math.Abs(expectedSkew - stats.Skew) < .0001m);
            Assert.IsTrue(Math.Abs(expectedKurt - stats.Kurt) < .0001m);
        }
    }
}
