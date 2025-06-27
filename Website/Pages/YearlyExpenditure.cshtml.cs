using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Website.Pages
{
    public class YearlyExpenditureModel : PageModel
    {
        [BindProperty]
        public IList<string>? SelectedYears { get; set; }

        public IList<Year>? ListOfYearsAvailableInDatabase { get; set; }
        public IDictionary<Year, double>? SumOfExpendituresByYears { get; set; }


        private readonly IDbDataAccess dbDataAccess;


        public YearlyExpenditureModel(IDbDataAccess dbDataAccess)
        {
            this.dbDataAccess = dbDataAccess;
        }

        public async Task OnGet()
        {
            IList<Year> yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
            this.ListOfYearsAvailableInDatabase = yearsAvailable;
        }

        public async Task OnPost()
        {
            // refetch the list of years available in database
            IList<Year> yearsAvailable = await this.dbDataAccess.DbContext.Years.ToListAsync();
            this.ListOfYearsAvailableInDatabase = yearsAvailable;

            // get total expenditure(s) of the year(s).
            this.SumOfExpendituresByYears = await GetSumOfExpenditureByYears();
        }

        private async Task<IDictionary<Year, double>> GetSumOfExpenditureByYears()
        {
            IDictionary<Year, double> expendituresByYears = new Dictionary<Year, double>();

            if (this.SelectedYears != null && this.SelectedYears.Any())
            {
                foreach (string y in this.SelectedYears)
                {
                    if (y != null && y != "select")
                    {
                        IList<Expense>? expenses = await this.dbDataAccess.DbContext.Expenses
                            .Include(e => e.Year)
                            .Where(e => e.Year.YearCode == Convert.ToInt32(y))
                            .ToListAsync();

                        Year year = new Year() { YearCode = Convert.ToInt32(y) };

                        expendituresByYears[year] = expenses.Sum(e => e.ExpenseCost);
                    }
                }
            }

            return expendituresByYears;
        }
    }
}
