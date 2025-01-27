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
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>>
            PutLogoutAll(Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        var allTokens = databaseHandle.RefreshTokens.ToList();

        if (allTokens.Count == 0)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "No active tokens found",
                Detail = "There are no tokens to revoke."
            });
        }

        foreach (var token in allTokens)
        {
            token.Revoked = true;
        }

        databaseHandle.SaveChanges();

        return TypedResults.Ok();
    }
}
