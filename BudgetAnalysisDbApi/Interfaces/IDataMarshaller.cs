using BudgetAnalysisDbApi.DTO;

namespace BudgetAnalysisDbApi.Interfaces
{
    public interface IDataMarshaller
    {
        public IDictionary<string, string> GetData(InsertionDataRequest insertionDataRequest);
    }
}
