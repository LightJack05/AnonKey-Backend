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
    /// <param name="username">The username of the user.</param>
    /// <param name="passwordHash">The result of the hash function given the password, salt and pepper.</param>
    /// <param name="passwordSalt">The salt used for the password.</param>
    public UserSecret(string uuid, string username, string passwordHash, string passwordSalt)
    {
        Uuid = uuid;
        Username = username;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    /// <summary>
    /// UUID of the User.
    /// </summary>
    public string Uuid { get; set; }
    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Hash, including Salt and Pepper, of the user's password.
    /// </summary>
    public string PasswordHash { get; set; }
    /// <summary>
    /// The salt applied to the users's password.
    /// </summary>
    public string PasswordSalt { get; set; }
}
