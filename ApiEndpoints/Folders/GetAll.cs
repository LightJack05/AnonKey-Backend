using AnonKeyBackend.ApiDatastructures.Folders.GetAll;
using AnonKeyBackend.Data;
using AnonKeyBackend.Models;

namespace AnonKeyBackend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders getall endpoint.
/// </summary>
public static class GetAll
{
    /// <summary>
    /// Gets all folders for a user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGetAll(ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        if (user.Identity == null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user identity is null",
                Detail = "The user identity is null, therefore no data extraction is possible"
            });
        }

        User? userObject = databaseHandle.Users.FirstOrDefault(u => u.Username == user.Identity.Name);
        if (userObject == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user does not exist",
                Detail = "The user does not exist in the database."
            });
        }
        FoldersGetAllResponseBody folders = GetAllFolders(userObject, databaseHandle);
        return TypedResults.Ok(folders);
    }

    private static FoldersGetAllResponseBody GetAllFolders(User userObject, DatabaseHandle databaseHandle)
    {
        List<AnonKeyBackend.Models.Folder> fetchedFolders = databaseHandle.Folders.Where(f => f.UserUuid == userObject.Uuid).ToList();
        AnonKeyBackend.ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody result = new AnonKeyBackend.ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody();
        result.Folder = new List<FoldersGetAllFolder>();
        foreach (AnonKeyBackend.Models.Folder fetchedFolder in fetchedFolders)
        {
            result.Folder.Add(new AnonKeyBackend.ApiDatastructures.Folders.GetAll.FoldersGetAllFolder()
            {
                Uuid = fetchedFolder.Uuid,
                Name = fetchedFolder.DisplayName,
                Icon = fetchedFolder.Icon
            });
        }

        return result;
    }
}
