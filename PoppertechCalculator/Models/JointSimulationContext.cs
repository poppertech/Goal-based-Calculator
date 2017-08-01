using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class JointSimulationContext
    {
        public IList<int> ParentAreaNumber { get; set; }
        public decimal GlobalXMin { get; set; }
        public decimal GlobalXMax { get; set; }
        public IList<decimal> ConditionalLeftTailSimulations { get; set; }
        public IList<decimal> ConditionalLeftNormalSimulations { get; set; }
        public IList<decimal> ConditionalRightNormalSimulations { get; set; }
        public IList<decimal> ConditionalRightTailSimulations { get; set; }
    }
}