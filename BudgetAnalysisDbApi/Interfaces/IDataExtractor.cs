namespace BudgetAnalysisDbApi.Interfaces
{
    public interface IDataExtractor
    {
        public IDictionary<string, string> GetColumnSpecificData(string headerColumn, string valueColumn);
    }
}
