using BudgetAnalysisDbApi.Classes;
using BudgetAnalysisDbApi.Database;
using BudgetAnalysisDbApi.DTO;
using BudgetAnalysisDbApi.Interfaces;
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

        [HttpPost("[action]")]
        public bool InsertDataFromTool(InsertionDataRequest insertionDataRequest)
        {
            bool status = false;

            if (insertionDataRequest.InsertionData != null)
            {
                SheetExpensesMarshalledData marshalledData = this._dataMarshaller.GetData(insertionDataRequest);
                status = this._dbService.SaveSyncToolData(marshalledData);
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
