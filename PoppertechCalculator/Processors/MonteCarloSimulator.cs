using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class MonteCarloSimulator : IMonteCarloSimulator
    {
        private decimal[] simulations;
        private int[] areaNumbers;

        public int[] GetAreaNumbers() {return areaNumbers; }

        public decimal[] GetSimulations() { return simulations;  }

        public void CalculateSimulations(IEnumerable<SimulationContext> context, IEnumerable<UniformRandom> rands)
        {
            var randArray = rands.ToArray();
            var contextArray = context.ToArray();
            simulations = new decimal[randArray.Length];
            areaNumbers = new int[randArray.Length];
            var areaLookup = contextArray.Select(c => c.AreaLower).ToArray();
            for (int cnt = 0; cnt < randArray.Length; cnt++)
            {
                var rand = randArray[cnt];
                var areaNum = ChooseAreaNumber(rand.Rand, areaLookup);
                var mSim = contextArray[areaNum].Slope;
                var bSim = contextArray[areaNum].Intercept;
                var areaLower = contextArray[areaNum].AreaLower;
                var xLower = contextArray[areaNum].XLower;
                var c1 = mSim / 2;
                var c2 = bSim;
                var c3 = areaLower - rand.Rand - ((mSim / 2) * ((decimal)Math.Pow((double)xLower, 2)) + bSim * xLower);
                var xSim = (-c2+(decimal)Math.Sqrt(Math.Pow((double)c2,2)-(double)(4*c1*c3)))/(2*c1);
                simulations[cnt] = xSim;
                areaNumbers[cnt] = areaNum;
            }
        }

        private int ChooseAreaNumber(decimal rand, decimal[] areaLookup)
        {
            for (int cnt = 0; cnt < areaLookup.Length; cnt++)
            {
                var value = areaLookup[cnt];
                if (value >= rand)
                    return cnt - 1;
            }
            return areaLookup.Length - 1;
        }
    }
}