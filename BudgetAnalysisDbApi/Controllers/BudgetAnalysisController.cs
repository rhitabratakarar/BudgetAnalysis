using BudgetAnalysisDbApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAnalysisDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetAnalysisController
    {
        //private readonly ILogger _logger;

        public BudgetAnalysisController() //ILogger logger
        {
            //this._logger = logger;
        }

        [HttpPost("[action]")]
        public async Task InsertDataFromTool(InsertionDataRequest insertionDataRequest)
        {
            Console.WriteLine(insertionDataRequest);
            return;

        }
    }
}
