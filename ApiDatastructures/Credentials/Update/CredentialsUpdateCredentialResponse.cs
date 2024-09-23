namespace AnonKey_Backend.ApiDatastructures.Credentials.Update;

/// <summary>
/// The response to a credential update operation.
/// </summary>
public class CredentialsUpdateCredentialResponse
{
    /// <summary>
    /// The UUID of the credential.
    /// </summary>
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
    /// A note attached to the credential.
    /// </summary>
    public string? Note { get; set; }
    /// <summary>
    /// The display name of the credential..
    /// </summary>
    public string? DisplayName { get; set; }
    /// <summary>
    /// The UUID of the folder the credential is in.
    /// Use NULL for no folder.
    /// </summary>
    public string? FolderUuid { get; set; }
    /// <summary>
    /// The unix timestamp at which the credential was created.
    /// </summary>
    public long CreatedTimestamp { get; set; }
    /// <summary>
    /// The unix timestamp at which the credential was last edited.
    /// </summary>
    public long ChangedTimestamp { get; set; }
    /// <summary>
    /// The unix timestamp the credential was deleted at.
    /// </summary>
    public long? DeletedTimestamp { get; set; }
}
