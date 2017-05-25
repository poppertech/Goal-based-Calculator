using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class ForecastGraphCalculations : IForecastGraphCalculations
    {
        private const decimal leftTail = 10;
        private const decimal rightTail = 10;
        private static decimal normal = 100 - leftTail - rightTail;

        private static decimal leftNormal, rightNormal;

        private static decimal xMin, xWorst, xLikely, xBest, xMax;
        private static decimal hWorst, hLikely, hBest;

        private static decimal m1, m2, m3, m4;
        private static decimal b1, b2, b3, b4;

        private SimulationContext context;

        public SimulationContext[] GetSimulationContext(IEnumerable<TextValuePair<decimal>> forecast)
        {
            var forecastArray = forecast.ToArray();
            SetParameters(forecastArray);

            hWorst = CalculateWorstCaseHeight();
            hBest = CalculateBestCaseHeight();
            hLikely = CalculateMostLikelyHeight();

            leftNormal = CalculateLeftNormal();
            rightNormal = CalculateRightNormal();

            m1 = CalculateSlope1();
            m2 = CalculateSlope2();
            m3 = CalculateSlope3();
            m4 = CalculateSlope4();

            b1 = CalculateIntercept1();
            b2 = CalculateIntercept2();
            b3 = CalculateIntercept3();
            b4 = CalculateIntercept4();

            var context1 = new SimulationContext() { XLower = xMin, AreaLower = 0, Intercept = b1, Slope = m1 };
            var context2 = new SimulationContext() { XLower = xWorst, AreaLower = leftTail, Intercept = b2, Slope = m2 };
            var context3 = new SimulationContext() { XLower = xLikely, AreaLower = leftTail + leftNormal, Intercept = b3, Slope = m3 };
            var context4 = new SimulationContext() { XLower = xBest, AreaLower = leftTail + normal, Intercept = b4, Slope = m4 };
            return new[] { context1, context2, context3, context4 };

        }

        private static void SetParameters(TextValuePair<decimal>[] forecast)
        {
            for (int cnt = 0; cnt < forecast.Length; cnt++)
            {
                var x = forecast[cnt];

                switch (x.Text)
                {
                    case "Minimum":
                        xMin = x.Value;
                        break;
                    case "Worst Case":
                        xWorst = x.Value;
                        break;
                    case "Most Likely":
                        xLikely = x.Value;
                        break;
                    case "Best Case":
                        xBest = x.Value;
                        break;
                    case "Maximum":
                        xMax = x.Value;
                        break;
                }
                
            }
        }

        private static decimal CalculateWorstCaseHeight()
        {
            return 2 * leftTail / (xWorst - xMin);
        }

        private static decimal CalculateBestCaseHeight()
        {
            return 2 * rightTail / (xMax - xBest);
        }

        private static decimal CalculateMostLikelyHeight()
        {
            return (2 * normal - hWorst * (xLikely - xWorst) - hBest * (xBest - xLikely)) / (xBest - xWorst);
        }

        private static decimal CalculateLeftNormal()
        {
            return (hWorst + hLikely) * (xLikely - xWorst) / 2;
        }

        private static decimal CalculateRightNormal()
        {
            return (hLikely + hBest) * (xBest - xLikely) / 2;
        }

        private static decimal CalculateSlope1()
        {
            return hWorst / (xWorst - xMin);
        }

        private static decimal CalculateSlope2()
        {
            return (hLikely - hWorst) / (xLikely - xWorst);
        }

        private static decimal CalculateSlope3()
        {
            return (hBest - hLikely) / (xBest - xLikely);
        }

        private static decimal CalculateSlope4()
        {
            return -hBest / (xMax - xBest);
        }

        private static decimal CalculateIntercept1()
        {
            return hWorst - (m1 * xWorst);
        }

        private static decimal CalculateIntercept2()
        {
            return hLikely - (m2 * xLikely);
        }

        private static decimal CalculateIntercept3()
        {
            return hLikely - (m3 * xLikely);
        }

        private static decimal CalculateIntercept4()
        {
            return hBest - (m4 * xBest);
        }

    }
}