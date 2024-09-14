using System.ComponentModel.DataAnnotations;
namespace AnonKey_Backend.Models;

/// <summary>
/// Secret user information only meant for use in the backend.
/// </summary>
public class UserSecret
{
    //NO-PROD: Debug list to test authentication. To be replaced by Entity Framework
    public static List<UserSecret> userSecrets = new() {
        new UserSecret("bc255057-8abe-4098-9f7e-4095b8677a20", "test1", "password1", ""),
        new UserSecret("6324bde4-611f-49b5-a155-6f912a8882ec", "test2", "password2", "")
    };

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
    [Key]
    public string? Uuid { get; set; }
    /// <summary>
    /// The username of the user.
    /// </summary>
    public string? Username { get; set; }
    /// <summary>
    /// Hash, including Salt and Pepper, of the user's password.
    /// </summary>
    public string? PasswordHash { get; set; }
    /// <summary>
    /// The salt applied to the users's password.
    /// </summary>
    public string? PasswordSalt { get; set; }
}
