using AnonKeyBackend.Data;
using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Folders;

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
        NotFound<ApiDatastructures.RequestError.ErrorResponseBody>,
        BadRequest<ApiDatastructures.RequestError.ErrorResponseBody>>
            DeleteDelete(string folderUuid, bool recursive, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null. Did you provide a valid JWT token?"
            });
        }
        databaseHandle.Database.EnsureCreated();
        if (String.IsNullOrEmpty(folderUuid))
        {
            return TypedResults.BadRequest(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The a parameter in the request was null",
                Detail = "One of the parameters in the request was null. This is not allowed, please fill in all parameters."
            });
        }

        User? userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        Folder? folder = databaseHandle.Folders.FirstOrDefault(f => f.Uuid == folderUuid);

        if (folder == null || userObject == null || folder.UserUuid != userObject.Uuid)
        {
            return TypedResults.NotFound(new ApiDatastructures.RequestError.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder does not exist. Please check the folder uuid."
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
        foreach (Credential item in databaseHandle.Credentials.Where(c => c.FolderUuid == folder.Uuid))
        {
            item.DeletedTimestamp = (long)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
            item.FolderUuid = null;
        }
    }
}
