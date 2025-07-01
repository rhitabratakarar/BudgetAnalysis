using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Website.Pages
{
    public class CleanUpModel : PageModel
    {
        public IList<Year>? ListOfYearsAvailableInDatabase { get; set; }
        public IList<Month>? ListOfMonthsAvailableInDatabase { get; set; }
        public string? CleanUpMessage { get; set; }

        [BindProperty]
        public IList<string>? SelectedYears { get; set; }

        [BindProperty]
        public IList<string>? SelectedMonths { get; set; }


        private readonly IDbDataAccess dbDataAccess;

        public CleanUpModel(IDbDataAccess dbDataAccess)
        {
            this.dbDataAccess = dbDataAccess;
        }

        public async Task OnGet()
        {
            IList<Year>? yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
            this.ListOfYearsAvailableInDatabase = yearsAvailable;

            IList<Month>? monthsAvailable = await this.dbDataAccess.DbContext.Months.ToListAsync();
            this.ListOfMonthsAvailableInDatabase = monthsAvailable;
        }

        public async Task OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            else
            {
                // execute for deletion if there are both selected years and months
                if (this.SelectedYears != null && this.SelectedYears.Any()
                    && this.SelectedMonths != null && this.SelectedMonths.Any())
                {
                    foreach (string year in this.SelectedYears)
                    {
                        foreach (string month in this.SelectedMonths)
                        {
                            if (month != "select" && year != "select")
                            {
                                // proceed to delete.
                                await RemoveExpensesFromDatabase(month, year);
                            }
                        }
                    }
                }

                // fetch the years again (other than OnGet)
                IList<Year> yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
                this.ListOfYearsAvailableInDatabase = yearsAvailable;

                // fetch the months again (other than OnGet)
                IList<Month> monthsAvailable = await this.dbDataAccess.DbContext.Months.ToListAsync();
                this.ListOfMonthsAvailableInDatabase = monthsAvailable;

                // assign confirmation message to the frontend.
                this.CleanUpMessage = "Done.";
            }
        }

        private async Task RemoveExpensesFromDatabase(string month, string year)
        {
            // search the expenses based on year and month to delete them first.
            IList<Expense> expensesToDelete = await this.dbDataAccess.DbContext.Expenses
                                        .Include(e => e.Year)
                                        .Include(e => e.Month)
                                        .Where(e => e.Month.MonthName == month && e.Year.YearCode == Convert.ToInt32(year))
                                        .ToListAsync();

            this.dbDataAccess.DbContext.RemoveRange(expensesToDelete);
            await this.dbDataAccess.DbContext.SaveChangesAsync();


            // remove the month also if no expense related to it found after the above deletion.
            IList<Expense> searchedExpenses = await this.dbDataAccess.DbContext.Expenses
                                    .Include(e => e.Month)
                                    .Include(e => e.Year)
                                    .Where(e => e.Month.MonthName == month)
                                    .ToListAsync();

            if (searchedExpenses == null || !searchedExpenses.Any())
            {
                // Delete the month entry if no expenses are found for that month across any year
                IList<Month> months = await this.dbDataAccess.DbContext
                                                    .Months
                                                    .Where(m => m.MonthName == month)
                                                    .ToListAsync();
                this.dbDataAccess.DbContext.RemoveRange(months);
                await this.dbDataAccess.DbContext.SaveChangesAsync();
            }


            // remove the year also if no expense related to it found after the above deletion
            searchedExpenses = await this.dbDataAccess.DbContext.Expenses
                                        .Include(e => e.Month)
                                        .Include(e => e.Year)
                                        .Where(e => e.Year.YearCode == Convert.ToInt32(year))
                                        .ToListAsync();

            if (searchedExpenses == null || !searchedExpenses.Any())
            {
                // Delete the year entry if no expenses are found for that year for any month
                IList<Year> years = await this.dbDataAccess.DbContext
                                            .Years
                                            .Where(y => y.YearCode == Convert.ToInt32(year))
                                            .ToListAsync();
                this.dbDataAccess.DbContext.RemoveRange(years);
                await this.dbDataAccess.DbContext.SaveChangesAsync();
            }
        }
    }
}
