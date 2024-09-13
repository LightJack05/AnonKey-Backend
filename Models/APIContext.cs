using Microsoft.EntityFrameworkCore;
using System.IO;

/// <summary>
/// Create a Context for the API
/// </summary>
public class APIContext : DbContext
{
  /// <summary>
  /// Set the DBPath to the ./Database/database.db
  /// </summary>
  public APIContext(DbContextOptions<APIContext> Options) : base(Options)
  {
  }
}

