using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Processors
{
    public interface IJointSimulator
    {
        decimal GetGlobalXMin();
        decimal GetGlobalXMax();
        int[] GetParentAreaNumbers();
        decimal[] CalculateUnconditionalSimulations(string variable, Forecast forecast);
        decimal[] CalculateJointSimulations(int[] parentAreaNumbers, string variable, ForecastRegion[] regions);        
    }
}
