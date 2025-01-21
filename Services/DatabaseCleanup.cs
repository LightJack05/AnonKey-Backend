using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Service responsible for cleaning up old entries in the database.
/// </summary>
public class DatabaseCleanup : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10); // Set the interval
    private readonly ILogger<DatabaseCleanup> _logger;


    /// <summary>
    /// Creates a new DatabaseCleanup Service
    /// </summary>
    public DatabaseCleanup(IServiceProvider serviceProvider, ILogger<DatabaseCleanup> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        logger.LogInformation("Starting up database scrubbing...");
    }

    /// <summary>
    /// Executes the service.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await ScrubOldEntriesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error while setting up scrubbing: {ex}");
            }

            await Task.Delay(_interval, cancellationToken);
        }
    }

    /// <summary>
    /// Scrubs old RefreshToken entries from the database.
    /// </summary>
    private async Task ScrubOldEntriesAsync(CancellationToken cancellationToken)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            AnonKeyBackend.Data.DatabaseHandle databaseHandle = scope.ServiceProvider.GetRequiredService<AnonKeyBackend.Data.DatabaseHandle>();

            long now = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
            IQueryable<AnonKeyBackend.Models.Token> tokensToDelete = databaseHandle.RefreshTokens.Where(t => t.ExpiresOn <= now - 200);
            databaseHandle.RefreshTokens.RemoveRange(tokensToDelete);
            _logger.LogInformation("Ran DB cleanup");

            await databaseHandle.SaveChangesAsync(cancellationToken);
        }

    }
}
