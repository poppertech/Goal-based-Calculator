using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class ForecastVariableDTO
    {
        public int Id { get; set; }
        public int VariableId { get; set; }
        public int ForecastId { get; set; }
        public string Variable { get; set; }
        public string Region { get; set; }
        public string ForecastType { get; set;}
        public decimal Forecast { get; set; }
        public string Parent { get; set; }
    }
}