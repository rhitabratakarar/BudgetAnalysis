using System.ComponentModel.DataAnnotations;

namespace BudgetAnalysisDbApi.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public Year Year { get; set; }
        public Month Month { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public double ExpenseCost { get; set; }
    }
}
