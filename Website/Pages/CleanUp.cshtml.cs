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
    }
}
