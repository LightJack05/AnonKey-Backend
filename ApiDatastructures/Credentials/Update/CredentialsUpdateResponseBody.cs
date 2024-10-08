namespace AnonKey_Backend.ApiDatastructures.Credentials.Update;

/// <summary>
/// The body of a credential update response.
/// </summary>
public class CredentialsUpdateResponseBody
{
    /// <summary>
    /// The update credential.
    /// </summary>
    public CredentialsUpdateCredentialResponse? Credential { get; set; }
}
