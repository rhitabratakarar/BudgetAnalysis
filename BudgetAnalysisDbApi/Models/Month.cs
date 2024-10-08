using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAnalysisDbApi.Models
{
    public class Month
    {
        [Key]
        public int Id { get; set; }

        public string MonthName { get; set; }
    }
}
