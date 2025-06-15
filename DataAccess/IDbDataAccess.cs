using DataAccess.Context;

namespace DataAccess
{
    public interface IDbDataAccess
    {
        BudgetAnalysisDbContext DbContext { get; }
    }
}
