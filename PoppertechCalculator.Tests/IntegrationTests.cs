using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Repositories;
using PoppertechCalculator.Models;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void GetUniformRandByRegionOnSuccessReturnsRand()
        {
            // INTEGRATION TEST

            //arrange
            var repository = new UniformRandomRepository();

            //act
            var result = repository.GetUniformRands("GDP", null);

        }

        [TestMethod]
        public void GetPortfolioResultsOnSuccessReturnsResults()
        {
            // INTEGRATION TEST

            //arrange
            var repository = new PortfolioResultsRepository();

            //act
            var result = repository.GetPortfolioResults();

        }
    }
}
