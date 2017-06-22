using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class HistogramData
    {
        public TextValuePair<decimal> Interval { get; set; }
        public TextValuePair<decimal> Frequency { get; set; }
    }
}