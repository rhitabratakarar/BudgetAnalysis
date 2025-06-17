using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Website.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "This field is required.")]
        public string? SearchInput { get; set; }
        public IList<Expense>? Expenses { get; set; }

        private IDbDataAccess? _dbDataAccess { get; set; }

        public SearchModel(IDbDataAccess dbDataAccess)
        {
            this._dbDataAccess = dbDataAccess;
            this.Expenses = new List<Expense>();
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            if (ModelState.IsValid)
            {
                // if the search input is valid, fetch the data using EF Core.

                if (this._dbDataAccess != null && SearchInput != null)
                {
                    IList<Expense> expenses = await this._dbDataAccess.DbContext.Expenses
                        .Where(e => e.ExpenseName.Contains(SearchInput))
                        .Include(e => e.Month)
                        .Include(e => e.Year)
                        .ToListAsync();
                    this.Expenses = expenses;
                }
            }
        }
    }
}
