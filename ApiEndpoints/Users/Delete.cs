using AnonKeyBackend.Data;
using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Users;

/// <summary>
/// Handles the users delete endpoint.
/// </summary>
public static class Delete
{
    /// <summary>
    /// Deletes an existing user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>,
        UnauthorizedHttpResult>
            DeleteDelete(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        //NOTE: Maybe change this to a soft delete in the future.
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
        Models.User? userToDelete = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        if (userToDelete == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "A user with that name does not exist.",
                Detail = "The user matching the name in the JWT claims could not be found in the database. Is it already deleted?"
            });
        }

        DeleteUserdata(databaseHandle, userToDelete);
        databaseHandle.SaveChanges();
        return TypedResults.Ok();

    }

    private static void DeleteUserdata(DatabaseHandle databaseHandle, User userToDelete)
    {
        databaseHandle.Credentials.RemoveRange(databaseHandle.Credentials.Where(c => c.UserUuid == userToDelete.Uuid));
        databaseHandle.Folders.RemoveRange(databaseHandle.Folders.Where(c => c.UserUuid == userToDelete.Uuid));
        databaseHandle.UserInfos.RemoveRange(databaseHandle.UserInfos.Where(u => u.UserUuid == userToDelete.Uuid));
        databaseHandle.Users.Remove(userToDelete);
    }
}
