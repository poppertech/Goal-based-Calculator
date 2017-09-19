using PoppertechCalculator.Logic.Interfaces.ForecastGraph;
using PoppertechCalculator.Models;
using PoppertechCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Logic.Processors.ForecastGraph
{
    public class ForecastProcessor : IForecastProcessor
    {
        private IForecastVariableRepository _repository;

        public ForecastProcessor(IForecastVariableRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ForecastVariable> GetForecastVariables()
        {
           ForecastVariable variable;
           ForecastRegion region;
           var forecastDto = _repository.GetForecastVariables();
           var variables = new List<ForecastVariable>();
           foreach (var dto in forecastDto)
           {
               var variableName = dto.Variable;
               if (!variables.Select(v => v.Name).Contains(variableName))
               {
                   variable = new ForecastVariable { Name = variableName, Regions = new List<ForecastRegion>() };
               }
               else
               {
                   variable = variables.Where(v => v.Name == variableName).First();
               }

               var regionName = dto.Region;
               if (!variable.Regions.Select(r => r.Name).Contains(regionName))
               {
                   region = new ForecastRegion { Name = regionName, Forecast = new Forecast() };
               }
               else
               {
                   region = variable.Regions.Where(r => r.Name == regionName).First();
               }

               switch (dto.ForecastType)
               {
                   case "Minimum":
                       region.Forecast.Minimum = dto.Forecast;
                       break;
                   case "Worst":
                       region.Forecast.Worst = dto.Forecast;
                       break;
                   case "Likely":
                       region.Forecast.Likely = dto.Forecast;
                       break;
                   case "Best":
                       region.Forecast.Best = dto.Forecast;
                       break;
                   case "Maximum":
                       region.Forecast.Maximum = dto.Forecast;
                       break;
                   default:
                       throw new ArgumentOutOfRangeException("ForecastType", dto.ForecastType, "The Forecast Type from the database is missing or unknown");
               }
           }
           return variables;
        }
    }
}