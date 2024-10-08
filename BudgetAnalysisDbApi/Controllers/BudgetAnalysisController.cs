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

        public BudgetAnalysisController(ICustomLogger logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._logger.LogInformation("BudgetAnalysisController Initialized!");
        }

        [HttpPost("[action]")]
        public async Task InsertDataFromTool(InsertionDataRequest insertionDataRequest)
        {
            if (insertionDataRequest.InsertionData != null)
            {
                IDataExtractor dataExtractor = new GoogleSheetDataExtractor(insertionDataRequest.InsertionData);

                IDictionary<string, string> optionalExpenses = dataExtractor.GetColumnSpecificData("", "");
                IDictionary<string, string> mandatoryExpenses = dataExtractor.GetColumnSpecificData("", "");

                // now save to DB using db context.
            }
            return;
        }

        [HttpGet("[action]")]
        public string Test()
        {
            this._logger.LogInformation("Test action is called");
            return "Test message from server!";
        }
    }
}
