using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class HistogramCalculations : IHistogramCalculations
    {
        const int num = 100;
        public IEnumerable<HistogramDatum> GetHistogramData(HistogramContext context)
        {
            var histogramDataArray = new HistogramDatum[num];
            decimal lastCumulativeFrequency = 0;       

            for (int cnt = 0; cnt < num; cnt++)
            {
                var histogramData = new HistogramDatum();
                var interval = ((decimal)cnt / (decimal)num) * (context.GlobalXMax - context.GlobalXMin) + context.GlobalXMin;
                histogramData.Interval = interval;

                var cumulativeFrequency = ((decimal)context.Simulations.Count(x => x < interval)) / ((decimal)context.Simulations.Length);
                var frequency = cumulativeFrequency - lastCumulativeFrequency;               
                histogramData.Frequency = frequency;
                lastCumulativeFrequency = cumulativeFrequency;
                histogramDataArray[cnt] = histogramData;
            }
            return histogramDataArray;
        }
    }
}