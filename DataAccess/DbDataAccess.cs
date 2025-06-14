using DataAccess.Context;

namespace DataAccess
{
    public class DbDataAccess : IDbDataAccess
    {
        private readonly string _connectionString;
        private readonly BudgetAnalysisDbContext dbContext;

        public DbDataAccess(string connectionString)
        {
            this._connectionString = connectionString;
            this.dbContext = new BudgetAnalysisDbContext(connectionString);
        }

        public bool SaveToDatabaseFromTool()
        {
            Console.WriteLine(this._connectionString);
            return true;
        }
    }
}
