﻿using BudgetAnalysisDbApi.Classes;
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

        /// <summary>
        /// This method saves the sync tool data into database.
        /// </summary>
        /// <param name="marshalledData">Data object which has been marshalled from DataMarshaller</param>
        /// <returns></returns>
        public async Task<bool> SaveSyncToolData(SheetExpensesMarshalledData marshalledData)
        {
            bool status = false;

            try
            {
                // check whether a year is existing in database.
                
                int yearToCheck = Convert.ToInt32(marshalledData.Year);
                Year? existingYear = await this.dbContext.Years.FirstOrDefaultAsync(y => y.YearCode == yearToCheck);
                
                if (existingYear == null)
                {
                    Year yearToInsert = new Year() { YearCode = yearToCheck };
                    await this.dbContext.Years.AddAsync(yearToInsert);
                    
                    // save the changes to retrieve the id.
                    await this.dbContext.SaveChangesAsync();

                    existingYear = yearToInsert;
                }

                // check whether a month is existing in database.

                string monthToCheck = marshalledData.Month.Trim();
                Month? existingMonth = await this.dbContext.Months.FirstOrDefaultAsync(m => m.MonthName == monthToCheck);

                if (existingMonth == null)
                {
                    Month monthToInsert = new Month() { MonthName = monthToCheck };
                    await this.dbContext.Months.AddAsync(monthToInsert);

                    // save the changes to retrieve the id.
                    await this.dbContext.SaveChangesAsync();

                    existingMonth = monthToInsert;
                }

                IList<Expense> expenses = new List<Expense>();

                foreach(KeyValuePair<string, string> expense in marshalledData.MandatoryExpenses)
                {
                    expenses.Add(new Expense()
                    {
                        Month = existingMonth,
                        Year = existingYear,
                        ExpenseType = ExpenseType.Mandatory,
                        ExpenseCost = Convert.ToDouble(expense.Value),
                        CreatedDate = DateTime.Now,
                        ExpenseName = expense.Key
                    });
                }

                foreach (KeyValuePair<string, string> expense in marshalledData.OptionalExpenses)
                {
                    expenses.Add(new Expense()
                    {
                        Month = existingMonth,
                        Year = existingYear,
                        ExpenseType = ExpenseType.Optional,
                        ExpenseCost = Convert.ToDouble(expense.Value),
                        CreatedDate = DateTime.Now,
                        ExpenseName = expense.Key
                    });
                }

                await this.dbContext.Expenses.AddRangeAsync(expenses);
                
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
