using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAnalysisDbApi.Models
{
    public class Expense
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Year")]
        public int YearId { get; set; }
        public virtual Year Year { get; set; }

        [ForeignKey("Month")]
        public int MonthId { get; set; }
        public virtual Month Month { get; set; }

        public ExpenseType ExpenseType { get; set; }
        public double ExpenseCost { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        // default date has been added to the migration with GETDATE().
        public DateTime CreatedDate { get; set; }
    }
}
