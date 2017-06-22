﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Models
{
    public class JointSimulationContext
    {
        public int[] UnconditionalAreaNumber { get; set; }
        public decimal XMin { get; set; }
        public decimal XMax { get; set; }
        public decimal[] ConditionalLeftTailSimulations { get; set; }
        public decimal[] ConditionalLeftNormalSimulations { get; set; }
        public decimal[] ConditionalRightNormalSimulations { get; set; }
        public decimal[] ConditionalRightTailSimulations { get; set; }
    }
}