using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Website.Pages
{
    public class MonthlyExpenditureModel : PageModel
    {
        public IList<Year>? ListOfYearsAvailableInDatabase { get; set; }
        public IList<Month>? ListOfMonthsAvailableInDatabase { get; set; }


        [BindProperty]
        public IList<Year>? SelectedYears { get; set; }
        [BindProperty]
        public IList<Month>? SelectedMonths { get; set; }


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
    }
}
