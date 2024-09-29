using AnonKey_Backend.ApiDatastructures.Folders.GetAll;
using AnonKey_Backend.Data;
using AnonKey_Backend.Models;

namespace AnonKey_Backend.ApiEndpoints.Folders;

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
        if (userObject == null)
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The user does not exist",
                Detail = "The user does not exist in the database.",
                InternalCode = 0x6
            });
        }
        FoldersGetAllResponseBody folders = GetAllFolders(userObject, databaseHandle);
        return TypedResults.Ok(folders);
    }

    private static FoldersGetAllResponseBody GetAllFolders(User userObject, DatabaseHandle databaseHandle)
    {
        List<AnonKey_Backend.Models.Folder> fetchedFolders = databaseHandle.Folders.Where(f => f.UserUuid == userObject.Uuid).ToList();
        AnonKey_Backend.ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody result = new AnonKey_Backend.ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody();
        result.Folder = new List<FoldersGetAllFolder>();
        foreach (AnonKey_Backend.Models.Folder fetchedFolder in fetchedFolders)
        {
            result.Folder.Add(new AnonKey_Backend.ApiDatastructures.Folders.GetAll.FoldersGetAllFolder()
            {
                Uuid = fetchedFolder.Uuid,
                Name = fetchedFolder.DisplayName,
                Icon = fetchedFolder.Icon
            });
        }

        return result;
    }
}
