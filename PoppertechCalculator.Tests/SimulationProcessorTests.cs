using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using Moq;
using PoppertechCalculator.Repositories;
using PoppertechCalculator.Processors;
using System.Collections.Generic;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class SimulationProcessorTests
    {
        [TestMethod]
        public void SimulateInvestmentsOnSuccessReturnsInvestmentStatistics()
        {
            //arrange

            var investmentName = "GDP";
            var childInvestmentName = "Stocks";

            var mean = 5;
            var stats = new Statistics{Mean = mean};

            var jointSimulations = new decimal[] { 122.2399811m, 97.44055169m, 41.76929575m, 122.3667352m };
            var monteCarloResults = new MonteCarloResults { Simulations = jointSimulations, AreaNumbers = new[] { 1 } };

            var forecast = new Forecast{ 
                Minimum  = 40, 
                Worst = 75, 
                Likely = 100, 
                Best = 130,
                Maximum = 150
            };

            var region = new ForecastRegion { Forecast = forecast };
            
            var variable1 = new ForecastVariable{
                Name=investmentName, 
                Regions = new[]{region}
            };

            var variable2 = new ForecastVariable
            {
                Name = childInvestmentName,
                Parent = investmentName,
                Regions = new[] { region}
            };

            var request = new[] { variable1, variable2 };

            var interval = 20;
            var frequency = .1m;
            var histogramDatum = new HistogramDatum{Interval = interval, Frequency = frequency};
            var histogramData = new[] { histogramDatum };

            var statisticsCalculations = new Mock<IStatisticsCalculations>();
            statisticsCalculations.Setup(r => r.GetStatistics(It.IsAny<IEnumerable<decimal>>())).Returns(stats);

            var histogramCalculations = new Mock<IHistogramCalculations>();
            histogramCalculations.Setup(r => r.GetHistogramData(It.IsAny<HistogramContext>())).Returns(histogramData);

            var jointSimulator = new Mock<IJointSimulator>();
            jointSimulator.Setup(j => j.CalculateJointSimulations(It.IsAny<IList<int>>(), It.IsAny<string>(), It.IsAny<IList<ForecastRegion>>())).Returns(monteCarloResults);
            jointSimulator.Setup(j => j.CalculateUnconditionalSimulations(It.IsAny<string>(), It.IsAny<Forecast>())).Returns(monteCarloResults);

            var processor = new SimulationProcessor(statisticsCalculations.Object, histogramCalculations.Object, jointSimulator.Object);

            //act
            var results = processor.SimulateInvestments(request);
            var result1 = results.First();
            var result2 = results.Last();

            //assert
            Assert.AreEqual(investmentName, result1.InvestmentName);
            Assert.AreEqual(interval, result1.HistogramsData.First().Interval);
            Assert.AreEqual(frequency, result1.HistogramsData.First().Frequency);
            Assert.AreEqual(mean, result1.Statistics.Mean);


            Assert.AreEqual(childInvestmentName, result2.InvestmentName);
            Assert.AreEqual(interval, result2.HistogramsData.First().Interval);
            Assert.AreEqual(frequency, result2.HistogramsData.First().Frequency);
            Assert.AreEqual(mean, result2.Statistics.Mean);
        }
    }
}
