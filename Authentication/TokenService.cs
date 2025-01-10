using AnonKeyBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AnonKeyBackend.Authentication;

/// <summary>
/// The service provider for token generation.
/// Initialized as singleton by the application builder.
/// </summary>
public class TokenService
{
    /// <summary>
    /// The time set for token expiry after it has been issued.
    /// </summary>
    public const int AccessTokenExpiryTimeInSeconds = 600;
    /// <summary>
    /// The time set for token expiry after it has been issued.
    /// </summary>
    public const int RefreshTokenExpiryTimeInSeconds = 7884000;
    /// <summary>
    /// Generate a new token for a user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <param name="tokenType">The type of token to generate.</param>
    public string GenerateNewToken(User user, string tokenType = "AccessToken")
    {
        if (user is null || user.Username is null)
        {
            throw new ArgumentNullException();
        }


        JwtSecurityTokenHandler tokenHandler = new();
        byte[] jwtIssuerSigningKey = Configuration.Settings.JwtIssuerSigningKey;
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
                    {
                        new (ClaimTypes.Name, user.Username),
                        new (ClaimTypes.Role, "user"),
                        new ("TokenType", "AccessToken"),
                        new ("TokenUuid", Guid.NewGuid().ToString())
                    }),
            Expires = DateTime.UtcNow.AddSeconds(AccessTokenExpiryTimeInSeconds),
            SigningCredentials = new(
                        new SymmetricSecurityKey(jwtIssuerSigningKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

}

