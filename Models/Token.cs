namespace AnonKeyBackend.Models;

/// <summary>
/// Descibes an Access- or Refresh-Token
/// </summary>
public class Token{
    /// <summary>
    /// The UUID of the Token
    /// </summary>
    public string Uuid {get; set;} = "";
    /// <summary>
    /// The time the Token expires on
    /// </summary>
    public long ExpiresOn {get; set;} = 0;
}
