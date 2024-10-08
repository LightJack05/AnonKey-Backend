using System.Reflection.Metadata.Ecma335;
using AnonKey_Backend.ApiDatastructures.Folders.Update;
using AnonKey_Backend.Data;
using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders update endpoint.
/// </summary>
public static class Update
{
    /// <summary>
    /// Updates an existing folder object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.Update.FoldersUpdateResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Folders.Update.FoldersUpdateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        if (requestBody.Folder == null || String.IsNullOrEmpty(requestBody.Folder.Uuid) || String.IsNullOrEmpty(requestBody.Folder.Name))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "A parameter in the request was null or an empty string",
                Detail = "One of the parameters in the request was null or an empty string. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        User? userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        if (userObject == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody
            {
                Message = "The user does not exist",
                Detail = "The user does not exist in the database, he might have been deleted.",
                InternalCode = 0x5
            });
        }

        Folder? folder = databaseHandle.Folders.FirstOrDefault(f => f.Uuid == requestBody.Folder.Uuid);
        if (folder == null || folder.UserUuid != userObject.Uuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder with the given UUID does not exist, or does not belong to the user.",
                InternalCode = 0x6
            });
        }

        UpdateFolder(requestBody, folder);
        databaseHandle.SaveChanges();

        return TypedResults.Ok(new ApiDatastructures.Folders.Update.FoldersUpdateResponseBody
        {
            FolderUuid = folder.Uuid
        });
    }

    private static void UpdateFolder(FoldersUpdateRequestBody requestBody, Folder folder)
    {
        folder.DisplayName = requestBody.Folder.Name;
        folder.Icon = requestBody.Folder.Icon;
    }
}
