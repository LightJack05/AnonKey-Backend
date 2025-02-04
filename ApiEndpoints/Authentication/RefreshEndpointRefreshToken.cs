using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the refresh refresh token endpoint.
/// </summary>
public static class RefreshEndpointRefreshToken
{

    /// <summary>
    /// Creates a new refresh token based on a refresh token.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.RefreshRefreshToken.AuthenticationRefreshRefreshTokenResponseBody>,
        UnauthorizedHttpResult>
     PostRefreshRefreshToken(ClaimsPrincipal user, AnonKeyBackend.Authentication.TokenService tokenService, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        if (!AnonKeyBackend.Authentication.TokenActions.ValidateClaimsOnRequest(user, databaseHandle, true))
        {
            return TypedResults.Unauthorized();
        }

        if (user.Identity == null)
        {
            return TypedResults.Unauthorized();
        }

        User? currentUser = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        if (currentUser == null)
        {
            return TypedResults.Unauthorized();
        }

        // Revoke the old refresh token.
        string tokenUuid = user.Claims.First(c => c.Type == "TokenUuid").Value;
        Token? token = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == tokenUuid);
        if (token == null)
        {
            return TypedResults.Unauthorized();
        }
        token.Revoked = true;

        // Generate a new token and return it to the user.
        Token refreshToken = tokenService.GenerateNewToken(currentUser, "RefreshToken");
        Token accessToken = tokenService.GenerateNewToken(currentUser, "AccessToken", refreshToken.Uuid);

        databaseHandle.SaveChanges();

        return TypedResults.Ok(new ApiDatastructures.Authentication.RefreshRefreshToken.AuthenticationRefreshRefreshTokenResponseBody
        {
            AccessToken = new()
            {
                Token = accessToken.TokenString,
                TokenType = accessToken.TokenType,
                ExpiryTimestamp = accessToken.ExpiresOn
            },
            RefreshToken = new()
            {
                Token = refreshToken.TokenString,
                TokenType = refreshToken.TokenType,
                ExpiryTimestamp = refreshToken.ExpiresOn
            }
        });
    }
}
