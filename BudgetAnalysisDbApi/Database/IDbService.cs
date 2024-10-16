using BudgetAnalysisDbApi.Classes;

namespace BudgetAnalysisDbApi.Database
{
    public interface IDbService
    {
        public string GetConnectionString();
        public Task<bool> SaveSyncToolData(SheetExpensesMarshalledData marshalledData);
    }
}
