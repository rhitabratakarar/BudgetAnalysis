using DataAccess.Context;

namespace DataAccess
{
    public class DbDataAccess : IDbDataAccess
    {
        private readonly BudgetAnalysisDbContext dbContext;

        public BudgetAnalysisDbContext DbContext
        {
            get
            {
                return this.dbContext;
            }
        }

        public DbDataAccess(string connectionString)
        {
            this.dbContext = new BudgetAnalysisDbContext(connectionString);
        }
    }
}
