using BudgetAnalysisDbApi.DTO;
using BudgetAnalysisDbApi.Interfaces;

namespace BudgetAnalysisDbApi.Classes
{
    public class DataMarshaller : IDataMarshaller
    {
        private readonly IConfiguration _configuration;

        public DataMarshaller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IDictionary<string, string> GetData(InsertionDataRequest insertionDataRequest)
        {
            IDictionary<string, string> marshalledData = new Dictionary<string, string>()
            {
                {"Year", insertionDataRequest.InsertionData![0][1] },
                {"Month", insertionDataRequest.InsertionData![1][1] },
                {"SheetColumnRange", insertionDataRequest.InsertionData![2][1] }
            };

            IList<string> sheetHeaders = insertionDataRequest.InsertionData![3];

            IDictionary<string, string> mandatoryExpenses = GetColumnSpecificData(sheetHeaders[0], sheetHeaders[1], 0, 1);
            IDictionary<string, string> optionalExpenses = GetColumnSpecificData(sheetHeaders[6], sheetHeaders[7], 6, 7);

            return marshalledData;
        }

        public IDictionary<string, string> GetColumnSpecificData(string headerColumn, string valueColumn, int headerColumnIndex, int valueColumnIndex)
        {
            throw new NotImplementedException();
        }
    }
}
