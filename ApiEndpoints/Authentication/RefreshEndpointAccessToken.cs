using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the refresh access token endpoint.
/// </summary>
public static class RefreshEndpointAccessToken
{

    /// <summary>
    /// Creates a new access token based on a refresh token.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.RefreshTokens.AuthenticationRefreshTokensResponseBody>,
        UnauthorizedHttpResult>
     PostRefreshAccessToken(ClaimsPrincipal user, AnonKeyBackend.Authentication.TokenService tokenService, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        if (!AnonKeyBackend.Authentication.TokenActions.ValidateClaimsOnRequest(user, databaseHandle, true))
        {
            return TypedResults.Unauthorized();
        }

        User? userName = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        if (userName == null)
        {
            return TypedResults.Unauthorized();
        }

        string tokenUuid = user.Claims.First(c => c.Type == "TokenParent").Value;

        // Generate a new access token and return it to the user.
        Token accessToken = tokenService.GenerateNewToken(userName, "AccessToken", tokenUuid);
        return TypedResults.Ok(new ApiDatastructures.Authentication.RefreshTokens.AuthenticationRefreshTokensResponseBody
        {
            AccessToken = new()
            {
                Token = accessToken.TokenString,
                TokenType = accessToken.TokenType,
                ExpiryTimestamp = accessToken.ExpiresOn
            }
        });
    }
}
