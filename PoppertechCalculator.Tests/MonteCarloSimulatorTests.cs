using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using PoppertechCalculator.Processors;
using System.Linq;
using Moq;
using PoppertechCalculator.Repositories;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class MonteCarloSimulatorTests
    {
        [TestMethod]
        public void CalculateSimulationsOnSuccessReturnsCorrectResults()
        {
            //arrange
            var expectedResult = new[] { 69.91519761m, 85.78603331m, 114.727512m, 142.570788m };

            var uniformRands = new[]{
                new UniformRandom{Rand = 7.30546161731345m},
                new UniformRandom{Rand = 19.7291475345555m},
                new UniformRandom{Rand = 70.4361363005224m},
                new UniformRandom{Rand = 98.6201702283365m}
            };

            var context = new[]{
                new SimulationContext{AreaLower=0,XLower= 40, Slope= 0.016326531m, Intercept = -0.653061224m},
                new SimulationContext{AreaLower=10,XLower= 75, Slope= 0.061298701m, Intercept = -4.025974026m},
                new SimulationContext{AreaLower= 43.44155844m,XLower= 100, Slope= -0.036796537m, Intercept = 5.783549784m},
                new SimulationContext{AreaLower= 90,XLower= 130, Slope= -0.05m, Intercept = 7.5m}
            };

            var repository = new Mock<IUniformRandomRepository>();
            repository.Setup(r => r.GetUniformRandByRegion(It.IsAny<string>())).Returns(uniformRands);

            var simulator = new MonteCarloSimulator(repository.Object);

            //act
            simulator.CalculateSimulations(context, "LeftTail");
            var result = simulator.GetSimulations().ToArray();

            //assert
            Assert.IsTrue(Math.Abs(result[0] - expectedResult[0])< .01m);
            Assert.IsTrue(Math.Abs(result[1] - expectedResult[1]) < .01m);
            Assert.IsTrue(Math.Abs(result[2] - expectedResult[2]) < .01m);
            Assert.IsTrue(Math.Abs(result[3] - expectedResult[3]) < .01m);
        }
    }
}
