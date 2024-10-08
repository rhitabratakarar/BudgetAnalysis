using BudgetAnalysisDbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAnalysisDbApi.Database
{
    public class BudgetAnalysisDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BudgetAnalysisDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public DbSet<Year> Years { get; set; }
        public DbSet<Month> Months { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("BudgetAnalysis"));
        }
    }
}
