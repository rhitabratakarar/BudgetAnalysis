namespace BudgetAnalysisDbApi.Classes
{
    public class SheetExpensesMarshalledData
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string SheetColumnRange { get; set; }
        public IList<string> SheetHeaders { get; set; }
        public IDictionary<string, string> MandatoryExpenses { get; set; }
        public IDictionary<string, string> OptionalExpenses { get; set; }
    }
}
