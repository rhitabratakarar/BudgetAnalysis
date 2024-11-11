using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.Database;
using BudgetAnalysisDbApi.DTO;
using BudgetAnalysisDbApi.Interfaces;
using BudgetAnalysisDbApi.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("[action]")]
        public async Task<MonthListDTO> GetMonthList()
        {
            IList<Month> months = await this._dbService.GetMonthList();
            return new MonthListDTO() { Months = months };
        }

        [HttpGet("[action]")]
        public async Task<YearListDTO> GetYearList()
        {
            IList<Year> years = await this._dbService.GetYearList();
            return new YearListDTO() { Years = years };
        }

        [HttpGet("[action]")]
        public IDictionary<string, ExpenseType> GetExpenseTypes()
        {
            return new Dictionary<string, ExpenseType>()
            {
                { "Mandatory", ExpenseType.Mandatory },
                { "Optional", ExpenseType.Optional }
            };
        }

        [HttpDelete("[action]")]
        public async Task<int> BulkUploadDelete(BulkUploadDeleteDTO dto)
        {
            // 1 = success
            // 0 = failure

            int status = await this._dbService.BulkUploadDelete(dto.YearName, dto.MonthName);
            return status;
        }

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

        [HttpGet("[action]")]
        public string Test()
        {
            this._logger.LogInformation("Test action is called");
            return this._dbService.GetConnectionString();
        }
    }
}
