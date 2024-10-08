using BudgetAnalysisDbApi.Classes;

namespace BudgetAnalysisDbApi.Database
{
    public interface IDbService
    {
        public string GetConnectionString();
        public bool SaveSyncToolData(SheetExpensesMarshalledData marshalledData);
    }
}
