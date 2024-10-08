using System.ComponentModel.DataAnnotations;

namespace BudgetAnalysisDbApi.Models
{
    public class Month
    {
        [Key]
        public int Id { get; set; }

        public string MonthName { get; set; }
    }
}
