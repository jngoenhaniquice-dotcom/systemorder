using System.Diagnostics;
using System.Security.Claims;

namespace Estoque.Api.Middleware;

public sealed class AuditMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditMiddleware> _logger;

    public AuditMiddleware(
        RequestDelegate next,
        ILogger<AuditMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var username =
                context.User.FindFirstValue(ClaimTypes.Name)
                ?? "anonymous";

            _logger.LogInformation(
                "AUDIT User={User} Method={Method} Path={Path} Status={Status} IP={IP} DurationMs={DurationMs} TraceId={TraceId}",
                username,
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                context.Connection.RemoteIpAddress?.ToString(),
                stopwatch.ElapsedMilliseconds,
                context.TraceIdentifier
            );
        }
    }
}