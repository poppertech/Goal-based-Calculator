using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoppertechCalculator.Repositories
{
    public interface IUniformRandomRepository
    {
        IEnumerable<UniformRandom> GetUniformRandByRegion(string ticker);
    }
}
