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
    /// The time set for access token expiry after it has been issued.
    /// </summary>
    public const int AccessTokenExpiryTimeInSeconds = 600;
    /// <summary>
    /// The time set for refresh token expiry after it has been issued.
    /// </summary>
    public const int RefreshTokenExpiryTimeInSeconds = 7884000;
    /// <summary>
    /// Generate a new token for a user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <param name="tokenType">The type of token to generate.</param>
    public string GenerateNewToken(User user, string tokenType = "AccessToken", string tokenParent = "")
    {
        if (user is null || user.Username is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        int tokenExpiryTime = 0;
        if (tokenType == "AccessToken")
        {
            tokenExpiryTime = AccessTokenExpiryTimeInSeconds;
        }
        else if (tokenType == "RefreshToken")
        {
            tokenExpiryTime = RefreshTokenExpiryTimeInSeconds;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(tokenType));
        }


        JwtSecurityTokenHandler tokenHandler = new();
        byte[] jwtIssuerSigningKey = Configuration.Settings.JwtIssuerSigningKey;
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
                    {
                        new (ClaimTypes.Name, user.Username),
                        new (ClaimTypes.Role, "user"),
                        new ("TokenType", tokenType),
                        new ("TokenUuid", Guid.NewGuid().ToString()),
                        new ("TokenParent", tokenParent),
                        new (ClaimTypes.Expiration, System.Convert.ToString((long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds + tokenExpiryTime))
                    }),
            Expires = DateTime.UtcNow.AddSeconds(tokenExpiryTime),
            SigningCredentials = new(
                        new SymmetricSecurityKey(jwtIssuerSigningKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

}

