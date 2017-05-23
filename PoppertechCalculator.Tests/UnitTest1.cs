using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Repositories;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetUniformRandByTickerReturnsOnSuccessReturnsRand()
        {
            // INTEGRATION TEST

            //arrange
            var connString = System.Configuration.ConfigurationManager.ConnectionStrings["ProbicastCalculator"].ConnectionString;
            var repository = new UniformRandomRepository(connString);

            //act
            var result = repository.GetUniformRandByTicker("gdp");

        }
    }
}
