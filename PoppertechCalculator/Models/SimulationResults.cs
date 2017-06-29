using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class SimulationResults
    {
        public IEnumerable<InvestmentStatistics> InvestmentsStatistics { get; set; }
        public IEnumerable<IEnumerable<HistogramData>> HistogramsData { get; set; }
    }
}