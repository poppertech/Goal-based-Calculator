using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Models
{
    public class Forecast
    {
        public decimal Minimum { get; set; }
        public decimal Worst { get; set; }
        public decimal Likely { get; set; }
        public decimal Best { get; set; }
        public decimal Maximum { get; set; }
    }
}
