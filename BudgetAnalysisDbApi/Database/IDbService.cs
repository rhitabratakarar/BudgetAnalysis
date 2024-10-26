using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.Models;

namespace BudgetAnalysisDbApi.Database
{
    public interface IDbService
    {
        public string GetConnectionString();
        public Task<bool> SaveSyncToolData(SheetExpensesMarshalledData marshalledData);
        public Task<IList<Month>> GetMonthList();
        public Task<IList<Year>> GetYearList();
    }
}
