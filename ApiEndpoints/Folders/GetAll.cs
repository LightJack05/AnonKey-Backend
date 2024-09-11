namespace AnonKey_Backend.ApiEndpoints.Folders;

public static class GetAll
{
    /// <summary>
    /// Gets all folders for a user.
    /// </summary>
    public static Microsoft.AspNetCore.Http.HttpResults.Results<
        Ok<ApiDatastructures.Folders.GetAll.FoldersGetAllResponseBody>,
        NotFound<ApiDatastructures.Error.ErrorResponseBody>,
        BadRequest<ApiDatastructures.Error.ErrorResponseBody>>
            GetGetAll()
    {
        throw new NotImplementedException();
        
    }
}
