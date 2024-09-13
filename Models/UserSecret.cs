namespace AnonKey_Backend.Models;

/// <summary>
/// Secret user information only meant for use in the backend.
/// </summary>
public class UserSecret
{
    /// <summary>
    /// Initialize a new user object with the given parameters.
    /// </summary>
    /// <param name="uuid">The uuid of the created user.</param>
    /// <param name="usernameHash">The result of the hash function given the username, salt and pepper.</param>
    /// <param name="passwordHash">The result of the hash function given the password, salt and pepper.</param>
    /// <param name="usernameSalt">The salt used for the username.</param>
    /// <param name="passwordSalt">The salt used for the password.</param>
    public UserSecret(string uuid, string usernameHash, string passwordHash, string usernameSalt, string passwordSalt)
    {
        Uuid = uuid;
        UsernameHash = usernameHash;
        PasswordHash = passwordHash;
        UsernameSalt = usernameSalt;
        PasswordSalt = passwordSalt;
    }

    /// <summary>
    /// UUID of the User.
    /// </summary>
    public string Uuid { get; set; }
    /// <summary>
    /// Hash, including Salt and Pepper, of the user's username.
    /// </summary>
    public string UsernameHash { get; set; }
    /// <summary>
    /// Hash, including Salt and Pepper, of the user's password.
    /// </summary>
    public string PasswordHash { get; set; }
    /// <summary>
    /// The salt applied to the users's username.
    /// </summary>
    public string UsernameSalt { get; set; }
    /// <summary>
    /// The salt applied to the users's password.
    /// </summary>
    public string PasswordSalt { get; set; }
}
