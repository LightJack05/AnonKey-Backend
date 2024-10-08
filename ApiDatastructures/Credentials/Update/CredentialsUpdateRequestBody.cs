namespace AnonKey_Backend.ApiDatastructures.Credentials.Update;

/// <summary>
/// The body of a credential update request.
/// </summary>
public class CredentialsUpdateRequestBody
{
    /// <summary>
    /// The credential to update with the new data.
    /// </summary>
    public CredentialsUpdateCredentialRequest? Credential { get; set; }
}
