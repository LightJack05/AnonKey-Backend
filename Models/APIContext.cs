namespace AnonKey_Backend.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Create a Context for the API
/// </summary>
public class APIContext : DbContext
{
  /// <summary>
  /// A table to save UserSecrets
  /// </summary>
  public DbSet<AnonKey_Backend.Models.UserSecret>? UserSecrets { get; set; }

  /// <summary>
  /// A table to save Users
  /// </summary>
  public DbSet<User>? Users { get; set; }

  /// <summary>
  /// A table to save Credentials
  /// </summary>
  public DbSet<Credential>? Credentials { get; set; }

  /// <summary>
  /// A table to save Folders
  /// </summary>
  public DbSet<Folder>? Folders { get; set; }

  /// <summary>
  /// Set the DBPath to the ./Database/database.db
  /// </summary>
  public APIContext(DbContextOptions<APIContext> Options) : base(Options)
  {
  }
}

