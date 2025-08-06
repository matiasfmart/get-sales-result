using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace get_sales_result.WebApi.Middlewares
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExecutionTimeMiddleware> _logger;

        public ExecutionTimeMiddleware(RequestDelegate next, ILogger<ExecutionTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var path = context.Request.Path;
            var method = context.Request.Method;

            _logger.LogInformation("[ExecutionTime] {Method} {Path} ejecutado en {ElapsedMilliseconds}ms",
                method, path, elapsedMs);
        }
    }
}
