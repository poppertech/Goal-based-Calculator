using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoppertechCalculator.Models;
using PoppertechCalculator.Processors;

namespace PoppertechCalculator.Tests
{
    [TestClass]
    public class ForecastGraphCalculationsTests
    {
        [TestMethod]
        public void GetSimulationContextOnSuccessReturnsCorrectResults()
        {
            //arrange
            decimal xMin = 40,  xWorst = 75, xLikely = 100, xBest = 130, xMax = 150;
            decimal leftTail = 10, leftNormal = 33.44m, rightTail = 10;
            decimal m1 = .016m, m2 = .061m, m3 = -.036m, m4 = -.05m;
            decimal b1 = -.653m, b2 = -4.025m, b3 = 5.783m, b4 = 7.5m;

            var forecast = new[]{
                new TextValuePair<decimal>{Text = "Minimum", Value = xMin},
                new TextValuePair<decimal>{Text = "Worst Case", Value = xWorst},
                new TextValuePair<decimal>{Text = "Most Likely", Value = xLikely},
                new TextValuePair<decimal>{Text = "Best Case", Value = xBest},
                new TextValuePair<decimal>{Text = "Maximum", Value = xMax},
            };

            var calculator = new ForecastGraphCalculations();

            //act
            var context = calculator.GetSimulationContext(forecast);

            //assert
            Assert.IsTrue(Math.Abs(context[0].XLower - xMin) < .01m);
            Assert.IsTrue(Math.Abs(context[0].AreaLower) < .01m);
            Assert.IsTrue(Math.Abs(context[0].Intercept - b1) < .01m);
            Assert.IsTrue(Math.Abs(context[0].Slope - m1) < .01m);

            Assert.IsTrue(Math.Abs(context[1].XLower - xWorst) < .01m);
            Assert.IsTrue(Math.Abs(context[1].AreaLower - leftTail) < .01m);
            Assert.IsTrue(Math.Abs(context[1].Intercept - b2) < .01m);
            Assert.IsTrue(Math.Abs(context[1].Slope - m2) < .01m);

            Assert.IsTrue(Math.Abs(context[2].XLower - xLikely) < .01m);
            Assert.IsTrue(Math.Abs(context[2].AreaLower - leftTail - leftNormal) < .01m);
            Assert.IsTrue(Math.Abs(context[2].Intercept - b3) < .01m);
            Assert.IsTrue(Math.Abs(context[2].Slope - m3) < .01m);

            Assert.IsTrue(Math.Abs(context[3].XLower - xBest) < .01m);
            Assert.IsTrue(Math.Abs(context[3].AreaLower - 100 + rightTail) < .01m);
            Assert.IsTrue(Math.Abs(context[3].Intercept - b4) < .01m);
            Assert.IsTrue(Math.Abs(context[3].Slope - m4) < .01m);

        }
    }
}
