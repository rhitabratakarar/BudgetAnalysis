using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.DTO;

namespace BudgetAnalysisDbApi.Interfaces
{
    public interface IDataMarshaller
    {
        public SheetExpensesMarshalledData GetData(InsertionDataRequest insertionDataRequest);
    }
}
