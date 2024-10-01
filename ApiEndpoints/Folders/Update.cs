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

        if (user == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user is null",
                Detail = "The user is null, therefore no data extraction is possible",
                InternalCode = 0x4
            });
        }

        User userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        Folder folder = databaseHandle.Folders.FirstOrDefault(f => f.Uuid == requestBody.Folder.Uuid);
        if (folder == null || userObject == null || folder.UserUuid != userObject.Uuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder with the given UUID does not exist.",
                InternalCode = 0x6
            });
        }

        UpdateFolder(requestBody, folder);
        databaseHandle.SaveChanges();
        Folder updatedFolder = databaseHandle.Folders.Single(f => f.Uuid == requestBody.Folder.Uuid);
        
        return TypedResults.Ok(new ApiDatastructures.Folders.Update.FoldersUpdateResponseBody{
            FolderUuid = updatedFolder.Uuid
        });
    }

    private static void UpdateFolder(FoldersUpdateRequestBody requestBody, Folder folder)
    {
        folder.Uuid = requestBody.Folder.Uuid;
        folder.DisplayName = requestBody.Folder.Name;
        folder.Icon = requestBody.Folder.Icon;
    }
}
