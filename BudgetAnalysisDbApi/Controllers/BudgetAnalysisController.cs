using BudgetAnalysisDbApi.Classes;
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

        public BudgetAnalysisController(ICustomLogger logger, IConfiguration configuration, IDataMarshaller dataMarshaller)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._dataMarshaller = dataMarshaller;
            this._logger.LogInformation("BudgetAnalysisController Initialized!");
        }

        [HttpPost("[action]")]
        public void InsertDataFromTool(InsertionDataRequest insertionDataRequest)
        {
            if (insertionDataRequest.InsertionData != null)
            {
                IDictionary<string, string> marshalledData = this._dataMarshaller.GetData(insertionDataRequest);

                // save to db.
            }
        }

        [HttpGet("[action]")]
        public string Test()
        {
            this._logger.LogInformation("Test action is called");
            return "Test message from server!";
        }
    }
}
