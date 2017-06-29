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
        decimal[] CalculateJointSimulations(ForecastRegion[] regions);        
    }
}
