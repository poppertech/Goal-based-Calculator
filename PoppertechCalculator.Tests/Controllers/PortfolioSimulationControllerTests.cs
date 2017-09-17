using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using PoppertechCalculator.Processors;
using PoppertechCalculator.Models;
using PoppertechCalculator.Controllers;
using System.Web.Http.Results;
using System.Linq;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class PortfolioSimulationControllerTests
    {
        [TestMethod]
        public void GetOnSuccessReturnsProbabilityChartData()
        {
            //arrange

            var key = "Year 1";
            var value = .5m;
            var expectedChartData = new Dictionary<string, decimal>() { { key, value } };

            var processor = new Mock<IGoalAttainmentProcessor>();
            processor.Setup(p => p.GetGoalAttainmentChartData()).Returns(expectedChartData);

            var controller = new PortfolioSimulationController(processor.Object);

            //act
            var response = controller.Get() as OkNegotiatedContentResult<Response<IDictionary<string, decimal>>>;

            //assert
            Assert.AreEqual(key, response.Content.Model.Keys.First());
            Assert.AreEqual(value, response.Content.Model.Values.First());
        }

        [TestMethod]
        public void PostOnSuccessReturnsProbabilityChartData()
        {
            //arrange
            var request = new GoalAttainmentContext();

            var key = "Year 1";
            var value = .5m;
            var expectedChartData = new Dictionary<string, decimal>() { { key, value } };

            var processor = new Mock<IGoalAttainmentProcessor>();
            processor.Setup(p => p.CalculateGoalAttainmentChartData(It.IsAny<GoalAttainmentContext>())).Returns(expectedChartData);

            var controller = new PortfolioSimulationController(processor.Object);

            //act
            var response = controller.Post(request) as OkNegotiatedContentResult<Response<IDictionary<string, decimal>>>;

            //assert
            Assert.AreEqual(key, response.Content.Model.Keys.First());
            Assert.AreEqual(value, response.Content.Model.Values.First());
        }
    }
}
