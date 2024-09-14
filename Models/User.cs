using System.ComponentModel.DataAnnotations;
namespace AnonKey_Backend.Models;

/// <summary>
/// A user model to be saved in a database
/// </summary>
public class User
{
  /// <summary>
  /// UUID of the User.
  /// </summary>
  [Key]
  public string UserUuid { get; set; }

  /// <summary>
  /// The username to be diplayed
  /// </summary>
  public string DisplayName { get; set; }
}
