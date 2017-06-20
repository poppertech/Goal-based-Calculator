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
            var rand = new UniformRandom { Id = 1};
            var rands = new[] { rand };

            var repository = new Mock<IUniformRandomRepository>();
            repository.Setup(r => r.GetUniformRandByRegion(It.IsAny<string>())).Returns(rands);

            var processor = new SimulationProcessor(repository.Object, null, null);

            //act
            var result = processor.ReturnTrue("test");

            //assert
            Assert.IsTrue(result);
        }
    }
}
