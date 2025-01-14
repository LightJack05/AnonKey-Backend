using System.ComponentModel.DataAnnotations;
namespace AnonKeyBackend.Models;

/// <summary>
/// Descibes an Access- or Refresh-Token
/// </summary>
public class Token
{
    /// <summary>
    /// The UUID of the Token
    /// </summary>
    [Key]
    public string Uuid { get; set; } = "";
    /// <summary>
    /// The time the Token expires on
    /// </summary>
    public long ExpiresOn { get; set; } = 0;
    /// <summary>
    /// The UUID of the parent refresh token
    /// </summary>
    public string ParentUuid { get; set; } = "";
    /// <summary>
    /// The string data of the token
    /// </summary>
    public string TokenString { get; set; } = "";
    /// <summary>
    /// The type of the token, either RefreshToken or AccessToken
    /// </summary>
    public string TokenType { get; set; } = "";
    /// <summary>
    /// Whether the token has been revoked.
    /// </summary>
    public bool Revoked {get; set;} = false;
}
