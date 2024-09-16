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
       throw new NotImplementedException(); 
    }
}
