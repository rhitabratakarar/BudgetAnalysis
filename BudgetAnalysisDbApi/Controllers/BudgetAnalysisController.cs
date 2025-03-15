using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.Database;
using BudgetAnalysisDbApi.DTO;
using BudgetAnalysisDbApi.Interfaces;
using BudgetAnalysisDbApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BudgetAnalysisDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetAnalysisController
    {
        private readonly ICustomLogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IDataMarshaller _dataMarshaller;
        private readonly IDbService _dbService;

        public BudgetAnalysisController(ICustomLogger logger,
            IConfiguration configuration,
            IDataMarshaller dataMarshaller,
            IDbService dbService)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._dataMarshaller = dataMarshaller;
            this._dbService = dbService;
            this._logger.LogInformation("BudgetAnalysisController Initialized!");
        }

        /// <summary>
        /// Method that will provide the list of months present in the database.
        /// </summary>
        /// <returns>The list of months which have been synced from database and present in local.</returns>
        [HttpGet("[action]")]
        public async Task<MonthListDTO> GetMonthList()
        {
            this._logger.LogInformation("GetMonthList called.");
            IList<Month> months = await this._dbService.GetMonthList();
            this._logger.LogInformation("GetMonthList ended.");

            return new MonthListDTO() { Months = months };
        }

        /// <summary>
        /// Method that provides the list of years present in the database based on how it is synced.
        /// </summary>
        /// <returns>List of year codes in database.</returns>
        [HttpGet("[action]")]
        public async Task<YearListDTO> GetYearList()
        {
            this._logger.LogInformation("GetYearList called.");
            IList<Year> years = await this._dbService.GetYearList();
            this._logger.LogInformation("GetYearList ended.");
            return new YearListDTO() { Years = years };
        }

        /// <summary>
        /// Used for deleting data in bulk upload.
        /// </summary>
        /// <param name="dto">year code and month name are the basis of deletion</param>
        /// <returns>Integer to define whether the operation was success or failure.</returns>
        [HttpDelete("[action]")]
        public async Task<int> BulkUploadDelete(BulkUploadDeleteDTO dto)
        {
            // 1 = success
            // 0 = failure
            this._logger.LogInformation("Initiating bulk deletion based on: " + dto.YearName + "/" + dto.MonthName);
            int status = await this._dbService.BulkUploadDelete(dto.YearName, dto.MonthName);
            this._logger.LogInformation("Bulk deletion status: " + status);
            return status;
        }

        /// <summary>
        /// Important method used by the sync tool to sync the excel changes and database.
        /// </summary>
        /// <param name="insertionDataRequest">Insertion data</param>
        /// <returns>Boolean defining success or failure.</returns>
        [HttpPost("[action]")]
        public async Task<bool> InsertDataFromTool(InsertionDataRequest insertionDataRequest)
        {
            this._logger.LogInformation("InsertDataFromTool action method called.");

            bool status = false;

            if (insertionDataRequest.InsertionData != null)
            {
                try
                {
                    this._logger.LogInformation("Trying to marshall insertion data request.");
                    SheetExpensesMarshalledData marshalledData = this._dataMarshaller.GetData(insertionDataRequest);

                    this._logger.LogInformation("Trying to call the db service to save data into the database");
                    status = await this._dbService.SaveSyncToolData(marshalledData);
                }
                catch (Exception e)
                {
                    this._logger.LogError("ERROR: " + e.Message);
                    this._logger.LogError("INNER EXCEPTION: " + e.InnerException?.Message);
                    this._logger.LogError("STACK TRACE: " + e.StackTrace);
                }
            }
            else
            {
                this._logger.LogWarning("Insertion Data request is found to be null");
            }

            return status;
        }

        /// <summary>
        /// Test method, generally used to test the connection.
        /// </summary>
        /// <returns>returns a string to specify whether the service is up or not.</returns>
        [HttpGet("[action]")]
        public string Test()
        {
            this._logger.LogInformation("Test action is called");
            return this._dbService.GetConnectionString();
        }

        /// <summary>
        /// Method to filter search results from UI via a search statement provided.
        /// </summary>
        /// <param name="searchStatement">string statement provided from the query.</param>
        /// <returns>the list of search results to map with the table in UI</returns>
        [HttpGet("[action]")]
        public async Task<IList<SearchResults>> GetSearchResults([FromQuery] string searchStatement)
        {
            IList<SearchResults> results = await this._dbService.GetSearchResults(searchStatement);
            return results;
        }
    }
}
