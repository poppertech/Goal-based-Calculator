using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Repositories
{
    public class ForecastVariableRepository: DbContext
    {
        public ForecastVariableRepository()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["ProbicastCalculator"].ConnectionString)
        {
            Database.SetInitializer<ForecastVariableRepository>(null);
        }

        public DbSet<ForecastVariableDTO> Forecasts { get; set; }

        public virtual IEnumerable<ForecastVariableDTO> GetForecastVariables()
        {
            return this.Database.SqlQuery<ForecastVariableDTO>(("[Simulation].[GetForecastVariables]")).ToArray();
        }
    }
}