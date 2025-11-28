using MobileProviderApi.Services;

namespace MobileProviderApi.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IRateLimitingService rate)
    {
        var path = context.Request.Path.Value?.ToLower();

        if (path != null && path.Contains("/query"))
        {
            var subscriberNo = context.Request.Query["subscriberNo"].ToString();

            if (!string.IsNullOrEmpty(subscriberNo))
            {
                if (!rate.IsAllowed(subscriberNo))
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Rate limit exceeded (3 per day).");
                    return;
                }
            }
        }
        await _next(context);
    }
}