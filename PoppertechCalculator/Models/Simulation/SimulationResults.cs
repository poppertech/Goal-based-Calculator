using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class SimulationResults
    {
        public string InvestmentName { get; set; }
        public Statistics Statistics { get; set; }
        public IEnumerable<HistogramDatum> HistogramsData { get; set; }
    }
}