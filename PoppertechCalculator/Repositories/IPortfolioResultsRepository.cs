using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoppertechCalculator.Repositories
{
    public interface IPortfolioResultsRepository
    {
        IDictionary<string, decimal> GetPortfolioResults();
    }
}
