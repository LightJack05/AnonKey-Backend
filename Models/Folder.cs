using System.ComponentModel.DataAnnotations;
namespace AnonKey_Backend.Models;

/// <summary>
/// A credetial model to be saved in a database
/// </summary>
public class Folder
{
  /// <summary>
  /// UUID of the Folder
  /// </summary>
  [Key]
  public string? Uuid { get; set; }

  /// <summary>
  /// The username to be diplayed
  /// </summary>
  public string? DisplayName { get; set; }

  /// <summary>
  /// The icon of the folder
  /// </summary>
  public string? Icon { get; set; }
}
