namespace BudgetAnalysisDbApi.Interfaces
{
    public interface ICustomLogger
    {
        public void LogInformation(string message);
        public void LogError(string message);
        public void LogWarning(string message);
        public void LogDebug(string message);
    }
}
