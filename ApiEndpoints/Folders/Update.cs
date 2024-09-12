namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class Update
{
    /// <summary>
    /// Updates an existing folder object.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.Update.FoldersUpdateResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            PutUpdate(ApiDatastructures.Folders.Update.FoldersUpdateRequestBody requestBody)
    {
        throw new NotImplementedException();
        
    }
}
