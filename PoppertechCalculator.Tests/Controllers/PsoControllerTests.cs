using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models.PSO;
using Moq;
using PoppertechCalculator.Logic.Interfaces.Pso;
using PoppertechCalculator.Controllers;
using PoppertechCalculator.Models;
using System.Web.Http.Results;

namespace PoppertechCalculator.Tests.Controllers
{
    [TestClass]
    public class PsoControllerTests
    {
        [TestMethod]
        public void PostOnSuccessReturnsResponse()
        {
            //arrange
            var psoResults = new PsoResults();
            var psoContext = new PsoContext();

            var processor = new Mock<IPsoCalculationsProcessor>();
            processor.Setup(p => p.OptimizePortfolio(It.IsAny<PsoContext>())).Returns(psoResults);

            var controller = new PsoController(processor.Object);

            //act
            var result = controller.Post(psoContext) as OkNegotiatedContentResult<Response<PsoResults>>;

            //assert
            Assert.AreSame(psoResults, result.Content.Model);
        }
    }
}
