namespace BudgetAnalysisDbApi.DTO
{
    public class SearchResults
    {
        public string ExpenseType { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public string ExpenseName { get; set; }
        public double ExpenseCost { get; set; }
    }
}
