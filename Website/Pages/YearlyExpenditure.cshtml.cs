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

            // additional calculations
        }
    }
}
