#pragma warning disable CA1848 
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AnonKeyBackend.Services;
/// <summary>
/// Service responsible for cleaning up old entries in the database.
/// </summary>
public class DatabaseCleanup : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromHours(2); // Set the interval
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
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ScrubOldEntriesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error while setting up scrubbing: {Ex}", ex);
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    /// <summary>
    /// Scrubs old RefreshToken entries from the database.
    /// </summary>
    private async Task ScrubOldEntriesAsync(CancellationToken stoppingToken)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            AnonKeyBackend.Data.DatabaseHandle databaseHandle = scope.ServiceProvider.GetRequiredService<AnonKeyBackend.Data.DatabaseHandle>();
            databaseHandle.Database.EnsureCreated();

            long now = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
            IQueryable<AnonKeyBackend.Models.Token> tokensToDelete = databaseHandle.RefreshTokens.Where(t => t.ExpiresOn <= now - 200);
            databaseHandle.RefreshTokens.RemoveRange(tokensToDelete);
            _logger.LogInformation("Ran DB cleanup");

            await databaseHandle.SaveChangesAsync(stoppingToken);
        }

    }
}
