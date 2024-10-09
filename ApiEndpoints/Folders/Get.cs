using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders get endpoint. 
/// </summary>
public static class Get
{

    /// <summary>
    /// Gets information on an existing folder.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.Get.FoldersGetResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGet(string folderUuid, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();

        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }
        if (String.IsNullOrEmpty(folderUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder UUID is missing",
                Detail = "The folder UUID is missing from the request. Please provide the folder UUID."
            });
        }

        User? userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        Folder? folder = databaseHandle.Folders.FirstOrDefault(f => f.Uuid == folderUuid);
        if (folder == null || userObject == null || folder.UserUuid != userObject.Uuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder with the given UUID does not exist."
            });
        }

        return TypedResults.Ok(new ApiDatastructures.Folders.Get.FoldersGetResponseBody
        {
            Folder = new ApiDatastructures.Folders.Get.FoldersGetFolder
            {
                Uuid = folder.Uuid,
                Name = folder.DisplayName,
                Icon = folder.Icon
            }
        });
    }
}
