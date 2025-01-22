namespace BudgetAnalysisDbApi.DTO
{
    public class ExpenseDTO
    {
        public string? YearCode { get; set; }
        public string? Month { get; set; }
        public string? ExpenseType { get; set; }
        public string? ExpenseName { get; set; }
        public string? ExpenseCost { get; set; }
    }
}
