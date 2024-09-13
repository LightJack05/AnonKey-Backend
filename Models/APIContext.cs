using Microsoft.EntityFrameworkCore;
using System.IO;

/// <summary>
/// Create a Context for the API
/// </summary>
public class APIContext : DbContext
{
  /// <summary>
  /// A Path to the database
  /// </summary>
  public string DbPath { get; }

  /// <summary>
  /// Set the DBPath to the ./Database/database.db
  /// </summary>
  public APIContext()
  {
    var path = Directory.GetCurrentDirectory();
    DbPath = System.IO.Path.Join(path, "Database", "database.db");
  }

  /// <summary>
  /// Creates a SQlite database at DBPath
  /// </summary>
  protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite($"Data Source={DbPath}");
}

