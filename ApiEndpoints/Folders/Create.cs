using System.Text.RegularExpressions;
using AnonKey_Backend.ApiDatastructures.Folders.Create;
using AnonKey_Backend.Data;


namespace AnonKey_Backend.ApiEndpoints.Folders;

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
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PostCreate(ApiDatastructures.Folders.Create.FoldersCreateRequestBody requestBody, ClaimsPrincipal user, Data.DatabaseHandle databaseHandle)
    {
        //throw new NotImplementedException();

        databaseHandle.Database.EnsureCreated();
        if (requestBody.Folder.Name is null)
        {
            return TypedResults.BadRequest(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The a parameter in the request was null",
                Detail = "One of the parameters in the request was null. This is not allowed, please fill in all parameters.",
                InternalCode = 0x4
            });
        }

        string token = CreateNewFolder(requestBody, user, databaseHandle);

        return TypedResults.Ok(new ApiDatastructures.Folders.Create.FoldersCreateResponseBody
        {
            FolderUuid = token
        });
    }

    private static string CreateNewFolder(FoldersCreateRequestBody requestBody, ClaimsPrincipal user, DatabaseHandle databaseHandle)
    {
        Models.Folder folder = new()
        {
            Uuid = Guid.NewGuid().ToString(),
            UserUuid = user.FindFirst(ClaimTypes.NameIdentifier)?.Value, //weiß nicht ob das so richtig ist
            DisplayName = requestBody.Folder.Name,
            Icon = requestBody.Folder.Icon.ToString()
        };
        
        databaseHandle.Folders.Add(folder);
        databaseHandle.SaveChanges();

        return folder.Uuid;
    }
}
