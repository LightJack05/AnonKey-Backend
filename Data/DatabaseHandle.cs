namespace AnonKeyBackend.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using AnonKeyBackend.Development;

#nullable disable

/// <summary>
/// Create a Context for the API
/// </summary>
public class DatabaseHandle : DbContext
{
    /// <summary>
    /// A table to save UserSecrets
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// A table to save Users
    /// </summary>
    public DbSet<UserInfo> UserInfos { get; set; }

    /// <summary>
    /// A table to save Credentials
    /// </summary>
    public DbSet<Credential> Credentials { get; set; }

    /// <summary>
    /// A table to save Folders
    /// </summary>
    public DbSet<Folder> Folders { get; set; }

    /// <summary>
    /// A table for revoked tokens
    /// </summary>
    public DbSet<Token> RevokedTokens {get; set;}

    /// <summary>
    /// Set the DBPath to the ./database.db
    /// </summary>
    public DatabaseHandle(DbContextOptions<DatabaseHandle> Options) : base(Options)
    {
    }

    /// <summary>
    /// Initialization Method for the Database Handle.
    /// Initializes the DB with sample data in case the application is running in Dev mode.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase))
        {
            modelBuilder.Seed();
        }
    }
}

#nullable restore
