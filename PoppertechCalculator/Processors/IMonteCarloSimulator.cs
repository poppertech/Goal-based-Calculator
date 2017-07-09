﻿using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppertechCalculator.Processors
{
    public interface IMonteCarloSimulator
    {
        int[] GetAreaNumbers();

        decimal[] GetSimulations();

        void CalculateSimulations(IEnumerable<SimulationContext> context, string region);
    }
}
