using PoppertechCalculator.Models;
using PoppertechCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Processors
{
    public class SimulationProcessor
    {
        private IUniformRandomRepository _repository;
        private IForecastGraphCalculations _forecastGraphCalcs;
        private IMonteCarloSimulator _monteCarloSimulator;
        private IStatisticsCalculations _statisticsCalculations;

        public SimulationProcessor(IUniformRandomRepository repository, IForecastGraphCalculations forecastGraphCalcs, IMonteCarloSimulator monteCarloSimulator, IStatisticsCalculations statisticsCalculations)
        {
            _repository = repository;
            _forecastGraphCalcs = forecastGraphCalcs;
            _monteCarloSimulator = monteCarloSimulator;
            _statisticsCalculations = statisticsCalculations;
        }

        public bool ReturnTrue(string test)
        {
            var rand = _repository.GetUniformRandByRegion(test);
            if (rand.Any())
                return true;
            return false;
        }

        public IEnumerable<InvestmentStatistics> SimulateInvestments(IEnumerable<ForecastVariable> request)
        {
            var forecasts = request.ToArray();
            var investmentStats = new InvestmentStatistics[forecasts.Length];
            for (int cnt = 0; cnt < forecasts.Length; cnt++)
			{
			    var forecast = forecasts[cnt];
                var investmentStat = SimulateInvestment(forecast);
                investmentStats[cnt] = investmentStat;
			}
            return investmentStats;
        }

        private InvestmentStatistics SimulateInvestment(ForecastVariable forecast)
        {
            var regions = forecast.Regions.ToArray();
            var jointContext = new JointSimulationContext();
            for (int cnt = 0; cnt < regions.Length; cnt++)
            {
                var region = regions[cnt];

                var context = _forecastGraphCalcs.GetSimulationContext(region.Forecast);
                var rand = _repository.GetUniformRandByRegion(forecast.Name);
                _monteCarloSimulator.CalculateSimulations(context, rand);
                InitializeJointContext(region.Name, jointContext, _monteCarloSimulator);
            }
            var jointSimulations = CalculateJointSimulations(jointContext);
            var statistics = _statisticsCalculations.GetStatistics(jointSimulations);
            return statistics;
        }

        private void InitializeJointContext(string regionName, JointSimulationContext jointContext, IMonteCarloSimulator monteCarloSimulator)
        {
            switch (regionName)
            {
                case "all":
                    jointContext.UnconditionalAreaNumber = monteCarloSimulator.AreaNumbers;
                    break;
                case "left_tail":
                    jointContext.ConditionalLeftTailSimulations = monteCarloSimulator.Simulations;
                    break;
                case "left_normal":
                    jointContext.ConditionalLeftNormalSimulations = monteCarloSimulator.Simulations;
                    break;
                case "right_normal":
                    jointContext.ConditionalRightNormalSimulations = monteCarloSimulator.Simulations;
                    break;
                case "right_tail":
                    jointContext.ConditionalRightTailSimulations = monteCarloSimulator.Simulations;
                    break;
            }
        }

        private decimal[] CalculateJointSimulations(JointSimulationContext jointContext)
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