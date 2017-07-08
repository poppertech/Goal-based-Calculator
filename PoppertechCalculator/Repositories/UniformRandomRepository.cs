﻿using PoppertechCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PoppertechCalculator.Repositories
{
    public class UniformRandomRepository: DbContext, IUniformRandomRepository
    {
        public UniformRandomRepository(string connString):base(connString){
            Database.SetInitializer<UniformRandomRepository>(null);
        }
        
        public DbSet<UniformRandom> Rands { get; set; }

        public virtual IEnumerable<UniformRandom> GetUniformRandByRegion(RegionName region)
        {
            var regionParameter = new SqlParameter("@Region", region);
            return this.Database.SqlQuery<UniformRandom>("[Simulation].[GetUniformRandByRegion] @Region", regionParameter).ToArray();
        }

    }
}