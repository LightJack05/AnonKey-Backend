namespace AnonKey_Backend.ApiDatastructures.Credentials.Create;

/// <summary>
/// The body of a response to a credential create request.
/// </summary>
public class CredentialsCreateResponseBody
{
    /// <summary>
    /// The UUID of the newly created credential.
    /// </summary>
    public string? CredentialUuid { get; set; }
}
