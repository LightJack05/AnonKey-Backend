using AnonKey_Backend.Data;
using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Users;

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
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        //NOTE: Maybe change this to a soft delete in the future.

        Models.User? userToDelete = databaseHandle.Users.Where(u => u.Username == user.Identity.Name).FirstOrDefault();

        if (userToDelete == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A user with that name does not exist.",
                Detail = "The user matching the name in the JWT claims could not be found in the database. Is it already deleted?",
                InternalCode = 0x7
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
