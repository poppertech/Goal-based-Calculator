using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class ForecastGraphCalculations : IForecastGraphCalculations
    {
        private const decimal _leftTail = 10;
        private const decimal _rightTail = 10;
        private static decimal _normal = 100 - _leftTail - _rightTail;

        private static decimal _leftNormal, _rightNormal;

        private static decimal _xMin, _xWorst, _xLikely, _xBest, _xMax;
        private static decimal _hWorst, _hLikely, _hBest;

        private static decimal _m1, _m2, _m3, _m4;
        private static decimal _b1, _b2, _b3, _b4;


        

        public IEnumerable<SimulationContext> GetSimulationContext(Forecast forecast)
        {

            _xMin = forecast.Minimum;
            _xWorst = forecast.Worst;
            _xLikely = forecast.Likely;
            _xBest = forecast.Best;
            _xMax = forecast.Maximum;

            _hWorst = CalculateWorstCaseHeight();
            _hBest = CalculateBestCaseHeight();
            _hLikely = CalculateMostLikelyHeight();

            _leftNormal = CalculateLeftNormal();
            _rightNormal = CalculateRightNormal();

            _m1 = CalculateSlope1();
            _m2 = CalculateSlope2();
            _m3 = CalculateSlope3();
            _m4 = CalculateSlope4();

            _b1 = CalculateIntercept1();
            _b2 = CalculateIntercept2();
            _b3 = CalculateIntercept3();
            _b4 = CalculateIntercept4();

            var context1 = new SimulationContext() { XLower = _xMin, AreaLower = 0, Intercept = _b1, Slope = _m1 };
            var context2 = new SimulationContext() { XLower = _xWorst, AreaLower = _leftTail, Intercept = _b2, Slope = _m2 };
            var context3 = new SimulationContext() { XLower = _xLikely, AreaLower = _leftTail + _leftNormal, Intercept = _b3, Slope = _m3 };
            var context4 = new SimulationContext() { XLower = _xBest, AreaLower = _leftTail + _normal, Intercept = _b4, Slope = _m4 };
            var context5 = new SimulationContext() { XLower = _xMax };
            return new[] { context1, context2, context3, context4, context5 };

        }


        private static decimal CalculateWorstCaseHeight()
        {
            return 2 * _leftTail / (_xWorst - _xMin);
        }

        private static decimal CalculateBestCaseHeight()
        {
            return 2 * _rightTail / (_xMax - _xBest);
        }

        private static decimal CalculateMostLikelyHeight()
        {
            return (2 * _normal - _hWorst * (_xLikely - _xWorst) - _hBest * (_xBest - _xLikely)) / (_xBest - _xWorst);
        }

        private static decimal CalculateLeftNormal()
        {
            return (_hWorst + _hLikely) * (_xLikely - _xWorst) / 2;
        }

        private static decimal CalculateRightNormal()
        {
            return (_hLikely + _hBest) * (_xBest - _xLikely) / 2;
        }

        private static decimal CalculateSlope1()
        {
            return _hWorst / (_xWorst - _xMin);
        }

        private static decimal CalculateSlope2()
        {
            return (_hLikely - _hWorst) / (_xLikely - _xWorst);
        }

        private static decimal CalculateSlope3()
        {
            return (_hBest - _hLikely) / (_xBest - _xLikely);
        }

        private static decimal CalculateSlope4()
        {
            return -_hBest / (_xMax - _xBest);
        }

        private static decimal CalculateIntercept1()
        {
            return _hWorst - (_m1 * _xWorst);
        }

        private static decimal CalculateIntercept2()
        {
            return _hLikely - (_m2 * _xLikely);
        }

        private static decimal CalculateIntercept3()
        {
            return _hLikely - (_m3 * _xLikely);
        }

        private static decimal CalculateIntercept4()
        {
            return _hBest - (_m4 * _xBest);
        }

    }
}