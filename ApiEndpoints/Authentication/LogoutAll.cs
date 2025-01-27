using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Authentication;

/// <summary>
/// Handles the logout all endpoint.
/// </summary>
public static class LogoutAll
{

    /// <summary>
    /// Logs out all users.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            PutLogoutAll(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
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

        User? currentUser = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        if (currentUser == null)
        {
            return TypedResults.Unauthorized();
        }
        
        databaseHandle.RefreshTokens.Where(t => t.UserUuid == currentUser.Uuid).ToList().ForEach(token => token.Revoked = true);

        databaseHandle.SaveChanges();

        return TypedResults.Ok();
    }
}
