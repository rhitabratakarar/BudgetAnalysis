﻿using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.DTO;
using BudgetAnalysisDbApi.Interfaces;
using BudgetAnalysisDbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetAnalysisDbApi.Database
{
    public class DbService : IDbService
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomLogger _logger;
        private readonly BudgetAnalysisDbContext dbContext;

        public DbService(IConfiguration configuration, BudgetAnalysisDbContext dbContext, ICustomLogger logger)
        {
            this._configuration = configuration;
            this.dbContext = dbContext;
            this._logger = logger;
        }

        /// <summary>
        /// Used as test, but can be useful sometimes to get the connection string.
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return this._configuration.GetConnectionString("BudgetAnalysis")!;
        }

        /// <summary>
        /// Get the list of months from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Month>> GetMonthList()
        {
            IList<Month> months = await this.dbContext.Months.ToListAsync();
            return months;
        }

        /// <summary>
        /// Get the list of years from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Year>> GetYearList()
        {
            IList<Year> years = await this.dbContext.Years.ToListAsync();
            return years;
        }

        /// <summary>
        /// Delete every expense based on month and year.
        /// </summary>
        /// <param name="yearName">Name of the year to remove.</param>
        /// <param name="monthName">Name of the month to remove.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> BulkUploadDelete(string yearName = "", string monthName = "")
        {
            int status = 0;

            try
            {
                if (yearName == null || yearName == string.Empty)
                    throw new Exception("ERROR: Received yearName as " + yearName);
                if (monthName == null || monthName == string.Empty)
                    throw new Exception("ERROR: Received monthName as " + monthName);

                int yearCode = Convert.ToInt32(yearName);

                IList<Expense> expensesToRemove = await (from expense in this.dbContext.Expenses
                                                         join month in this.dbContext.Months on expense.MonthId equals month.Id
                                                         join year in this.dbContext.Years on expense.YearId equals year.Id
                                                         where year.YearCode == yearCode && month.MonthName == monthName
                                                         select expense).ToListAsync();

                this.dbContext.Expenses.RemoveRange(expensesToRemove);

                await this.dbContext.SaveChangesAsync();
                status = 1;
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error occured" + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
            }
            return status;
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

                foreach (KeyValuePair<string, string> expense in marshalledData.MandatoryExpenses)
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
            catch (Exception)
            {
                status = false;
            }

            return status;
        }

        /// <summary>
        /// This method is used to query the database using a search term. The method primarily searches on expense name.
        /// An improved method will be built later on over this query limiting more search specific options.
        /// </summary>
        /// <param name="searchQry">The string to query the database based on expense name</param>
        /// <returns>The search result retrieved from the database.</returns>
        public async Task<IList<SearchResults>> GetSearchResults(string searchQry)
        {
            IList<Expense> expenses = await this.dbContext.Expenses.Where(e => e.ExpenseName.Contains(searchQry))
                .Include(e => e.Month)
                .Include(e => e.Year)
                .ToListAsync();

            if (expenses.Count == 0)
                return new List<SearchResults>();
            else
            {
                IList<SearchResults> results = expenses.Select(e => new SearchResults()
                {
                    ExpenseCost = e.ExpenseCost,
                    ExpenseName = e.ExpenseName,
                    ExpenseType = (e.ExpenseType == ExpenseType.Mandatory) ? "Mandatory" : "Optional",
                    Month = e.Month.MonthName,
                    Year = e.Year.YearCode
                }).ToList();

                return results;
            }
        }
    }
}
