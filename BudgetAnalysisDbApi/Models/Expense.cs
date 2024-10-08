namespace BudgetAnalysisDbApi.Models
{
    public class Expense
    {
        public Year YearId { get; set; }
        public Month MonthId { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public double ExpenseCost { get; set; }
    }
}
