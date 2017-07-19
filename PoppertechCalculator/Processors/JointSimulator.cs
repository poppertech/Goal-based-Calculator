using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class JointSimulator : IJointSimulator
    {
        private IForecastGraphCalculations _forecastGraphCalcs;
        private IMonteCarloSimulator _monteCarloSimulator;

        public JointSimulator(IForecastGraphCalculations forecastGraphCalcs, IMonteCarloSimulator monteCarloSimulator)
        {
            _forecastGraphCalcs = forecastGraphCalcs;
            _monteCarloSimulator = monteCarloSimulator;
        }


        public MonteCarloResults CalculateUnconditionalSimulations(string variable, Forecast forecast)
        {
            var context = _forecastGraphCalcs.GetSimulationContext(forecast);
            var monteCarloResults = _monteCarloSimulator.CalculateSimulations(context, variable, null);
            return monteCarloResults;
        }

        public MonteCarloResults CalculateJointSimulations(int[] parentAreaNumbers, string variable, ForecastRegion[] regions)
        {
            var jointContext = new JointSimulationContext();
            

            jointContext.ParentAreaNumber = parentAreaNumbers;

            for (int cnt = 0; cnt < regions.Length; cnt++)
            {
                var region = regions[cnt];

                var context = _forecastGraphCalcs.GetSimulationContext(region.Forecast);
                var num = context.Count();
          
                var monteCarloResults = _monteCarloSimulator.CalculateSimulations(context, variable, region.Name);
                InitializeJointContext(region.Name, jointContext, monteCarloResults.Simulations);
            }
            var simulations = CalculateJointSimulation(jointContext);
            return new MonteCarloResults { Simulations = simulations };
        }

        private void InitializeJointContext(string regionName, JointSimulationContext jointContext, decimal[] monteCarloSimulations)
        {
            switch (regionName)
            {
                case "LeftTail":
                    jointContext.ConditionalLeftTailSimulations = monteCarloSimulations;
                    break;
                case "LeftNormal":
                    jointContext.ConditionalLeftNormalSimulations = monteCarloSimulations;
                    break;
                case "RightNormal":
                    jointContext.ConditionalRightNormalSimulations = monteCarloSimulations;
                    break;
                case "RightTail":
                    jointContext.ConditionalRightTailSimulations = monteCarloSimulations;
                    break;
            }
        }

        private decimal[] CalculateJointSimulation(JointSimulationContext jointContext)
        {
            var num = jointContext.ParentAreaNumber.Length;
            var jointSimulations = new decimal[num];

            for (int cnt = 0; cnt < num; cnt++)
            {
                var areaNum = jointContext.ParentAreaNumber[cnt];
                switch (areaNum)
                {
                    case 0:
                        jointSimulations[cnt] = jointContext.ConditionalLeftTailSimulations[cnt];
                        break;
                    case 1:
                        jointSimulations[cnt] = jointContext.ConditionalLeftNormalSimulations[cnt];
                        break;
                    case 2:
                        jointSimulations[cnt] = jointContext.ConditionalRightNormalSimulations[cnt];
                        break;
                    case 3:
                        jointSimulations[cnt] = jointContext.ConditionalRightTailSimulations[cnt];
                        break;
                }
            }
            return jointSimulations;
        }
    }
}