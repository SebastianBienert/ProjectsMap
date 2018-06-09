﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace ProjectsMap.WebApi.Migrations
{
    public class DataContextConfiguration : DbConfiguration
    {
        public DataContextConfiguration()
        {
            SetExecutionStrategy(
                "System.Data.SqlClient",
                () => new SqlAzureExecutionStrategy(2, TimeSpan.FromSeconds(600)));
        }
    }
}