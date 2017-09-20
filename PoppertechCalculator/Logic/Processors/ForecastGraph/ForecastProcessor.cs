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

               variable = GetForecastVariable(dto.Variable, dto.Parent, variables);
               region = GetForecastRegion(dto.Region, variable.Regions);
               var propertyInfo = region.Forecast.GetType().GetProperty(dto.ForecastType);
               propertyInfo.SetValue(region.Forecast, dto.Forecast);

           }
           return variables;
        }

        private ForecastVariable GetForecastVariable(string variableName, string parentName, IList<ForecastVariable> variables)
        {
            ForecastVariable variable;
            if (!variables.Select(v => v.Name).Contains(variableName))
            {
                variable = new ForecastVariable { Name = variableName, Parent = parentName, Regions = new List<ForecastRegion>() };
                variables.Add(variable);
            }
            else
            {
                variable = variables.Where(v => v.Name == variableName).First();
            }
            return variable;
        }

        private ForecastRegion GetForecastRegion(string regionName, IList<ForecastRegion> regions)
        {
            ForecastRegion region;
            if ((regionName == null && !regions.Any()) || !regions.Select(r => r.Name).Contains(regionName))
            {
                region = new ForecastRegion { Name = regionName, Forecast = new Forecast() };
                regions.Add(region);
            }
            else
            {
                region = regions.Where(r => r.Name == regionName).First();
            }
            return region;
        }
    }
}