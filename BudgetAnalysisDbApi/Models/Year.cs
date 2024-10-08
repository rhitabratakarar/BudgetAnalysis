using System.ComponentModel.DataAnnotations;

namespace BudgetAnalysisDbApi.Models
{
    public class Year
    {
        [Key]
        public int Id { get; set; }

        public int YearCode { get; set; }
    }
}
