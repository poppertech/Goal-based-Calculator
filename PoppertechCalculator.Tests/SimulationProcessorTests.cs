using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using Moq;
using PoppertechCalculator.Repositories;
using PoppertechCalculator.Processors;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class SimulationProcessorTests
    {
        [TestMethod]
        public void SimulateInvestmentsOnSuccessReturnsInvestmentStatistics()
        {
            //arrange
            var investmentName = "gdp";
            var statistics = new InvestmentStatistics(){Investment = investmentName};
            var xMin = 20;
            var xMax = 170;


            var intervalText = "Interval";
            var histogramInterval = new TextValuePair<decimal>{Text = intervalText};
            var histogramDatum = new HistogramData{Interval = histogramInterval};
            var histogramData = new[] { histogramDatum };

            var statisticsCalculations = new Mock<IStatisticsCalculations>();
            statisticsCalculations.Setup(r => r.GetStatistics(It.IsAny<decimal[]>())).Returns(statistics);

            var histogramCalculations = new Mock<IHistogramCalculations>();
            histogramCalculations.Setup(r => r.GetHistogramData(It.IsAny<decimal[]>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(histogramData);



            var processor = new SimulationProcessor(statisticsCalculations.Object, histogramCalculations.Object, null);

            //act
            var result = processor.SimulateInvestments(null);

            //assert
            Assert.IsNull(result);
        }
    }
}
