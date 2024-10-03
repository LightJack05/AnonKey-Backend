namespace AnonKey_Backend.Data;
using Models;
using Microsoft.EntityFrameworkCore;
using AnonKey_Backend.Development;

/// <summary>
/// Create a Context for the API
/// </summary>
public class DatabaseHandle : DbContext
{
    /// <summary>
    /// A table to save UserSecrets
    /// </summary>
    public DbSet<User>? Users { get; set; }

    /// <summary>
    /// A table to save Users
    /// </summary>
    public DbSet<UserInfo>? UserInfos { get; set; }

    /// <summary>
    /// A table to save Credentials
    /// </summary>
    public DbSet<Credential>? Credentials { get; set; }

    /// <summary>
    /// A table to save Folders
    /// </summary>
    public DbSet<Folder>? Folders { get; set; }

    /// <summary>
    /// Set the DBPath to the ./database.db
    /// </summary>
    public DatabaseHandle(DbContextOptions<DatabaseHandle> Options) : base(Options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase))
        {
            modelBuilder.Seed();
        }
    }
}

