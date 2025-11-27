namespace MobileProviderApi.Services;

public interface IRateLimitingService
{
    bool IsAllowed(string subscriber);
}

public class RateLimitingService : IRateLimitingService
{
    private readonly Dictionary<string, int> _requestCount = new();

    public bool IsAllowed(string subscriber)
    {
        if (!_requestCount.ContainsKey(subscriber))
            _requestCount[subscriber] = 0;

        if (_requestCount[subscriber] >= 3)
            return false;

        _requestCount[subscriber]++;
        return true;
    }
}
