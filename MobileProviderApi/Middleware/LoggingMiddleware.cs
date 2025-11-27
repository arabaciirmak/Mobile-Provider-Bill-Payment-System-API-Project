using System.Diagnostics;

namespace MobileProviderApi.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        Console.WriteLine($"REQUEST → {request.Method} {request.Path}{request.QueryString}");

        await _next(context);

        stopwatch.Stop();

        Console.WriteLine($"RESPONSE → {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds}ms");
    }
}
