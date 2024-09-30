using Microsoft.AspNetCore.Mvc;
using BudgetAnalysisDbApi.DTO;

namespace BudgetAnalysisDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetAnalysisController
    {
        private readonly ILogger _logger;

        public BudgetAnalysisController(ILogger logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        public async Task InsertDataFromTool(InsertionDataRequest insertionDataRequest)
        {
            throw new NotImplementedException();
        }
    }
}
