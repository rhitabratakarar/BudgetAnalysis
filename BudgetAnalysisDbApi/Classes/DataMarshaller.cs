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

        public SheetExpensesMarshalledData GetData(InsertionDataRequest insertionDataRequest)
        {
            IList<string> sheetHeaders = insertionDataRequest.InsertionData![3];

            SheetExpensesMarshalledData marshalledData = new SheetExpensesMarshalledData()
            {
                Year = insertionDataRequest.InsertionData![0][1],
                Month = insertionDataRequest.InsertionData![1][1],
                SheetColumnRange = insertionDataRequest.InsertionData![2][1],
                SheetHeaders = sheetHeaders,
                MandatoryExpenses = GetExpensesMappings(sheetHeaders[0], sheetHeaders[1], 0, 1, insertionDataRequest),
                OptionalExpenses = GetExpensesMappings(sheetHeaders[6], sheetHeaders[7], 6, 7, insertionDataRequest)
            };
            return marshalledData;
        }

        public IDictionary<string, string> GetExpensesMappings(string headerColumnName, string valueColumnName, 
            int headerColumnIndex, int valueColumnIndex, InsertionDataRequest insertionDataRequest)
        {
            IDictionary<string, string> mappedData = new Dictionary<string, string>() { };
            
            // skipping the first 4 rows since they contains data other than expenses.
            foreach(IList<string> row in insertionDataRequest.InsertionData!.Skip(4).ToList())
            {
                string expenseType = row[headerColumnIndex];
                string expenseCost = row[valueColumnIndex];
                mappedData.Add(expenseType, expenseCost);
            }

            return mappedData;
        }
    }
}
