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
        if (!databaseHandle.Folders.Any(f => f.Uuid == folderUuid))
        {
            return TypedResults.NotFound(new ApiDatastructures.Error.ErrorResponseBody()
            {
                Message = "The folder does not exist",
                Detail = "The folder with the given UUID does not exist.",
                InternalCode = 0x6
            });
        }

        
    }
}
