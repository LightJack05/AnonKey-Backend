namespace AnonKey_Backend.ApiDatastructures.Credentials.Create;

/// <summary>
/// The credential object inside a credential create request.
/// </summary>
public class CredentialsCreateCredential
{
    /// <summary>
    /// The UUID of the credential.
    /// </summary>
    /// <note>Should be obtained from the UUID endpoint.</note>
    public string? Uuid { get; set; }
    /// <summary>
    /// The encrypted password to store.
    /// </summary>
    public string? Password { get; set; }
    /// <summary>
    /// The salt of the stored password.
    /// </summary>
    public string? PasswordSalt { get; set; }
    /// <summary>
    /// The encrypted username to store.
    /// </summary>
    public string? Username { get; set; }
    /// <summary>
    /// The salt of the encrypted username.
    /// </summary>
    public string? UsernameSalt { get; set; }
    /// <summary>
    /// The URL of the website the credential belongs to.
    /// </summary>
    public string? WebsiteUrl { get; set; }
    /// <summary>
    /// The WebsiteUrlSalt of the credetial
    /// </summary>
    public string? WebsiteUrlSalt { get; set; }
    /// <summary>
    /// A note attached to the credential.
    /// </summary>
    public string? Note { get; set; }
    /// <summary>
    /// The NoteSalt  of the credetial
    /// </summary>
    public string? NoteSalt { get; set; }
    /// <summary>
    /// The display name of the credential..
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// The DisplayNameSalt of the credetial
    /// </summary>
    public string? DisplayNameSalt { get; set; }
    /// <summary>
    /// The UUID of the folder the credential is in.
    /// Use NULL for no folder.
    /// </summary>
    public string? FolderUuid { get; set; }
}
