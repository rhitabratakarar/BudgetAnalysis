using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
                // fetch the years again (other than OnGet)
                IList<Year> yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
                this.ListOfYearsAvailableInDatabase = yearsAvailable;

                // fetch the months again (other than OnGet)
                IList<Month> monthsAvailable = await this.dbDataAccess.DbContext.Months.ToListAsync();
                this.ListOfMonthsAvailableInDatabase = monthsAvailable;

                // execute for deletion if there are both selected years and months
                if (this.SelectedYears != null && this.SelectedYears.Any()
                    && this.SelectedMonths != null && this.SelectedMonths.Any())
                {
                    foreach (string year in this.SelectedYears)
                    {
                        foreach (string month in this.SelectedMonths)
                        {
                            if (month == "select" || year == "select")
                            {
                                continue;
                            }
                            else
                            {
                                // proceed to delete.
                                await RemoveExpensesFromDatabase(month, year);
                            }
                        }
                    }
                }

                // assign confirmation message to the frontend.
                this.CleanUpMessage = "Done.";
            }
        }

        private async Task RemoveExpensesFromDatabase(string month, string year)
        {
            IList<Expense> expenses = await this.dbDataAccess.DbContext.Expenses
                                        .Include(e => e.Year)
                                        .Include(e => e.Month)
                                        .Where(e => e.Month.MonthName == month)
                                        .ToListAsync();

            // remove expenses
            // remove the month also if no expense related to it found after the above deletion.
            // remove the year also if no expense related to it found after the above deletion
            throw new NotImplementedException("Logic yet to be implemented in RemoveExpensesFromDatabase()");
        }
    }
}
