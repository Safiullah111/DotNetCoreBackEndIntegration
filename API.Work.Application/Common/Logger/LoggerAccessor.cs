using Microsoft.Extensions.Logging;

public static class LoggerAccessor
{
    private static ILogger _logger;

    public static void Configure(ILogger logger)
    {
        if (_logger != null)
            throw new InvalidOperationException("Logger has already been configured.");

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public static ILogger Logger => _logger
        ?? throw new InvalidOperationException("Logger has not been configured.");
}
