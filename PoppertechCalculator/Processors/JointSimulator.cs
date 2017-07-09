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

        private decimal[] _jointSimulations;

        private decimal _xMinGlobal, _xMaxGlobal;

        public JointSimulator(IForecastGraphCalculations forecastGraphCalcs, IMonteCarloSimulator monteCarloSimulator)
        {
            _forecastGraphCalcs = forecastGraphCalcs;
            _monteCarloSimulator = monteCarloSimulator;
        }

        public decimal GetGlobalXMin()
        {
            return _xMinGlobal;
        }

        public decimal GetGlobalXMax()
        {
            return _xMaxGlobal;
        }

        public decimal[] CalculateJointSimulations(ForecastRegion[] regions)
        {
            var jointContext = new JointSimulationContext();
            for (int cnt = 0; cnt < regions.Length; cnt++)
            {
                var region = regions[cnt];

                var context = _forecastGraphCalcs.GetSimulationContext(region.Forecast);

                if (cnt == 0)
                    _xMinGlobal = _forecastGraphCalcs.GetXMin();

                if (cnt == regions.Length - 1)
                    _xMaxGlobal = _forecastGraphCalcs.GetXMax();
          
                _monteCarloSimulator.CalculateSimulations(context, region.Name);
                InitializeJointContext(region.Name, jointContext, _monteCarloSimulator);
            }
            _jointSimulations = CalculateJointSimulation(jointContext);
            return _jointSimulations;
        }

        private void InitializeJointContext(string regionName, JointSimulationContext jointContext, IMonteCarloSimulator monteCarloSimulator)
        {
            switch (regionName)
            {
                case "LeftTail":
                    jointContext.ConditionalLeftTailSimulations = monteCarloSimulator.GetSimulations();
                    break;
                case "LeftNormal":
                    jointContext.ConditionalLeftNormalSimulations = monteCarloSimulator.GetSimulations();
                    break;
                case "RightNormal":
                    jointContext.ConditionalRightNormalSimulations = monteCarloSimulator.GetSimulations();
                    break;
                case "RightTail":
                    jointContext.ConditionalRightTailSimulations = monteCarloSimulator.GetSimulations();
                    break;
            }
        }

        private decimal[] CalculateJointSimulation(JointSimulationContext jointContext)
        {
            var num = jointContext.UnconditionalAreaNumber.Length;
            var jointSimulations = new decimal[num];

            for (int cnt = 0; cnt < num; cnt++)
            {
                var areaNum = jointContext.UnconditionalAreaNumber[cnt];
                switch (areaNum)
                {
                    case 1:
                        jointSimulations[cnt] = jointContext.ConditionalLeftTailSimulations[cnt];
                        break;
                    case 2:
                        jointSimulations[cnt] = jointContext.ConditionalLeftNormalSimulations[cnt];
                        break;
                    case 3:
                        jointSimulations[cnt] = jointContext.ConditionalRightNormalSimulations[cnt];
                        break;
                    case 4:
                        jointSimulations[cnt] = jointContext.ConditionalRightTailSimulations[cnt];
                        break;
                }
            }
            return jointSimulations;
        }
    }
}