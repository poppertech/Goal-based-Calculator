using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using Moq;
using PoppertechCalculator.Repositories;
using PoppertechCalculator.Logic.Processors.ForecastGraph;
using System.Linq;

namespace PoppertechCalculator.Tests.Processors
{
    [TestClass]
    public class ForecastProcessorTests
    {
        [TestMethod]
        public void GetForecastVariablesOnSuccessReturnsVariables()
        {
            //arrange
            var parentName = "GDP";
            var childName = "Stocks";
            var forecastType1 = "Minimum";
            var forecastType2 = "Worst";
            var region1 = "LeftTail";
            var region2 = "LeftNormal";
            var parentDto1 = new ForecastVariableDTO() { Id = 1, VariableId = 1, Variable = parentName, Parent = null, Region = null, ForecastId = 1, ForecastType = forecastType1, Forecast = .1m };
            var parentDto2 = new ForecastVariableDTO() { Id = 2, VariableId = 1, Variable = parentName, Parent = null, Region = null, ForecastId = 2, ForecastType = forecastType2, Forecast = .2m };

            var childDto1 = new ForecastVariableDTO() { Id = 3, VariableId = 2, Variable = childName, Parent = parentName, Region = region1, ForecastId = 3, ForecastType = forecastType1, Forecast = .3m };
            var childDto2 = new ForecastVariableDTO() { Id = 4, VariableId = 2, Variable = childName, Parent = parentName, Region = region2, ForecastId = 4, ForecastType = forecastType2, Forecast = .4m };

            var dtos = new[] { parentDto1, parentDto2, childDto1, childDto2 };

            var repository = new Mock<IForecastVariableRepository>();
            repository.Setup(r => r.GetForecastVariables()).Returns(dtos);

            var processor = new ForecastProcessor(repository.Object);

            //act
            var variables = processor.GetForecastVariables();
            var variable1 = variables.First();
            var variable2 = variables.Last();

            //assert
            Assert.AreEqual(parentName, variable1.Name);
            Assert.IsNull(variable1.Parent);
            Assert.IsNull(variable1.Regions.First().Name);
            Assert.AreEqual(parentDto1.Forecast, variable1.Regions.First().Forecast.Minimum);
            Assert.AreEqual(parentDto2.Forecast, variable1.Regions.First().Forecast.Worst);

            Assert.AreEqual(childName, variable2.Name);
            Assert.AreEqual(parentName, variable2.Parent);
            Assert.AreEqual(region1, variable2.Regions.First().Name);
            Assert.AreEqual(region2, variable2.Regions.Last().Name);
            Assert.AreEqual(childDto1.Forecast, variable2.Regions.First().Forecast.Minimum);
            Assert.AreEqual(childDto2.Forecast, variable2.Regions.Last().Forecast.Worst);
        }
    }
}
