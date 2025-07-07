using DataAccess.Models;

namespace Website.Classes
{
    public class YearMonthTotalExpense
    {
        public int? YearCode { get; set; }
        public string? MonthName { get; set; }
        public double TotalExpense { get; set; } = 0.0;
    }
}
