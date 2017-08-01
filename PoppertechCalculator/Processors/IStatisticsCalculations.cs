using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Processors
{
    public interface IStatisticsCalculations
    {
        Statistics GetStatistics(IEnumerable<decimal> inputReturns);
    }
}
