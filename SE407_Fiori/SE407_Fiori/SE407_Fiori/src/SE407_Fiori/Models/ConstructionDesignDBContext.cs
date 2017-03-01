﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SE407_Fiori
{
    public class ConstructionDesignDBContext: DbContext
    {
        public IConfigurationRoot Configuration { get; set; }

        public DbSet<ConstructionDesign> ConstructionDesigns { get; set; }

        public ConstructionDesignDBContext()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                   Configuration.GetConnectionString("MSSQLDB"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}

