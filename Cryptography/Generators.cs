namespace AnonKey_Backend.Cryptography;
using System.Security.Cryptography;

/// <summary>
/// Cryptographic generators for values.
/// </summary>
public static class Generators{
    /// <summary>
    /// Generates a new random byte array with the specified length.
    /// </summary>
    public static byte[] NewRandomByteArray(int length){
        byte[] randomBytes = RandomNumberGenerator.GetBytes(length);
        return randomBytes;
    }
}
