﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Processors
{
    public interface ICumulativeReturnsCalculator
    {
        decimal[,] CalculateTimeSeriesReturns(decimal initialPrice, decimal[] simulations, int numCashFlows);
    }
}
