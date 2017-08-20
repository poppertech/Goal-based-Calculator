using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class CumulativeReturnsCalculator : ICumulativeReturnsCalculator
    {
        public decimal[,] CalculateTimeSeriesReturns(decimal initialPrice, IList<decimal> simulations, int numCashFlows)
        {
            var singlePeriodCumReturns = CalculateSinglePeriodCumulativeReturns(initialPrice, simulations);
            var timeSeries = InitializeTimeSeriesCumulativeReturns(singlePeriodCumReturns, numCashFlows);
            timeSeries = CalculateTimeSeriesCumulativeReturns(timeSeries, singlePeriodCumReturns);
            return timeSeries;
        }

        private static IList<decimal> CalculateSinglePeriodCumulativeReturns(decimal initialPrice, IList<decimal> simulations)
        {
            var numSimulations = simulations.Count();
            var singlePeriodCumReturns = new decimal[simulations.Count()];
            for (int cnt = 0; cnt < numSimulations; cnt++)
            {
                singlePeriodCumReturns[cnt] = simulations[cnt] / initialPrice;
            }
            return singlePeriodCumReturns;
        }

        private static decimal[,] InitializeTimeSeriesCumulativeReturns(IList<decimal> singlePeriodCumReturns, int numCashFlows)
        {
            var numTimeSeriesSimulations = singlePeriodCumReturns.Count() / (numCashFlows - 1);
            var timeSeries = new decimal[numCashFlows - 1, numTimeSeriesSimulations];
            for (int cntTimeSeriesSimulations = 0; cntTimeSeriesSimulations < numTimeSeriesSimulations; cntTimeSeriesSimulations++)
            {
                timeSeries[0, cntTimeSeriesSimulations] = singlePeriodCumReturns[cntTimeSeriesSimulations];
            }
            return timeSeries;
        }

        private static decimal[,] CalculateTimeSeriesCumulativeReturns(decimal[,] timeSeries, IList<decimal> singlePeriodCumReturns)
        {
            var numCashFlows = timeSeries.GetUpperBound(0) + 1;
            var numTimeSeriesSimulations = timeSeries.GetUpperBound(1) + 1;
            for (int cntCashFlows = 1; cntCashFlows < numCashFlows; cntCashFlows++)
            {
                for (int cntTimeSeriesSimulations = 0; cntTimeSeriesSimulations < numTimeSeriesSimulations; cntTimeSeriesSimulations++)
                {
                    var index = cntCashFlows*numTimeSeriesSimulations + cntTimeSeriesSimulations;
                    timeSeries[cntCashFlows, cntTimeSeriesSimulations] = singlePeriodCumReturns[index];
                }
            }
            return timeSeries;
        }

    }
}