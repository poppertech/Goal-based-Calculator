using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Repositories
{
    public interface IForecastVariableRepository
    {
        IEnumerable<ForecastVariableDTO> GetForecastVariables();
    }
}
