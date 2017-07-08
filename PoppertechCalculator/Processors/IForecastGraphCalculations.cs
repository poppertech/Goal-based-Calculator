﻿using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppertechCalculator.Processors
{
    public interface IForecastGraphCalculations
    {
        SimulationContext[] GetSimulationContext(Forecast forecast);
        decimal GetXMin();
        decimal GetXMax();
    }
}
