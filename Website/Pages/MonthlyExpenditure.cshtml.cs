using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Classes;

namespace Website.Pages
{
    public class MonthlyExpenditureModel : PageModel
    {
        public IList<Year>? ListOfYearsAvailableInDatabase { get; set; }
        public IList<Month>? ListOfMonthsAvailableInDatabase { get; set; }

        public IList<YearMonthTotalExpense>? YearMonthTotalExpense { get; set; }


        [BindProperty]
        public IList<string>? SelectedYears { get; set; }

        [BindProperty]
        public IList<string>? SelectedMonths { get; set; }


        private readonly IDbDataAccess dbDataAccess;

        public MonthlyExpenditureModel(IDbDataAccess dbDataAccess)
        {
            this.dbDataAccess = dbDataAccess;
        }

        public async Task OnGet()
        {
            IList<Year> yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
            this.ListOfYearsAvailableInDatabase = yearsAvailable;

            IList<Month> monthsAvailable = await this.dbDataAccess.DbContext.Months.ToListAsync();
            this.ListOfMonthsAvailableInDatabase = monthsAvailable;
        }

        public async Task OnPost()
        {
            // reassign available years
            IList<Year> yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
            this.ListOfYearsAvailableInDatabase = yearsAvailable;

            // reassign available months
            IList<Month> monthsAvailable = await this.dbDataAccess.DbContext.Months.ToListAsync();
            this.ListOfMonthsAvailableInDatabase = monthsAvailable;

            await CalculateTotalMonthlyExpenditures();
        }

        public async Task CalculateTotalMonthlyExpenditures()
        {
            this.YearMonthTotalExpense = new List<YearMonthTotalExpense>();

            // based on selected year and month, calculate the total expenses on a month basis.
            if (this.SelectedYears != null && this.SelectedMonths != null && this.SelectedYears.Any() && this.SelectedMonths.Any())
            {
                foreach (string year in this.SelectedYears)
                {
                    foreach(string month in this.SelectedMonths)
                    {
                        if (year != "select" && month != "select")
                        {
                            IList<Expense> e = await this.dbDataAccess.DbContext.Expenses
                                                .Include(e => e.Year)
                                                .Include(e => e.Month)
                                                .Where(e => e.Year.YearCode == Convert.ToInt32(year) && e.Month.MonthName == month)
                                                .ToListAsync();

                            this.YearMonthTotalExpense.Add(new YearMonthTotalExpense()
                            {
                                YearCode = Convert.ToInt32(year),
                                MonthName = month,
                                TotalExpense = e.Sum(s => s.ExpenseCost)
                            });
                        }
                    }
                }
            }
        }
    }
}
