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
            DeleteDelete(string folderUuid, bool recursive, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
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

        User? userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        Folder? folder = databaseHandle.Folders.FirstOrDefault(f => f.Uuid == folderUuid);

        if (folder == null || userObject == null || folder.UserUuid != userObject.Uuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder does not exist. Please check the folder uuid.",
                InternalCode = 0x6
            });
        }

        if (recursive)
        {
            DeleteFolderItems(folder, databaseHandle);
        }
        else
        {
            ClearFolder(folder, databaseHandle);
        }
        databaseHandle.Folders.Remove(folder);
        databaseHandle.SaveChanges();
        return TypedResults.Ok();
    }

    /// <summary>
    /// Remove all Items from Folder 
    /// </summary>
    /// <param name="folder">Folder to remove the Items from</param>
    /// <param name="databaseHandle">Database to handle</param>
    private static void ClearFolder(Folder folder, DatabaseHandle databaseHandle)
    {
        foreach (Credential credential in databaseHandle.Credentials.Where(c => c.FolderUuid == folder.Uuid))
        {
            credential.FolderUuid = null;
        }
    }

    /// <summary>
    /// Delete all Items in Folder
    /// </summary>
    /// <param name="folder">Folder to delete the Items from</param>
    /// <param name="databaseHandle">Database to handle</param>
    private static void DeleteFolderItems(Folder folder, DatabaseHandle databaseHandle)
    {
        databaseHandle.Credentials.RemoveRange(databaseHandle.Credentials.Where(c => c.FolderUuid == folder.Uuid));
    }
}
