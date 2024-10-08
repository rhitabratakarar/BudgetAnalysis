using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAnalysisDbApi.Models
{
    public class Year
    {
        [Key]
        public int Id { get; set; }

        public int YearCode { get; set; }
    }
}
