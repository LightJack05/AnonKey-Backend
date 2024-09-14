using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnonKey_Backend.Authentication;

/// <summary>
/// The service provider for token generation.
/// Initialized as singleton by the application builder.
/// </summary>
public class TokenService
{
    /// <summary>
    /// The time set for token expiry after it has been issued.
    /// </summary>
    public const int TokenExpiryGraceInSeconds = 1800;
    /// <summary>
    /// Generate a new token for a user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    public string GenerateNewToken(Models.UserSecret user)
    {
        if(user is null || user.Username is null){
            throw new ArgumentNullException();
        }


        JwtSecurityTokenHandler tokenHandler = new();
        byte[] jwtIssuerSigningKey = Configuration.Settings.JwtIssuerSigningKey;
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
                    {
                        new (ClaimTypes.Name, user.Username),
                        new (ClaimTypes.Role, "user")
                    }),
            Expires = DateTime.UtcNow.AddSeconds(TokenExpiryGraceInSeconds),
            SigningCredentials = new(
                        new SymmetricSecurityKey(jwtIssuerSigningKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

}

