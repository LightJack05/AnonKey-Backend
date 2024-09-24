using AnonKey_Backend.Data;
using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders delete endpoint.
/// </summary>
public static class Delete
{

    /// <summary>
    /// Deletes an existing folder.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            DeleteDelete(string folderUuid, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (String.IsNullOrEmpty(folderUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The a parameter in the request was null",
                Detail = "One of the parameters in the request was null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        if (databaseHandle.Folders.Any(f => f.Uuid == folderUuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder does not exist. Please check the folder uuid.",
                InternalCode = 0x6
            });
        }

        DeleteFolder(folderUuid, user, databaseHandle);
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }

    private static void DeleteFolder(string folderUuid, ClaimsPrincipal user, DatabaseHandle databaseHandle)
    {
        User userObject = databaseHandle.Users.First(u => u.Username == user.Identity.Name);
        databaseHandle.Folders.RemoveRange(databaseHandle.Folders.Where(f => f.Uuid == folderUuid && f.UserUuid == userObject.Uuid));
    }
}
