using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the refresh access token endpoint.
/// </summary>
public static class RefreshEndpointAccessToken
{

    /// <summary>
    /// Creates a new access token.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody>,
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
     PostRefreshAccessToken(ClaimsPrincipal userClaimsPrincipal, ApiDatastructures.Authentication.Login.AuthenticationLoginRequestBody requestBody, AnonKeyBackend.Authentication.TokenService tokenService, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        if (!AnonKeyBackend.Authentication.TokenActions.ValidateClaimsOnRequest(userClaimsPrincipal, databaseHandle))
        {
            return TypedResults.Unauthorized();
        }

        User? user = databaseHandle.Users.Where(u => u.Username == requestBody.UserName).FirstOrDefault();

        // If no user is found matching the redentials, return 404.
        if (user == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody
            {
                Message = "Invalid username or password",
                Detail = "The username and password combination did not match any known combination. Please ensure the password and username are correct."
            });
        }

        // Generate a new token and return it to the user.
        Token refreshToken = tokenService.GenerateNewToken(user, "RefreshToken");
        Token accessToken = tokenService.GenerateNewToken(user, "AccessToken");
        AnonKeyBackend.Authentication.TokenActions.StoreRefreshTokenInDb(refreshToken, databaseHandle);
        databaseHandle.SaveChanges();
        return TypedResults.Ok(new ApiDatastructures.Authentication.Login.AuthenticationLoginResponseBody
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
