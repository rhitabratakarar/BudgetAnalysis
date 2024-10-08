using BudgetAnalysisDbApi.Classes;

namespace BudgetAnalysisDbApi.Database
{
    public class DbService : IDbService
    {
        private readonly IConfiguration _configuration;
        private readonly BudgetAnalysisDbContext dbContext;

        public DbService(IConfiguration configuration, BudgetAnalysisDbContext dbContext)
        {
            this._configuration = configuration;
            this.dbContext = dbContext;
        }

        public string GetConnectionString()
        {
            return this._configuration.GetConnectionString("BudgetAnalysis")!;
        }

        public bool SaveSyncToolData(SheetExpensesMarshalledData marshalledData)
        {
            throw new NotImplementedException();
        }
    }
}
