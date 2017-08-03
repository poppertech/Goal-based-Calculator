using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Models
{
    public class Statistics
    {
        public decimal Mean { get; set; }
        public decimal Stdev { get; set; }
        public decimal Skew { get; set; }
        public decimal Kurt { get; set; }
    }
}
