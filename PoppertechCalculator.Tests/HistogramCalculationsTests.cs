using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using PoppertechCalculator.Processors;
using System.Linq;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class HistogramCalculationsTests
    {
        [TestMethod]
        public void GetHistogramDataOnSuccessReturnsHistogramData()
        {
            //arrange
            var context = new JointSimulationContext() { XMin = 20, XMax = 170};
            var simulations = new decimal[] {19, 21, 22, 24};

            var calculator = new HistogramCalculations();

            //act
            var result = calculator.GetHistogramData(context, simulations).ToArray();

            //assert
            Assert.AreEqual(20, result[0].Interval.Value);
            Assert.AreEqual(21.5m, result[1].Interval.Value);
            Assert.AreEqual(23, result[2].Interval.Value);
            Assert.AreEqual(24.5m, result[3].Interval.Value);

            Assert.AreEqual(.25m, result[0].Frequency.Value);
            Assert.AreEqual(.25m, result[1].Frequency.Value);
            Assert.AreEqual(.25m, result[2].Frequency.Value);
            Assert.AreEqual(.25m, result[3].Frequency.Value);
        }
    }
}
