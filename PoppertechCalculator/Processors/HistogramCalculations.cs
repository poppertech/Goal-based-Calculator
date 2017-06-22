﻿using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class HistogramCalculations : IHistogramCalculations
    {
        const int num = 100;
        public IEnumerable<HistogramData> GetHistogramData(JointSimulationContext context, decimal[] jointSimulations){
            var histogramDataArray = new HistogramData[num];
            decimal lastCumulativeFrequency = 0;

            var xMin = context.XMin;
            var xMax = context.XMax;           

            for (int cnt = 0; cnt < num; cnt++)
            {
                var histogramData = new HistogramData();
                var interval = ((decimal)cnt / (decimal)num) * (xMax - xMin) + xMin;
                var textIntervalPair = new TextValuePair<decimal> { Text = "Interval", Value = interval };
                histogramData.Interval = textIntervalPair;

                var cumulativeFrequency = ((decimal)jointSimulations.Count(x => x < interval))/((decimal)jointSimulations.Length);
                var frequency = cumulativeFrequency - lastCumulativeFrequency;               
                var textFrequencyPair = new TextValuePair<decimal> { Text = "Frequency", Value = frequency };
                histogramData.Frequency = textFrequencyPair;
                lastCumulativeFrequency = cumulativeFrequency;
                histogramDataArray[cnt] = histogramData;
            }
            return histogramDataArray;
        }
    }
}