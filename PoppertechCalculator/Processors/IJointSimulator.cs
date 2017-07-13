using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Processors
{
    public interface IJointSimulator
    {
        MonteCarloResults CalculateUnconditionalSimulations(string variable, Forecast forecast);
        HistogramContext CalculateJointSimulations(int[] parentAreaNumbers, string variable, ForecastRegion[] regions);        
    }
}
