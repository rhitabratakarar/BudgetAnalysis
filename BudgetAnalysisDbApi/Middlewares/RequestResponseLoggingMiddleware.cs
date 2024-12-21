using BudgetAnalysisDbApi.Interfaces;

namespace BudgetAnalysisDbApi.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICustomLogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ICustomLogger logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            this._logger.LogInformation("Received Request at + " + context.GetEndpoint());
            await _next(context);
            this._logger.LogInformation("Sending response status: " + context.Response.StatusCode);
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }

}
