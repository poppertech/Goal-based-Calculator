using PoppertechCalculator.Models.Portfolio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Repositories
{
    public class PortfolioResultsRepository : DbContext, IPortfolioResultsRepository
    {
        public PortfolioResultsRepository()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["ProbicastCalculator"].ConnectionString)
        {
            Database.SetInitializer<PortfolioResultsRepository>(null);
        }

        public DbSet<PortfolioResult> Results { get; set; }

        public virtual IDictionary<string, decimal> GetPortfolioResults()
        {
            return this.Database.SqlQuery<PortfolioResult>("[Simulation].[GetPortfolioResults]").ToDictionary(r => r.Year, r => r.Probability);
        }
    }
}