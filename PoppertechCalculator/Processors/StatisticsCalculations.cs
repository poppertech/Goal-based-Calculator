using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class StatisticsCalculations : IStatisticsCalculations 
    {
        private double _count;
        private double _mean, _stdev, _skew, _kurt;
        private double[] returns, deMeanedReturns;
        private const int NUMBER_DAYS_IN_YEAR = 250;
        private const int ERROR_CODE = 9999;

        public Statistics GetStatistics(decimal[] inputReturns)
        {
            returns = Array.ConvertAll(inputReturns, r => (double)r);
            _mean = returns.Average();
            _count = returns.Count();

            CalculateStdev();
            CalculateSkew();
            CalculateKurt();


            var stats = new Statistics
            {
                Mean = (decimal)_mean,
                Stdev = (decimal)_stdev,
                Skew = (decimal)_skew,
                Kurt = (decimal)_kurt
            };

            return stats;
        }


        private void CalculateStdev()
        {
                deMeanedReturns = returns.Select(rett => rett - _mean).ToArray();
                double sumSq = deMeanedReturns.Select(rett => Math.Pow(rett, 2)).Sum();
                _stdev = Math.Pow(sumSq / (_count - 1), .5);
        }

        private void CalculateSkew()
        {
            double sumCube = deMeanedReturns.Select(rett => Math.Pow(rett, 3)).Sum();
            _skew = (_count / ((_count - 1) * (_count - 2))) * (sumCube / Math.Pow(_stdev, 3));
        }

        private void CalculateKurt()
        {

                double sumPow4 = deMeanedReturns.Select(rett => Math.Pow(rett, 4)).Sum();
                double coef = (((_count) * (_count + 1)) / ((_count - 1) * (_count - 2) * (_count - 3)));
                double adjFact = (-3 * ((Math.Pow(_count - 1, 2)) / ((_count - 2) * (_count - 3))));
                _kurt = (coef * (sumPow4 / Math.Pow(_stdev, 4)) + adjFact);

        }
        
    }
}