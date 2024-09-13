namespace AnonKey_Backend.Authentication;

/// <summary>
/// Holds methods used for authenticating users.
/// </summary>
public static class Authentication{
    /// <summary>
    /// Authenticates a user based on <paramref name="username"/> and <paramref name="kdfPasswordResult"/>.
    /// </summary>
    /// <returns>A Token object that contains an access token for using other API endpoints.</returns>
    public static Token AuthenticateUser(string username, string kdfPasswordResult){
        throw new NotImplementedException();
    }
}
