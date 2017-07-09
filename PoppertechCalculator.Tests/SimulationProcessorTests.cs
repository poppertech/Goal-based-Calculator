﻿using System;
using System.Linq;
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
            // TODO: change the forecasts to models on the ui

            //arrange
            var xMin = 20;
            var xMax = 170;
            var investmentName = "GDP";

            var mean = 5;
            var stats = new Statistics{Mean = mean};

            var jointSimulations = new decimal[] { 122.2399811m, 97.44055169m, 41.76929575m, 122.3667352m };

            var forecast = new Forecast{ 
                Minimum  = 40, 
                Worst = 75, 
                Likely = 100, 
                Best = 130,
                Maximum = 150
            };

            var region = new ForecastRegion { Forecast = forecast };
            var variable = new ForecastVariable{
                Name=investmentName, 
                Regions = new[]{region}
            };
            var request = new[] { variable };

            var interval = 20;
            var frequency = .1m;
            var histogramDatum = new HistogramDatum{Interval = interval, Frequency = frequency};
            var histogramData = new[] { histogramDatum };

            var statisticsCalculations = new Mock<IStatisticsCalculations>();
            statisticsCalculations.Setup(r => r.GetStatistics(It.IsAny<decimal[]>())).Returns(stats);

            var histogramCalculations = new Mock<IHistogramCalculations>();
            histogramCalculations.Setup(r => r.GetHistogramData(It.IsAny<decimal[]>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(histogramData);

            var jointSimulator = new Mock<IJointSimulator>();
            jointSimulator.Setup(j => j.CalculateJointSimulations(It.IsAny<ForecastRegion[]>())).Returns(jointSimulations);
            jointSimulator.Setup(j => j.GetGlobalXMin()).Returns(xMin);
            jointSimulator.Setup(j => j.GetGlobalXMax()).Returns(xMax);

            var processor = new SimulationProcessor(statisticsCalculations.Object, histogramCalculations.Object, jointSimulator.Object);

            //act
            var results = processor.SimulateInvestments(request);
            var result = results.First();

            //assert
            Assert.AreEqual("GDP", result.InvestmentName);
            Assert.AreEqual(interval, result.HistogramsData.First().Interval);
            Assert.AreEqual(frequency, result.HistogramsData.First().Frequency);
            
            Assert.AreEqual(mean, result.Statistics.Mean);
        }
    }
}
