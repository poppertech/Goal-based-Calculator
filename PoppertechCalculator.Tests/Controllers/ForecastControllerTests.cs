using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PoppertechCalculator.Logic.Interfaces.ForecastGraph;
using PoppertechCalculator.Models;
using PoppertechCalculator.Controllers;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Linq;

namespace PoppertechCalculator.Tests.Controllers
{
    [TestClass]
    public class ForecastControllerTests
    {
        [TestMethod]
        public void GetForecastVariablesOnSuccessReturnsVariables()
        {
            //arrange
            var variable = new ForecastVariable(){Name = "GDP"};
            var variables = new[]{variable};

            var processor = new Mock<IForecastProcessor>();
            processor.Setup(r => r.GetForecastVariables()).Returns(variables);

            var controller = new ForecastController(processor.Object);

            //act
            var response = controller.Get() as OkNegotiatedContentResult<Response<IEnumerable<ForecastVariable>>>;

            //assert
            Assert.AreEqual(variable.Name, response.Content.Model.First().Name);
        }
    }
}
