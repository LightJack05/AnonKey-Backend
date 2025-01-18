using AnonKeyBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
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
    /// <param name="tokenParent">The UUID of the parent of the parent token</param>
    public Models.Token GenerateNewToken(User user, string tokenType = "AccessToken", string tokenParent = "")
    {
        ArgumentNullException.ThrowIfNull(user.Uuid);
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(user.Username);

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
        long tokenExpiryTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds + tokenExpiryTime;
        string tokenUuid = Guid.NewGuid().ToString();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
                    {
                        new (ClaimTypes.Name, user.Username),
                        new (ClaimTypes.Role, "user"),
                        new ("TokenType", tokenType),
                        new ("TokenUuid", tokenUuid),
                        new ("TokenParent", tokenParent),
                        new (ClaimTypes.Expiration, System.Convert.ToString(tokenExpiryTimestamp, CultureInfo.InvariantCulture))
                    }),
            Expires = DateTime.UtcNow.AddSeconds(tokenExpiryTime),
            SigningCredentials = new(
                        new SymmetricSecurityKey(jwtIssuerSigningKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        string tokenString = tokenHandler.WriteToken(token);
        return new Models.Token()
        {
            TokenString = tokenString,
            ExpiresOn = tokenExpiryTimestamp,
            ParentUuid = tokenParent,
            Uuid = tokenUuid,
            TokenType = tokenType,
            UserUuid = user.Uuid
        };

    }

}

