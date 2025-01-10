using AnonKeyBackend.ApiDatastructures.Folders.Create;
using AnonKeyBackend.Data;
using AnonKeyBackend.Models;


namespace AnonKeyBackend.ApiEndpoints.Folders;

/// <summary>
/// Handles the folders create endpoint.
/// </summary>
public static class Create
{

    /// <summary>
    /// Creates a new folder.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.Create.FoldersCreateResponseBody>,
        Conflict<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostCreate(ApiDatastructures.Folders.Create.FoldersCreateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        databaseHandle.Database.EnsureCreated();
        if (requestBody.Folder is null || String.IsNullOrEmpty(requestBody.Folder.Name))
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The a parameter in the request was null",
                Detail = "One of the parameters in the request was null. This is not allowed, please fill in all parameters."
            });
        }

        if (databaseHandle.Folders.Any(f => f.DisplayName == requestBody.Folder.Name))
        {
            return TypedResults.Conflict(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder name is already taken",
                Detail = "The folder name is already taken. Please choose a different name."
            });
        }

        string folderUuid = CreateNewFolder(requestBody, user, databaseHandle);

        return TypedResults.Ok(new ApiDatastructures.Folders.Create.FoldersCreateResponseBody
        {
            FolderUuid = folderUuid
        });
    }

    private static string CreateNewFolder(FoldersCreateRequestBody requestBody, ClaimsPrincipal user, DatabaseHandle databaseHandle)
    {
        User userObject = RetrieveUserFromDatabase(user, databaseHandle);
        if (requestBody.Folder is null) throw new ArgumentNullException();

        Models.Folder folder = new()
        {
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = userObject.Uuid,
            DisplayName = requestBody.Folder.Name,
            Icon = requestBody.Folder.Icon
        };

        databaseHandle.Folders.Add(folder);
        databaseHandle.SaveChanges();

        return folder.Uuid;
    }

    private static User RetrieveUserFromDatabase(ClaimsPrincipal user, DatabaseHandle databaseHandle)
    {
        if (user.Identity is null) throw new ArgumentNullException();
        return databaseHandle.Users.First(u => u.Username == user.Identity.Name);
    }
}
