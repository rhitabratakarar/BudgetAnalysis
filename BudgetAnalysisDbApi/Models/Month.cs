using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAnalysisDbApi.Models
{
    public class Month
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(maximumLength:10, MinimumLength = 3, ErrorMessage = "Month name should be greater than 3 and less than 10 characters.")]
        public string MonthName { get; set; }
    }
}
