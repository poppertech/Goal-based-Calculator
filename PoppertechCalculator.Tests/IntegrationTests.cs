using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Repositories;

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
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["ProbicastCalculator"].ConnectionString;
            var repository = new UniformRandomRepository(connString);

            //act
            var result = repository.GetUniformRandByRegion("right_tail");

        }
    }
}
