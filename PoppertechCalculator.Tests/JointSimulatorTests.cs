using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using Moq;
using PoppertechCalculator.Processors;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class JointSimulatorTests
    {
        [TestMethod]
        public void CalculateUnConditionalSimulationsOnSuccessReturnsMonteCarloResults()
        {
            //arrange
            var variable = "GDP";
            var forecast = new Forecast();
            var simulationContext = new SimulationContext();
            var simulationContexts = new[] { simulationContext };
            var areaNumber = 1;
            var simulation = 100m;
            var expectedResult = new MonteCarloResults { AreaNumbers = new[] { areaNumber }, Simulations = new[]{ simulation } };

            var forecastGraphCalcs = new Mock<IForecastGraphCalculations>();
            forecastGraphCalcs.Setup(f => f.GetSimulationContext(It.IsAny<Forecast>())).Returns(simulationContexts);

            var monteCarloSimulator = new Mock<IMonteCarloSimulator>();
            monteCarloSimulator.Setup(s => s.CalculateSimulations(It.IsAny<IEnumerable<SimulationContext>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResult);

            var jointSimulator = new JointSimulator(forecastGraphCalcs.Object, monteCarloSimulator.Object);

            //act
            var actualResult = jointSimulator.CalculateUnconditionalSimulations(variable, forecast);

            //assert
            Assert.AreEqual(areaNumber, actualResult.AreaNumbers[0]);
            Assert.AreEqual(simulation, actualResult.Simulations[0]);
        }


        [TestMethod]
        public void CalculateJointSimulationsOnSuccessReturnsMonteCarloResults()
        {
            //arrange
            var variable = "GDP";
            var leftTail = new ForecastRegion {Name = "LeftTail" };
            var leftNormal = new ForecastRegion { Name = "LeftNormal" };
            var rightNormal = new ForecastRegion { Name = "RightNormal" };
            var rightTail = new ForecastRegion { Name = "RightTail" };
            var regions = new[] { leftTail, leftNormal, rightNormal, rightTail };
            var simulationContext = new SimulationContext();
            var simulationContexts = new[] { simulationContext };
            var parentAreaNumbers = new[] { 0, 1, 2, 3 };
            var expectedResult1 = 100m;
            var expectedResult2 = 95m;
            var expectedResult3 = 105m;
            var expectedResult4 = 110m;
            var expectedResult = new MonteCarloResults { Simulations = new[] { expectedResult1, expectedResult2, expectedResult3, expectedResult4 } };

            var forecastGraphCalcs = new Mock<IForecastGraphCalculations>();
            forecastGraphCalcs.Setup(f => f.GetSimulationContext(It.IsAny<Forecast>())).Returns(simulationContexts);

            var monteCarloSimulator = new Mock<IMonteCarloSimulator>();
            monteCarloSimulator.Setup(s => s.CalculateSimulations(It.IsAny<IEnumerable<SimulationContext>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResult);

            var jointSimulator = new JointSimulator(forecastGraphCalcs.Object, monteCarloSimulator.Object);

            //act
            var actualResult = jointSimulator.CalculateJointSimulations(parentAreaNumbers, variable, regions);

            //assert
            Assert.AreEqual(expectedResult1, actualResult.Simulations[0]);
            Assert.AreEqual(expectedResult2, actualResult.Simulations[1]);
            Assert.AreEqual(expectedResult3, actualResult.Simulations[2]);
            Assert.AreEqual(expectedResult4, actualResult.Simulations[3]);
        }
    }
}
