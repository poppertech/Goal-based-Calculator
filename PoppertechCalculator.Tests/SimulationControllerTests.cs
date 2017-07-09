using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using System.Collections.Generic;
using Moq;
using PoppertechCalculator.Processors;
using PoppertechCalculator.Controllers;
using System.Web.Http.Results;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class SimulationControllerTests
    {
        [TestMethod]
        public void PostOnSuccessReturnsSimulationResults()
        {
            //arrange
            var investmentName = "GDP";
            var forecastVariable = new ForecastVariable(){Name = investmentName};
            var request = new[] { forecastVariable };

            var simulationResult = new SimulationResults() { InvestmentName = investmentName };
            var simulationResults = new[] { simulationResult };

            var processor = new Mock<ISimulationProcessor>();
            processor.Setup(p => p.SimulateInvestments(It.IsAny<IEnumerable<ForecastVariable>>())).Returns(simulationResults);

            var controller = new SimulationController(processor.Object);
            
            //act
            var response = controller.Post(request) as OkNegotiatedContentResult<Response<IEnumerable<SimulationResults>>>;

            //assert
            Assert.AreEqual(investmentName, response.Content.Model.First().InvestmentName);
        }
    }
}
