using BudgetAnalysisDbApi.Interfaces;
using Serilog;

namespace BudgetAnalysisDbApi.Classes
{
    public class CustomLogger : ICustomLogger
    {
        private readonly Serilog.Core.Logger _logger;
        private readonly IConfiguration _configuration;

        public CustomLogger(IConfiguration configuration)
        {
            this._configuration = configuration;

            this._logger = new LoggerConfiguration()
                        .WriteTo.File(this._configuration!["LogPath"]!, rollingInterval: RollingInterval.Day)
                        .CreateLogger();
        }

        public void LogError(string message)
        {
            this._logger.Error(message);
        }

        public void LogDebug(string message)
        {
            this._logger.Debug(message);
        }

        public void LogInformation(string message)
        {
            this._logger.Information(message);
        }

        public void LogWarning(string message)
        {
            this._logger.Warning(message);
        }
    }
}
