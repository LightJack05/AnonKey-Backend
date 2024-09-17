namespace AnonKey_Backend.Data;
using Models;
using Microsoft.EntityFrameworkCore;

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
}

