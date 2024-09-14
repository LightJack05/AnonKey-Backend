using System.ComponentModel.DataAnnotations;
namespace AnonKey_Backend.Models;

/// <summary>
/// A credetial model to be saved in a database
/// </summary>
public class Credential
{
  /// <summary>
  /// UUID of the Credential
  /// </summary>
  [Key]
  public string? UserUuid { get; set; }

  /// <summary>
  /// The password of the credetial
  /// </summary>
  public string? Password { get; set; }

  /// <summary>
  /// The PasswordSalt of the credetial
  /// </summary>
  public string? PasswordSalt { get; set; }

  /// <summary>
  /// The Username of the credetial
  /// </summary>
  public string? Username { get; set; }

  /// <summary>
  /// The UsernameSalt of the credetial
  /// </summary>
  public string? UsernameSalt { get; set; }

  /// <summary>
  /// The WebsiteUrl of the credetial
  /// </summary>
  public string? WebsiteUrl { get; set; }

  /// <summary>
  /// The Note of the credetial
  /// </summary>
  public string? Note { get; set; }

  /// <summary>
  /// The DisplayName of the credetial
  /// </summary>
  public string? DisplayName { get; set; }

  /// <summary>
  /// The FolderUuid of the credetial
  /// </summary>
  public string? FolderUuid { get; set; }

  /// <summary>
  /// The CreatedTimestamp of the credetial
  /// </summary>
  public string? CreatedTimestamp { get; set; }

  /// <summary>
  /// The ChangedTimestamp of the credetial
  /// </summary>
  public string? ChangedTimestamp { get; set; }

  /// <summary>
  /// The DeletedTimestamp of the credetial
  /// </summary>
  public string? DeletedTimestamp { get; set; }
}
