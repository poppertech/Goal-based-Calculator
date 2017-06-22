﻿using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppertechCalculator.Processors
{
    public interface IHistogramCalculations
    {
        IEnumerable<HistogramData> GetHistogramData(JointSimulationContext context, decimal[] jointSimulations);
    }
}
