using Microsoft.EntityFrameworkCore;

/// <summary>
/// Create a Context for the API
/// </summary>
public class APIContext : DbContext
{
  /*/// <summary>*/
  /*/// A table to save UserSecret*/
  /*/// </summary>*/
  /*public DbSet<AnonKey_Backend.Models.UserSecret> UserSecrets { get; set; }*/

  public DbSet<User> Users { get; set; }

  /// <summary>
  /// Set the DBPath to the ./Database/database.db
  /// </summary>
  public APIContext(DbContextOptions<APIContext> Options) : base(Options)
  {
  }

  /*protected override void OnModelCreating(ModelBuilder modelBuilder)*/
  /*{*/
  /*  modelBuilder.Entity<AuditEntry>();*/
  /*}*/
}

