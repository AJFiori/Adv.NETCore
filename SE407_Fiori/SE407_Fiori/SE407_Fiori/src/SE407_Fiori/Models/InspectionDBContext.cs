using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace SE407_Fiori
{
    public class InspectionDBContext: DbContext
    {
        public IConfigurationRoot Configuration { get; set; }

        public DbSet<Inspection> Inspections { get; set; }

        public InspectionDBContext()
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