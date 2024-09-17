using Konscious.Security.Cryptography;
using System.Text;
namespace AnonKey_Backend.Cryptography;

/// <summary>
/// Holds methods for hashing passwords, and checking the hashed passwords.
/// </summary>
public static class PasswordHashing
{
    /// <summary>
    /// Hash a password with the given salt and password.
    /// The Pepper will automatically be obtained from settings.
    /// </summary>
    public static string HashPassword(string password, string salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        Argon2id argon2Id = new Argon2id(passwordBytes);
        argon2Id.DegreeOfParallelism = 16;
        argon2Id.MemorySize = 16384;
        argon2Id.Iterations = 100;
        argon2Id.Salt = Encoding.UTF8.GetBytes(salt + Configuration.Settings.ServerUserPasswordPepper);
        byte[] hash = argon2Id.GetBytes(512);
        return Convert.ToBase64String(hash);
    }

    public static bool isPasswordValid(string password, string userSalt, string userPasswordHash)
    {

    }

}
