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
        public void GetUniformRandByRegionReturnsOnSuccessReturnsRand()
        {
            // INTEGRATION TEST

            //arrange
            var repository = new UniformRandomRepository();

            //act
            var result = repository.GetUniformRandByRegion("LeftTail");

        }
    }
}
