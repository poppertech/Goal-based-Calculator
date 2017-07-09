using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Repositories
{
    public class UniformRandomRepository : DbContext, IUniformRandomRepository
    {
        public UniformRandomRepository()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["ProbicastCalculator"].ConnectionString)
        {
            Database.SetInitializer<UniformRandomRepository>(null);
        }

        public DbSet<UniformRandom> Rands { get; set; }

        public virtual IEnumerable<UniformRandom> GetUniformRands(string variable, string region)
        {
            var variableParameter = new SqlParameter("@Variable", variable);
            SqlParameter regionParameter;
            if (!string.IsNullOrWhiteSpace(region))
            {
                regionParameter = new SqlParameter("@Region", region);
                return this.Database.SqlQuery<UniformRandom>("[Simulation].[GetUniformRandByRegion] @Variable, @Region", variableParameter, regionParameter).ToArray();
            }

            return this.Database.SqlQuery<UniformRandom>("[Simulation].[GetUniformRandByRegion] @Variable", variableParameter).ToArray();
        }

    }
}