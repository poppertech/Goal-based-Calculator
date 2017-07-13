using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class HistogramContext
    {
        public decimal[] Simulations { get; set; }
        public decimal GlobalXMin { get; set; }
        public decimal GlobalXMax { get; set; }
    }
}