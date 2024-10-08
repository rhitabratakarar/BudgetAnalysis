using BudgetAnalysisDbApi.Interfaces;

namespace BudgetAnalysisDbApi.Classes
{
    public class GoogleSheetDataExtractor : IDataExtractor
    {
        public IList<IList<string>>? Data { get; set; }

        public GoogleSheetDataExtractor(IList<IList<string>> data)
        {
            this.Data = data;
        }

        public IDictionary<string, string> GetColumnSpecificData(string headerColumn, string valueColumn)
        {
            throw new NotImplementedException();
        }

        private int GetColumnHeaderIndex(string columnHeader)
        {
            throw new NotImplementedException();
        }
    }
}
