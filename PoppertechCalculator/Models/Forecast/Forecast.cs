using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Models
{
    public class Forecast
    {
        [Range(0,1000)]
        public decimal Minimum { get; set; }
        [Range(0, 1000)]
        public decimal Worst { get; set; }
        [Range(0, 1000)]
        public decimal Likely { get; set; }
        [Range(0, 1000)]
        public decimal Best { get; set; }
        [Range(0, 1000)]
        public decimal Maximum { get; set; }
    }
}
