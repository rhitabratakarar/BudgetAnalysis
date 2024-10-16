using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAnalysisDbApi.Database
{
    public class DbService : IDbService
    {
        private readonly IConfiguration _configuration;
        private readonly BudgetAnalysisDbContext dbContext;

        public DbService(IConfiguration configuration, BudgetAnalysisDbContext dbContext)
        {
            this._configuration = configuration;
            this.dbContext = dbContext;
        }

        public string GetConnectionString()
        {
            return this._configuration.GetConnectionString("BudgetAnalysis")!;
        }

        public async Task<bool> SaveSyncToolData(SheetExpensesMarshalledData marshalledData)
        {
            bool status = false;

            try
            {
                int yearToCheck = Convert.ToInt32(marshalledData.Year);
                Year? existingYear = await this.dbContext.Years.FirstOrDefaultAsync(y => y.YearCode == yearToCheck);

                if (existingYear == null)
                {
                    // insert the newly added year into the database.
                    await this.dbContext.Years.AddAsync(new Year() { YearCode = yearToCheck });
                }

                string monthToCheck = marshalledData.Month.Trim();
                Month? existingMonth = await this.dbContext.Months.FirstOrDefaultAsync(m => m.MonthName == monthToCheck);

                if (existingMonth == null)
                {
                    // insert the newly added month into database.
                    await this.dbContext.Months.AddAsync(new Month() { MonthName = monthToCheck });
                }

                // todo: proceed with inserting in expenses

                await this.dbContext.SaveChangesAsync();
                status = true;
            }
            catch(Exception)
            {
                status = false;
            }

            return status;
        }
    }
}
