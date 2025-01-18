using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the authentication logout endpoint.
/// </summary>
public static class Logout
{

    /// <summary>
    /// Authenticates the user and returns an access token. 
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            PutLogout(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        if (!AnonKeyBackend.Authentication.TokenActions.ValidateClaimsOnRequest(user, databaseHandle))
        {
            return TypedResults.Unauthorized();
        }

        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }

        string parentUuid = user.Claims.First(c => c.Type == "TokenParent").Value;
        Token? parentToken = databaseHandle.RefreshTokens.FirstOrDefault(t => t.Uuid == parentUuid);
        if (parentToken == null)
        {
            return TypedResults.Unauthorized();
        }

        parentToken.Revoked = true;

        databaseHandle.SaveChanges();

        return TypedResults.Ok();
    }
}
