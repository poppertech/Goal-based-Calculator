using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class CumulativeReturnsCalculator : ICumulativeReturnsCalculator
    {
        public decimal[,] CalculateTimeSeriesReturns(decimal initialPrice, decimal[] simulations, int numCashFlows)
        {
            var singlePeriodCumReturns = CalculateSinglePeriodCumulativeReturns(initialPrice, simulations);
            var timeSeries = InitializeTimeSeriesCumulativeReturns(singlePeriodCumReturns, numCashFlows);
            timeSeries = CalculateTimeSeriesCumulativeReturns(timeSeries, singlePeriodCumReturns);
            return timeSeries;
        }

        private static decimal[] CalculateSinglePeriodCumulativeReturns(decimal initialPrice, decimal[] simulations)
        {
            var singlePeriodCumReturns = new decimal[simulations.Length];
            for (int cnt = 0; cnt < simulations.Length; cnt++)
            {
                singlePeriodCumReturns[cnt] = simulations[cnt] / initialPrice;
            }
            return singlePeriodCumReturns;
        }

        private static decimal[,] InitializeTimeSeriesCumulativeReturns(decimal[] singlePeriodCumReturns, int numCashFlows)
        {
            var numTimeSeriesSimulations = singlePeriodCumReturns.Length / numCashFlows;
            var timeSeries = new decimal[numCashFlows, numTimeSeriesSimulations];
            for (int cntTimeSeriesSimulations = 0; cntTimeSeriesSimulations < numTimeSeriesSimulations; cntTimeSeriesSimulations++)
            {
                timeSeries[0, cntTimeSeriesSimulations] = singlePeriodCumReturns[cntTimeSeriesSimulations];
            }
            return timeSeries;
        }

        private static decimal[,] CalculateTimeSeriesCumulativeReturns(decimal[,] timeSeries, decimal[] singlePeriodCumReturns)
        {
            var numTimeSeriesSimulations = timeSeries.GetUpperBound(0) + 1;
            var numCashFlows = timeSeries.GetUpperBound(1) + 1;
            for (int cntCashFlows = 1; cntCashFlows < numCashFlows; cntCashFlows++)
            {
                for (int cntTimeSeriesSimulations = 0; cntTimeSeriesSimulations < numTimeSeriesSimulations; cntTimeSeriesSimulations++)
                {
                    var index = cntCashFlows*numTimeSeriesSimulations + cntTimeSeriesSimulations;
                    timeSeries[cntCashFlows, cntTimeSeriesSimulations] = timeSeries[cntCashFlows - 1, cntTimeSeriesSimulations] * singlePeriodCumReturns[index];
                }
            }
            return timeSeries;
        }

    }
}