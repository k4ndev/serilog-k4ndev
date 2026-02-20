using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Context;

namespace K4ndev.Serilog;

public static class LoggingExtensions
{
    public static IHostBuilder UseK4ndevSerilog(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((context, services, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        });
    }
    public static IApplicationBuilder UseK4ndevLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";

            var forwarded = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            var clientIp = string.IsNullOrWhiteSpace(forwarded)
                ? context.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
                : forwarded.Split(',').First().Trim();

            using (LogContext.PushProperty("ClientIp", clientIp))
            using (LogContext.PushProperty("UserId", userId))
            {
                await next();
            }
        });
    }
}
